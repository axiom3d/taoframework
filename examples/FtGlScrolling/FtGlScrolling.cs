namespace FtGlScrolling
{
    using Tao.FtGl;
    using Tao.FreeGlut;
    using Tao.FreeType;
    using Tao.OpenGl;
    using System;
    using System.Collections.Generic;

    /// Starfield class. Puts stars at random on the screen.
    public class StarField
    {
        int _nstars;
        float[] _x, _y, _size;

        public StarField(int nstars)
        {
            _nstars = nstars;
            _x = new float[_nstars];
            _y = new float[_nstars];
            _size = new float[_nstars];

            Random rand = new Random();
            for(int i = 0; i < _nstars; i++)
            {
                _x[i] = rand.Next(-100, 100) * 0.1f;
                _y[i] = rand.Next(-100, 100) * 0.1f;
                _size[i] = rand.Next(1, 30) * 0.1f;
            }
        }

        public void Display()
        {
            Gl.glDisable(Gl.GL_FOG);
            Gl.glColor3f(0.8f, 0.8f, 0.8f);

            for(int i = 0; i < _nstars; i++)
            {
                Gl.glPointSize(_size[i]);
                Gl.glBegin(Gl.GL_POINTS);
                    Gl.glVertex3f(_x[i], _y[i], -2);
                Gl.glEnd();
            }
        }
    }

    /// Black fog class. Helps the text fade out.
    public class BlackFog
    {
        float density = 0.06f;
        float []fogColor = new float[4]{0.0f, 0.0f, 0.0f, 1.0f};

        public void Display()
        {
            Gl.glEnable(Gl.GL_FOG);
            Gl.glFogi(Gl.GL_FOG_MODE, Gl.GL_EXP2);
            Gl.glFogfv(Gl.GL_FOG_COLOR, fogColor);
            Gl.glFogf(Gl.GL_FOG_START, 5000.0f);
            Gl.glFogf(Gl.GL_FOG_END, 10000.0f);
            Gl.glFogf(Gl.GL_FOG_DENSITY, density);
            Gl.glHint(Gl.GL_FOG_HINT, Gl.GL_NICEST);
        }
    }

    /// The scrolltext class. This is where the FTGL stuff happens.
    public class ScrollText
    {
        DateTime _starttime;
        FtGl.FTSimpleLayout _layout;
        FtGl.FTFont _font;
        float _width = 15.0f;
        string _text;

        public ScrollText(string text)
        {
            _font = new FtGl.FTGLExtrudeFont("/usr/share/fonts/truetype/ttf-dejavu/DejaVuSans.ttf");
            _font.Depth(0.01f);
            _font.FaceSize(1);
            _font.Outset(0.0f, 0.02f);
            _font.CharMap(FT_Encoding.FT_ENCODING_UNICODE);

            _layout = new FtGl.FTSimpleLayout();
            _layout.SetLineLength(_width);
            _layout.SetFont(_font);
            _layout.SetAlignment(FtGl.TextAlignment.ALIGN_CENTER);

            _text = text;
            _starttime = DateTime.Now;
        }

        public void Display()
        {
            TimeSpan span = DateTime.Now.Subtract(_starttime);
            float translate = -10.0f + (float)span.TotalSeconds / 2;

            Gl.glPushMatrix();
                Gl.glTranslatef(-_width / 2, 0, 0);
                Gl.glRotatef(-90, 1, 0, 0);
                Gl.glTranslatef(0, translate, 0);
                Gl.glPushMatrix();
                    Gl.glColor3f(1.0f, 1.0f, 0.0f);
                    _layout.RenderMode(_text, FtGl.RenderMode.RENDER_FRONT);
                    Gl.glColor3f(0.8f, 0.8f, 0.0f);
                    _layout.RenderMode(_text, FtGl.RenderMode.RENDER_SIDE);
                Gl.glPopMatrix();
            Gl.glPopMatrix();
        }

        public void Destroy()
        {
            _font.Destroy();
            _layout.Destroy();
        }
    }

    /// Our main OpenGL application class.
    public class FtGlScrolling
    {
        static StarField _starfield;
        static ScrollText _scrolltext;
        static BlackFog _blackfog;

        static private void Idle()
        {
            Glut.glutPostRedisplay();
        }

        static private void Reshape(int w, int h)
        {
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(45, (float)w / (float)h, 1, 1000);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glViewport(0, 0, w, h);
            Gl.glLoadIdentity();
            Glu.gluLookAt(0.0f, 5.0f, 15.0f,
                          0.0f, 0.0f, 0.0f,
                          0.0f, 1.0f, 0.0f);
        }

        static private void Display()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 0.8f);

            Gl.glEnable(Gl.GL_DEPTH_TEST);

            _starfield.Display();
            _blackfog.Display();
            _scrolltext.Display();

            Glut.glutSwapBuffers();
        }

        static private void Parsekey(byte key, int x, int y)
        {
            int k = Convert.ToInt32(key.ToString());
            switch(k)
            {
                case 27:
                    _scrolltext.Destroy();
                    System.Environment.Exit(0);
                    break;
            }
        }

        static void Main(string[] args)
        {
            _starfield = new StarField(150);
            _blackfog = new BlackFog();
            _scrolltext = new ScrollText(
                "FTGL 2.1.3: A NEW VERSION\n"
              + "\n"
              + "FTGL is a free cross-platform Open Source library that "
              + "uses Freetype2 to simplify rendering fonts in OpenGL "
              + "applications. FTGL supports bitmap, pixmap, texture map, "
              + "outline, polygon mesh, and extruded polygon rendering "
              + "modes.\n"
              + "\n"
              + "FTGL offers both a C++ and a plain C programming interface. "
              + "Various bindings exist for other languages such as Python or "
              + "Ruby. This program was done in C# using FTGL's .NET "
              + "language bindings.\n"
              + "\n"
              + "It's time to press ESC because this is the end of the "
              + "scrolltext."
              + "\n\n\n\n\n\n\n\n\n\n\n\n\n"
              + "No, really.\n"
              + "\n\n\n\n\n\n\n\n\n\n\n\n\n"
              + "It's over.\n"
              + "\n\n\n\n\n\n\n\n\n\n\n\n\n"
              + "Please let go now!");

            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_DEPTH | Glut.GLUT_RGB | Glut.GLUT_DOUBLE);
            Glut.glutInitWindowPosition(50, 50);
            Glut.glutInitWindowSize(480, 270);
            Glut.glutCreateWindow("FTGL scrolltext");
            Glut.glutDisplayFunc(Display);
            Glut.glutKeyboardFunc(Parsekey);
            Glut.glutReshapeFunc(Reshape);
            Glut.glutIdleFunc(Idle);
            Glut.glutMainLoop();
        }
    }
}

