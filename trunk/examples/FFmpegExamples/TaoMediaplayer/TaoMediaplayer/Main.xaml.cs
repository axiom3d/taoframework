/*
Copyright (c) 2008 Tao Framework

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Tao.OpenAl;

namespace TaoMediaplayer
{
    public partial class Main : Window
    {
        MediaFile media;

        bool paused = false;
        bool playing = false;
        Stopwatch timer = new Stopwatch();
        private AudioSource audio = new AudioSource();
        private int audioformat;
        ManualResetEvent audiosync = new ManualResetEvent(false);

        private delegate void VoidDelegate();

        public Main()
        {
            InitializeComponent();
        }

        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            // File dialog
            OpenFileDialog opendialog = new OpenFileDialog();
            opendialog.CheckFileExists = true;
            opendialog.Multiselect = false;

            // Open media file
            if (opendialog.ShowDialog(this) == true)
                media = new MediaFile(opendialog.FileName);
            else
                return;

            // Translate audioformat
            if(media.NumChannels == 1)
            {
                if (media.AudioDepth == 8)
                    audioformat = Al.AL_FORMAT_MONO8;
                else if (media.AudioDepth == 16)
                    audioformat = Al.AL_FORMAT_MONO16;
                else 
                    throw new Exception("Unsupported audio bit depth");
            } else if(media.NumChannels == 2)
            {
                if (media.AudioDepth == 8)
                    audioformat = Al.AL_FORMAT_STEREO8;
                else if (media.AudioDepth == 16)
                    audioformat = Al.AL_FORMAT_STEREO16;
                else
                    throw new Exception("Unsupported audio bit depth");               
            } else
            {
                throw new Exception("Unsupported amount of channels");
            }
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            if (!playing)
            {
                // Start playing
                playing = true;

                if(media.HasVideo)
                    ThreadPool.QueueUserWorkItem(videoUpdater);
                if (media.HasAudio)
                {
                    ThreadPool.QueueUserWorkItem(audioUpdater);

                    // Wait until audio is buffered
                    audiosync.WaitOne();
                    audio.Play();
                }
                timer.Start();
                playButton.Content = "Pause";
            }
            else if (playing && !paused)
            {
                // Pause
                if(media.HasAudio)
                    audio.Pause();

                timer.Stop();
                paused = true;
                playButton.Content = "Play";
            }
            else if (playing && paused)
            {
                // Resume
                if(media.HasAudio)
                    audio.Play();

                timer.Start();
                paused = false;
                playButton.Content = "Pause";
            }
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            // Stop and rewind
            if(media.HasAudio)
                audio.Stop();

            timer.Reset();
            playing = false;
            paused = false;
            playButton.Content = "Play";
            media.Rewind();
        }     
   
        private void audioUpdater(object state)
        {
            // Notify audio api of the update thread
            audio.RegisterThread();

            // Allocate buffer
            IntPtr buffer = Marshal.AllocHGlobal(192000);

            try
            {
                while(playing)
                {
                    // Check if we have free audio buffers
                    if (audio.BufferFinished())
                    {
                        // Decode next audio frame
                        int buffersize = 192000;
                        bool rv = media.NextAudioFrame(buffer, ref buffersize, 20000);
                        if(!media.HasVideo)
                            playing = rv;

                        // Send audio frame to audio buffer
                        if (buffersize != 0)
                            audio.BufferData(buffer, buffersize, audioformat, media.Frequency);

                        audiosync.Set();

                        if (!rv)
                            break;
                    }

                    if(!audio.HasFreeBuffers())
                        Thread.Sleep(10);
                }

                audiosync.Reset();

                // Rewind when stopped
                if(!media.HasVideo)
                    media.Rewind();

            } finally
            {
                // Free buffer
                Marshal.FreeHGlobal(buffer);

                if(!media.HasVideo)
                    playButton.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                                      (VoidDelegate)delegate
                                                         {
                                                             playButton.Content = "Play";
                                                         });

            }
        }
        
        private void videoUpdater(object state)
        {
            // Allocate framebuffer
            IntPtr buffer = Marshal.AllocHGlobal(media.Width*media.Height*3);
            
            try
            {
                while (playing)
                {
                    // Load next frame, or stop playing if eof
                    double time = (double)timer.ElapsedMilliseconds/1000.0;
                    playing = media.NextVideoFrame(buffer, Tao.FFmpeg.FFmpeg.PixelFormat.PIX_FMT_RGB24, ref time);

                    if (time == 0)
                    {
                        // Load frame in image
                        videoImage.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                                                     (VoidDelegate) delegate
                                                                        {
                                                                            videoImage.Source =
                                                                                BitmapSource.Create(media.Width,
                                                                                                    media.Height, 96, 96,
                                                                                                    PixelFormats.Rgb24,
                                                                                                    null, buffer,
                                                                                                    media.Width*
                                                                                                    media.Height*3,
                                                                                                    (media.Width*3 + 3) &
                                                                                                    ~3);
                                                                        });
                    } else if (playing && time > 0.005)
                    {
                        // Wait for next frame
                        Thread.Sleep((int)(time * 1000.0));
                    }

                    // Wait while we're paused
                    while (paused)
                        Thread.Sleep((int)((1.0 / 25.0) * 1000));
                }

                // Rewind to start when video ended
                media.Rewind();
            }
            finally
            {
                // Free framebuffer, reset UI
                Marshal.FreeHGlobal(buffer);

                playButton.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                                                  (VoidDelegate) delegate
                                                                     {
                                                                         videoImage.Source = null;
                                                                         playButton.Content = "Play";
                                                                     });
            }
        }


    }
}
