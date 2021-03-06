#region License
/*
MIT License
Copyright ?2003-2005 Tao Framework Team
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

#region Original Credits / License
/* Copyright (c) Mark J. Kilgard, 1994. */
/*
 * (c) Copyright 1993, Silicon Graphics, Inc.
 * ALL RIGHTS RESERVED 
 * Permission to use, copy, modify, and distribute this software for 
 * any purpose and without fee is hereby granted, provided that the above
 * copyright notice appear in all copies and that both the copyright notice
 * and this permission notice appear in supporting documentation, and that 
 * the name of Silicon Graphics, Inc. not be used in advertising
 * or publicity pertaining to distribution of the software without specific,
 * written prior permission. 
 *
 * THE MATERIAL EMBODIED ON THIS SOFTWARE IS PROVIDED TO YOU "AS-IS"
 * AND WITHOUT WARRANTY OF ANY KIND, EXPRESS, IMPLIED OR OTHERWISE,
 * INCLUDING WITHOUT LIMITATION, ANY WARRANTY OF MERCHANTABILITY OR
 * FITNESS FOR A PARTICULAR PURPOSE.  IN NO EVENT SHALL SILICON
 * GRAPHICS, INC.  BE LIABLE TO YOU OR ANYONE ELSE FOR ANY DIRECT,
 * SPECIAL, INCIDENTAL, INDIRECT OR CONSEQUENTIAL DAMAGES OF ANY
 * KIND, OR ANY DAMAGES WHATSOEVER, INCLUDING WITHOUT LIMITATION,
 * LOSS OF PROFIT, LOSS OF USE, SAVINGS OR REVENUE, OR THE CLAIMS OF
 * THIRD PARTIES, WHETHER OR NOT SILICON GRAPHICS, INC.  HAS BEEN
 * ADVISED OF THE POSSIBILITY OF SUCH LOSS, HOWEVER CAUSED AND ON
 * ANY THEORY OF LIABILITY, ARISING OUT OF OR IN CONNECTION WITH THE
 * POSSESSION, USE OR PERFORMANCE OF THIS SOFTWARE.
 * 
 * US Government Users Restricted Rights 
 * Use, duplication, or disclosure by the Government is subject to
 * restrictions set forth in FAR 52.227.19(c)(2) or subparagraph
 * (c)(1)(ii) of the Rights in Technical Data and Computer Software
 * clause at DFARS 252.227-7013 and/or in similar or successor
 * clauses in the FAR or the DOD or NASA FAR Supplement.
 * Unpublished-- rights reserved under the copyright laws of the
 * United States.  Contractor/manufacturer is Silicon Graphics,
 * Inc., 2011 N.  Shoreline Blvd., Mountain View, CA 94039-7311.
 *
 * OpenGL(TM) is a trademark of Silicon Graphics, Inc.
 */
#endregion Original Credits / License

using System;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace Redbook {
    #region Class Documentation
    /// <summary>
    ///     This program shows a NURBS (Non-uniform rational B-splines) surface, shaped like
    ///     a heart.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Original Author:    Mark J. Kilgard
    ///     </para>
    ///     <para>
    ///         C# Implementation:  Randy Ridge
    ///         http://www.taoframework.com
    ///     </para>
    /// </remarks>
    #endregion Class Documentation
    public sealed class Nurbs {
        // --- Fields ---
        #region Private Constants
        private const int SPOINTS = 13;
        private const int SORDER = 3;
        private const int SKNOTS = (SPOINTS + SORDER);
        private const int TPOINTS = 3;
        private const int TORDER = 3;
        private const int TKNOTS = (TPOINTS + TORDER);
        private const float SQRT2 = 1.41421356237309504880f;
        #endregion Private Constants

        #region Private Fields
        private static float[] sknots = {
            -1.0f, -1.0f, -1.0f, 0.0f, 1.0f, 2.0f, 3.0f, 4.0f,
            4.0f,  5.0f,  6.0f, 7.0f, 8.0f, 9.0f, 9.0f, 9.0f
        };
        private static float[] tknots = {1.0f, 1.0f, 1.0f, 2.0f, 2.0f, 2.0f};
        private static float[/*S_NUMPOINTS*/, /*T_NUMPOINTS*/, /*4*/] controlPoints = {
			{
				{4.0f, 2.0f, 2.0f, 1.0f},
				{4.0f, 1.6f, 2.5f, 1.0f},
				{4.0f, 2.0f, 3.0f, 1.0f}
			},
			{
				{5.0f, 4.0f, 2.0f, 1.0f},
				{5.0f, 4.0f, 2.5f, 1.0f},
				{5.0f, 4.0f, 3.0f, 1.0f}
			},
			{
				{6.0f, 5.0f, 2.0f, 1.0f},
				{6.0f, 5.0f, 2.5f, 1.0f},
				{6.0f, 5.0f, 3.0f, 1.0f}
			},
			{
				{SQRT2 * 6.0f, SQRT2 * 6.0f, SQRT2 * 2.0f, SQRT2},
				{SQRT2 * 6.0f, SQRT2 * 6.0f, SQRT2 * 2.5f, SQRT2},
				{SQRT2 * 6.0f, SQRT2 * 6.0f, SQRT2 * 3.0f, SQRT2}
			},
			{
				{5.2f, 6.7f, 2.0f, 1.0f},
				{5.2f, 6.7f, 2.5f, 1.0f},
				{5.2f, 6.7f, 3.0f, 1.0f}
			},
			{
				{SQRT2 * 4.0f, SQRT2 * 6.0f, SQRT2 * 2.0f, SQRT2},
				{SQRT2 * 4.0f, SQRT2 * 6.0f, SQRT2 * 2.5f, SQRT2},
				{SQRT2 * 4.0f, SQRT2 * 6.0f, SQRT2 * 3.0f, SQRT2}
			},
			{
				{4.0f, 5.2f, 2.0f, 1.0f},
				{4.0f, 4.6f, 2.5f, 1.0f},
				{4.0f, 5.2f, 3.0f, 1.0f}
			},
			{
				{SQRT2 * 4.0f, SQRT2 * 6.0f, SQRT2 * 2.0f, SQRT2},
				{SQRT2 * 4.0f, SQRT2 * 6.0f, SQRT2 * 2.5f, SQRT2},
				{SQRT2 * 4.0f, SQRT2 * 6.0f, SQRT2 * 3.0f, SQRT2}
			},
			{
				{2.8f, 6.7f, 2.0f, 1.0f},
				{2.8f, 6.7f, 2.5f, 1.0f},
				{2.8f, 6.7f, 3.0f, 1.0f}
			},
			{
				{SQRT2 * 2.0f, SQRT2 * 6.0f, SQRT2 * 2.0f, SQRT2},
				{SQRT2 * 2.0f, SQRT2 * 6.0f, SQRT2 * 2.5f, SQRT2},
				{SQRT2 * 2.0f, SQRT2 * 6.0f, SQRT2 * 3.0f, SQRT2}
			},
			{
				{2.0f, 5.0f, 2.0f, 1.0f},
				{2.0f, 5.0f, 2.5f, 1.0f},
				{2.0f, 5.0f, 3.0f, 1.0f}
			},
			{
				{3.0f, 4.0f, 2.0f, 1.0f},
				{3.0f, 4.0f, 2.5f, 1.0f},
				{3.0f, 4.0f, 3.0f, 1.0f}
			},
			{
				{4.0f, 2.0f, 2.0f, 1.0f},
				{4.0f, 1.6f, 2.5f, 1.0f},
				{4.0f, 2.0f, 3.0f, 1.0f}
			}
		};
        private static Glu.GLUnurbs nurb;
        #endregion Private Fields

        // --- Entry Point ---
        #region Run()
        /// <summary>
        ///     <para>
        ///         Open window with initial window size, title bar, RGBA display mode, and
        ///         handle input events.
        ///     </para>
        /// </summary>
        [STAThread]
        public static void Run() {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_SINGLE | Glut.GLUT_RGB | Glut.GLUT_DEPTH);
            Glut.glutCreateWindow("Nurbs");
            Init();
            Glut.glutDisplayFunc(new Glut.DisplayCallback(Display));
            Glut.glutKeyboardFunc(new Glut.KeyboardCallback(Keyboard));
            Glut.glutReshapeFunc(new Glut.ReshapeCallback(Reshape));
            Glut.glutMainLoop();
        }
        #endregion Run()

        // --- Application Methods ---
        #region Init()
        /// <summary>
        ///     <para>
        ///         Initialize depth buffer, light source, material property, and lighting model.
        ///     </para>
        /// </summary>
        private static void Init() {
            float[] materialAmbient = {1.0f, 1.0f, 1.0f, 1.0f};
            float[] materialDiffuse = {1.0f, 0.2f, 1.0f, 1.0f};
            float[] materialSpecular = {1.0f, 1.0f, 1.0f, 1.0f};
            float[] materialShininess = {50.0f};
            float[] light0Position = {1.0f, 0.1f, 1.0f, 0.0f};
            float[] light1Position = {-1.0f, 0.1f, 1.0f, 0.0f};
            float[] lightModelAmbient = {0.3f, 0.3f, 0.3f, 1.0f};

            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT, materialAmbient);
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_DIFFUSE, materialDiffuse);
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SPECULAR, materialSpecular);
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SHININESS, materialShininess);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, light0Position);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_POSITION, light1Position);
            Gl.glLightModelfv(Gl.GL_LIGHT_MODEL_AMBIENT, lightModelAmbient);

            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);
            Gl.glEnable(Gl.GL_LIGHT1);
            Gl.glDepthFunc(Gl.GL_LESS);
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_AUTO_NORMAL);

            nurb = Glu.gluNewNurbsRenderer();

            Glu.gluNurbsProperty(nurb, Glu.GLU_SAMPLING_TOLERANCE, 25.0f);
            Glu.gluNurbsProperty(nurb, Glu.GLU_DISPLAY_MODE, Glu.GLU_FILL);
        }
        #endregion Init()

        // --- Callbacks ---
        #region Display()
        private static void Display() {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glPushMatrix();
                Gl.glTranslatef(4.0f, 4.5f, 2.5f);
                Gl.glRotatef(220.0f, 1.0f, 0.0f, 0.0f);
                Gl.glRotatef(115.0f, 0.0f, 1.0f, 0.0f);
                Gl.glTranslatef(-4.0f, -4.5f, -2.5f);

                Glu.gluBeginSurface(nurb);
                    Glu.gluNurbsSurface(nurb, SKNOTS, sknots, TKNOTS, tknots, 4 * TPOINTS, 4, controlPoints, SORDER, TORDER, Gl.GL_MAP2_VERTEX_4);
                Glu.gluEndSurface(nurb);
            Gl.glPopMatrix();
            Gl.glFlush();
        }
        #endregion Display()

        #region Keyboard(byte key, int x, int y)
        private static void Keyboard(byte key, int x, int y) {
            switch(key) {
                case 27:
                    Environment.Exit(0);
                    break;
            }
        }
        #endregion Keyboard(byte key, int x, int y)

        #region Reshape(int w, int h)
        private static void Reshape(int w, int h) {
            Gl.glViewport(0, 0, w, h);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glFrustum(-1.0, 1.0, -1.5, 0.5, 0.8, 10.0);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Glu.gluLookAt(7.0, 4.5, 4.0, 4.5, 4.5, 2.0, 6.0, -3.0, 2.0);
        }
        #endregion Reshape(int w, int h)
    }
}
