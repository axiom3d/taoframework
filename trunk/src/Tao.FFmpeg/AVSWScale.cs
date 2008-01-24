#region License
/*
MIT License
Copyright ©2003-2006 Tao Framework Team
http://www.taoframework.com
All rights reserved.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion License

using System;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security;

namespace Tao.FFmpeg
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class FFmpeg
    {
        #region Private Constants
        #region string AVUTIL_NATIVE_LIBRARY
        /// <summary>
        ///     Specifies AVUTIL's native library archive.
        /// </summary>
        /// <remarks>
        ///     Specifies avutil.dll everywhere; will be mapped via .config for mono.
        /// </remarks>
        private const string AVSWSCALE_NATIVE_LIBRARY = "avswscale-0.5.dll";
        #endregion string AVSWSCALE_NATIVE_LIBRARY
		#endregion Private Constants
		
		[DllImport(AVSWSCALE_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
		public static extern void sws_freeContext(IntPtr SwsContext);
		
		[DllImport(AVSWSCALE_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr sws_getContext(int source_width, int source_height,
		int source_pix_fmt, int dest_width, int dest_height, int dest_pix_fmt, int flags,
		IntPtr srcFilter, IntPtr destFilter, IntPtr Param);
		
		[DllImport(AVSWSCALE_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
		public static extern int sws_scale(IntPtr SwsContext,
		IntPtr src, 
		int[] srcStride, 
        int srcSliceY, int srcSliceH, 
		IntPtr dst, 
		int[] dstStride);
		
/* values for the flags, the stuff on the command line is different */
        public const int SWS_FAST_BILINEAR=     1;
        public const int SWS_BILINEAR     =     2;
        public const int SWS_BICUBIC      =     4;
        public const int SWS_X            =     8;
        public const int SWS_POINT        =  0x10;
        public const int SWS_AREA         =  0x20;
        public const int SWS_BICUBLIN     =  0x40;
        public const int SWS_GAUSS        =  0x80;
        public const int SWS_SINC         = 0x100;
        public const int SWS_LANCZOS      = 0x200;
        public const int SWS_SPLINE       = 0x400;

        public const int SWS_SRC_V_CHR_DROP_MASK   =  0x30000;
        public const int SWS_SRC_V_CHR_DROP_SHIFT  =  16;

        public const int SWS_PARAM_DEFAULT     =      123456;

        public const int SWS_PRINT_INFO        =      0x1000;

//the following 3 flags are not completely implemented
//internal chrominace subsampling info
        public const int SWS_FULL_CHR_H_INT   = 0x2000;
//input subsampling info
        public const int SWS_FULL_CHR_H_INP   = 0x4000;
        public const int SWS_DIRECT_BGR       = 0x8000;
        public const int SWS_ACCURATE_RND     = 0x40000;

       // public const int SWS_CPU_CAPS_MMX     = 0x80000000;
       // public const int SWS_CPU_CAPS_MMX2    = 0x20000000;
       // public const int SWS_CPU_CAPS_3DNOW   = 0x40000000;
        //public const int SWS_CPU_CAPS_ALTIVEC = 0x10000000;
        //public const int SWS_CPU_CAPS_BFIN    = 0x01000000;

        public const double SWS_MAX_REDUCE_CUTOFF = 0.002;			
	}
}