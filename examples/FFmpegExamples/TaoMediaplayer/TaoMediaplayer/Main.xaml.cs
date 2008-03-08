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

namespace TaoMediaplayer
{
    public partial class Main : Window
    {
        MediaFile media;

        bool paused = false;
        bool playing = false;
        Stopwatch timer = new Stopwatch();

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
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            if (!playing)
            {
                // Start playing
                playing = true;
                timer.Start();
                ThreadPool.QueueUserWorkItem(videoUpdater);
                playButton.Content = "Pause";
            }
            else if (playing && !paused)
            {
                // Pause
                timer.Stop();
                paused = true;
                playButton.Content = "Play";
            }
            else if (playing && paused)
            {
                // Resume
                timer.Start();
                paused = false;
                playButton.Content = "Pause";
            }
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            // Stop and rewind
            timer.Reset();
            playing = false;
            paused = false;
            playButton.Content = "Play";
            media.Rewind();
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
