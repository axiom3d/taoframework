namespace FtGlSimple
{
    using Tao.FtGl;
    using Tao.FreeGlut;
    using Tao.FreeType;
    using Tao.OpenGl;
    using System;
    using System.Reflection;
    public class FtGlSimple
    {
        static float[] texture = new float[]{ 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
                                              1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
                                              0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f,
                                              0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f};

        private const int FTGL_BITMAP=0;
        private const int FTGL_PIXMAP = 1;
        private const int FTGL_OUTLINE = 2;
        private const int FTGL_POLYGON = 3;
        private const int FTGL_EXTRUDE = 4;
        private const int FTGL_TEXTURE = 5;
        private static int current_font = FTGL_EXTRUDE;

        private static int w_win = 640, h_win = 480;
        private enum mode_t{INTERACTIVE, EDITING};
        private static mode_t mode = mode_t.INTERACTIVE;

        private static string myString = "a";

        static private void setUpLighting()
        {
            // Set up lighting.
            float[] light1_ambient = new float [] { 1.0f, 1.0f, 1.0f, 1.0f };
            float[] light1_diffuse  = new float [] { 1.0f, 0.9f, 0.9f, 1.0f };
            float[] light1_specular = new float [] { 1.0f, 0.7f, 0.7f, 1.0f };
            float[] light1_position = new float [] { -1.0f, 1.0f, 1.0f, 0.0f };
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_AMBIENT,  light1_ambient);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_DIFFUSE,  light1_diffuse);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_SPECULAR, light1_specular);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_POSITION, light1_position);
            Gl.glEnable(Gl.GL_LIGHT1);

            float[] light2_ambient  = new float [] { 0.2f, 0.2f, 0.2f, 1.0f };
            float[] light2_diffuse  = new float [] { 0.9f, 0.9f, 0.9f, 1.0f };
            float[] light2_specular = new float [] { 0.7f, 0.7f, 0.7f, 1.0f };
            float[] light2_position = new float [] { 1.0f, -1.0f, -1.0f, 0.0f };
            Gl.glLightfv(Gl.GL_LIGHT2, Gl.GL_AMBIENT,  light2_ambient);
            Gl.glLightfv(Gl.GL_LIGHT2, Gl.GL_DIFFUSE,  light2_diffuse);
            Gl.glLightfv(Gl.GL_LIGHT2, Gl.GL_SPECULAR, light2_specular);
            Gl.glLightfv(Gl.GL_LIGHT2, Gl.GL_POSITION, light2_position);
            //Gl.glEnable(Gl.GL_LIGHT2);

            float[] front_emission = new float [] { 0.3f, 0.2f, 0.1f, 0.0f };
            float[] front_ambient  = new float [] { 0.2f, 0.2f, 0.2f, 0.0f };
            float[] front_diffuse  = new float [] { 0.95f, 0.95f, 0.8f, 0.0f };
            float[] front_specular = new float [] { 0.6f, 0.6f, 0.6f, 0.0f };
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_EMISSION, front_emission);
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT, front_ambient);
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_DIFFUSE, front_diffuse);
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SPECULAR, front_specular);
            Gl.glMaterialf(Gl.GL_FRONT, Gl.GL_SHININESS, 16.0f);
            Gl.glColor4fv(front_diffuse);

            Gl.glLightModeli(Gl.GL_LIGHT_MODEL_TWO_SIDE, Gl.GL_FALSE);
            Gl.glColorMaterial(Gl.GL_FRONT, Gl.GL_DIFFUSE);
            Gl.glEnable(Gl.GL_COLOR_MATERIAL);

            Gl.glEnable(Gl.GL_LIGHTING);
        }

        const string fontName = "/usr/share/fonts/truetype/ttf-dejavu/DejaVuSerif-BoldItalic.ttf";
        static FtGl.FTFont[] fonts = new FtGl.FTFont[6];
        static FtGl.FTFont infoFont = new FtGl.FTGLPixmapFont(fontName);

        private static void setUpFonts()
        {
            fonts[FTGL_BITMAP] = new FtGl.FTGLBitmapFont(fontName);
            fonts[FTGL_PIXMAP] = new FtGl.FTGLPixmapFont(fontName);
            fonts[FTGL_OUTLINE] = new FtGl.FTGLOutlineFont(fontName);
            fonts[FTGL_POLYGON] = new FtGl.FTGLPolygonFont(fontName);
            fonts[FTGL_EXTRUDE] = new FtGl.FTGLExtrudeFont(fontName);
            fonts[FTGL_TEXTURE] = new FtGl.FTGLTextureFont(fontName);

            for( int x = 0; x < 6; ++x)
            {
                if( fonts[x].Error() < 0)
                {
                    Console.WriteLine("Failed to open font {0}", fontName);
                    System.Environment.Exit(0);
                }

                if( fonts[x].FaceSize( 144) == 0)
                {
                    Console.WriteLine("Failed to set size");
                    System.Environment.Exit(0);
                }

                fonts[x].Depth(20);

                fonts[x].CharMap( FT_Encoding.FT_ENCODING_UNICODE);
            }

            //infoFont.FTGLPixmapFont(fontName);

            if( infoFont.Error() < 0)
            {
                Console.WriteLine("Failed to open font {0}", fontName);
                System.Environment.Exit(0);
            }

            infoFont.FaceSize( 18);

            //myString[0] = 65;
            //myString[1] = 0;
        }

        static private void renderFontmetrics()
        {
            float x1, y1, z1, x2, y2, z2;
            fonts[(int)current_font].BBox( myString, out x1, out y1, out z1, out x2, out y2, out z2);

            // Draw the bounding box
            Gl.glDisable( Gl.GL_LIGHTING);
            Gl.glDisable( Gl.GL_TEXTURE_2D);
            Gl.glEnable( Gl.GL_LINE_SMOOTH);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc( Gl.GL_SRC_ALPHA, Gl.GL_ONE); // GL_ONE_MINUS_SRC_ALPHA

            Gl.glColor3f( 0.0f, 1.0f, 0.0f);
            // Draw the front face
            Gl.glBegin( Gl.GL_LINE_LOOP);
                Gl.glVertex3f( x1, y1, z1);
                Gl.glVertex3f( x1, y2, z1);
                Gl.glVertex3f( x2, y2, z1);
                Gl.glVertex3f( x2, y1, z1);
            Gl.glEnd();
            // Draw the back face
            if( current_font == FTGL_EXTRUDE && z1 != z2)
            {
                Gl.glBegin( Gl.GL_LINE_LOOP);
                    Gl.glVertex3f( x1, y1, z2);
                    Gl.glVertex3f( x1, y2, z2);
                    Gl.glVertex3f( x2, y2, z2);
                    Gl.glVertex3f( x2, y1, z2);
                Gl.glEnd();
            // Join the faces
                Gl.glBegin( Gl.GL_LINES);
                    Gl.glVertex3f( x1, y1, z1);
                    Gl.glVertex3f( x1, y1, z2);

                    Gl.glVertex3f( x1, y2, z1);
                    Gl.glVertex3f( x1, y2, z2);

                    Gl.glVertex3f( x2, y2, z1);
                    Gl.glVertex3f( x2, y2, z2);

                    Gl.glVertex3f( x2, y1, z1);
                    Gl.glVertex3f( x2, y1, z2);
                Gl.glEnd();
            }
             // Draw the baseline, Ascender and Descender
            Gl.glBegin( Gl.GL_LINES);
                Gl.glColor3f( 0.0f, 0.0f, 1.0f);
                Gl.glVertex3f( 0.0f, 0.0f, 0.0f);
                Gl.glVertex3f( fonts[(int)current_font].Advance( myString), 0.0f, 0.0f);
                Gl.glVertex3f( 0.0f, fonts[(int)current_font].Ascender(), 0.0f);
                Gl.glVertex3f( 0.0f, fonts[(int)current_font].Descender(), 0.0f);
            Gl.glEnd();

            // Draw the origin
            Gl.glColor3f( 1.0f, 0.0f, 0.0f);
            Gl.glPointSize( 5.0f);
            Gl.glBegin( Gl.GL_POINTS);
                Gl.glVertex3f( 0.0f, 0.0f, 0.0f);
            Gl.glEnd();
        }
        private static void renderFontInfo()
        {
            Gl.glMatrixMode( Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluOrtho2D(0f, (float)w_win, 0f, (float)h_win);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            // draw mode
            Gl.glColor3f( 1.0f, 1.0f, 1.0f);
            Gl.glRasterPos2f( 20.0f , h_win - ( 20.0f + infoFont.Ascender()));

            switch( mode)
            {
                case mode_t.EDITING:
                    infoFont.Render("Edit Mode");
                    break;
                case mode_t.INTERACTIVE:
                    break;
            }

            // draw font type
            Gl.glRasterPos2i( 20 , 20);
            switch( current_font)
            {
                case FTGL_BITMAP:
                    infoFont.Render("Bitmap Font");
                    break;
                case FTGL_PIXMAP:
                    infoFont.Render("Pixmap Font");
                    break;
                case FTGL_OUTLINE:
                    infoFont.Render("Outline Font");
                    break;
                case FTGL_POLYGON:
                    infoFont.Render("Polygon Font");
                    break;
                case FTGL_EXTRUDE:
                    infoFont.Render("Extruded Font");
                    break;
                case FTGL_TEXTURE:
                    infoFont.Render("Texture Font");
                    break;
            }

            Gl.glRasterPos2f( 20.0f , 20.0f + infoFont.LineHeight());
            infoFont.Render(fontName);
        }
        static int textureID;
        private static void do_display ()
        {
            switch( current_font)
            {
                case FTGL_BITMAP:
                case FTGL_PIXMAP:
                case FTGL_OUTLINE:
                    break;
                case FTGL_POLYGON:
                    Gl.glEnable( Gl.GL_TEXTURE_2D);
                    Gl.glBindTexture(Gl.GL_TEXTURE_2D, textureID);
                    Gl.glDisable( Gl.GL_BLEND);
                    setUpLighting();
                    break;
                case FTGL_EXTRUDE:
                    Gl.glEnable( Gl.GL_DEPTH_TEST);
                    Gl.glDisable( Gl.GL_BLEND);
                    Gl.glEnable( Gl.GL_TEXTURE_2D);
                    Gl.glBindTexture(Gl.GL_TEXTURE_2D, textureID);
                    setUpLighting();
                    break;
                case FTGL_TEXTURE:
                    Gl.glEnable( Gl.GL_TEXTURE_2D);
                    Gl.glDisable( Gl.GL_DEPTH_TEST);
                    setUpLighting();
                    Gl.glNormal3f( 0.0f, 0.0f, 1.0f);
                    break;

            }

            Gl.glColor3f( 1.0f, 1.0f, 1.0f);
        // If you do want to switch the color of bitmaps rendered with glBitmap,
        // you will need to explicitly call glRasterPos3f (or its ilk) to lock
        // in a changed current color.
            Gl.glPushMatrix();
                fonts[(int)current_font].Render( myString);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
                renderFontmetrics();
            Gl.glPopMatrix();

            renderFontInfo();
        }

        private static void display()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
               SetCamera();

            switch( current_font)
            {
                case FTGL_BITMAP:
                case FTGL_PIXMAP:
                    Gl.glRasterPos2i( w_win / 2, h_win / 2);
                    Gl.glTranslatef(  w_win / 2f, h_win / 2f, 0.0f);
                    break;
                case FTGL_OUTLINE:
                case FTGL_POLYGON:
                case FTGL_EXTRUDE:
                case FTGL_TEXTURE:
                    //tbMatrix();
                    break;
            }

            Gl.glPushMatrix();

            do_display();

            Gl.glPopMatrix();

            Glut.glutSwapBuffers();
        }

        private static void mytest()
        {
           int cnt = fonts[0].CharMapCount();
           Console.WriteLine("CharMap : {0}", cnt);
           float line = fonts[0].LineHeight();
           Console.WriteLine("Line Height : {0}", line);
           float des = fonts[0].Descender();
           Console.WriteLine("Descender : {0}", des);
           float asc = fonts[0].Ascender();
           Console.WriteLine("Ascender : {0}", asc);
           float adv = fonts[0].Advance("Coucou");
           Console.WriteLine("Advance : {0}", adv);
           bool att = fonts[0].Attach("/usr/share/fonts/truetype/FreeMono.ttf");
           Console.WriteLine("Attach : {0}", att);
           att = fonts[0].Attach("dsadsadas", 10);
           Console.WriteLine("Attach : {0}", att);

           fonts[FTGL_EXTRUDE].SetDisplayList(false);
        }

        private static void myinit()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glClearColor( 0.13f, 0.17f, 0.32f, 0.0f);
            Gl.glColor3f( 1.0f, 1.0f, 1.0f);

            Gl.glEnable( Gl.GL_CULL_FACE);
            Gl.glFrontFace( Gl.GL_CCW);

            Gl.glEnable( Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_CULL_FACE);
            Gl.glShadeModel(Gl.GL_SMOOTH);

            Gl.glEnable( Gl.GL_POLYGON_OFFSET_LINE);
            Gl.glPolygonOffset( 1.0f, 1.0f); // ????

            SetCamera();

            //tbInit(GLUT_LEFT_BUTTON);
            //tbAnimate( Gl.GL_FALSE);

            setUpFonts();

            Gl.glGenTextures(1, out textureID);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, textureID);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_NEAREST);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_NEAREST);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB, 4, 4, 0, Gl.GL_RGB, Gl.GL_FLOAT, texture);


        }

        static private void parsekey(byte key, int x, int y)
        {
            int k = Convert.ToInt32(key.ToString());
            switch (k)
            {
                case 27:
                    for( int i = 0; i < 6; ++i)
                        fonts[i].Dispose();
                    System.Environment.Exit(0);
                    break;
                case 13:
                    if( mode == mode_t.EDITING)
                    {
                        mode = mode_t.INTERACTIVE;
                    }
                    else
                    {
                        mode = mode_t.EDITING;
                    }
                    break;
                case 32:
                    current_font++;
                    if((int)current_font > 5)
                        current_font = 0;
                    break;
                default:
                    byte[] tmp = new byte[1];
                    tmp[0] = key;
                    if( mode == mode_t.INTERACTIVE)
                        myString = System.Text.Encoding.ASCII.GetString(tmp);
                    else
                        myString += System.Text.Encoding.ASCII.GetString(tmp);
                    break;
                }
                Glut.glutPostRedisplay();
        }
        static private void parseSpecialKey(int key, int x, int y)
        {
            switch(key)
            {
                case Glut.GLUT_KEY_UP:
                    current_font = (current_font + 1) % 6;
                    break;
                case Glut.GLUT_KEY_DOWN:
                    current_font = (current_font + 5) % 6;
                    break;
            }
        }
        static private void mouse(int b, int s, int x, int y)
        {
            //tbMouse( button, state, x, y);
        }
        static private void motion(int x, int y)
        {
            //tbMotion( x, y);
        }
        static private void myReshape(int w, int h)
        {
            Gl.glMatrixMode (Gl.GL_MODELVIEW);
            Gl.glViewport (0, 0, w, h);
            Gl.glLoadIdentity();

            w_win = w;
            h_win = h;
            SetCamera();

            //tbReshape(w_win, h_win);

        }

        static private void SetCamera()
        {
            switch( current_font)
            {
                case FTGL_BITMAP:
                case FTGL_PIXMAP:
                    Gl.glMatrixMode( Gl.GL_PROJECTION);
                    Gl.glLoadIdentity();
                    Glu.gluOrtho2D(0, w_win, 0, h_win);
                    Gl.glMatrixMode(Gl.GL_MODELVIEW);
                    Gl.glLoadIdentity();
                    break;
                case FTGL_OUTLINE:
                case FTGL_POLYGON:
                case FTGL_EXTRUDE:
                case FTGL_TEXTURE:
                    Gl.glMatrixMode (Gl.GL_PROJECTION);
                    Gl.glLoadIdentity ();
                    Glu.gluPerspective( 90, (float)w_win / (float)h_win, 1, 1000);
                    Gl.glMatrixMode(Gl.GL_MODELVIEW);
                    Gl.glLoadIdentity();
                    Glu.gluLookAt( 0.0, 0.0, (float)h_win / 2.0f, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0);
                    break;
            }
        }

        static void Main(string[] args)
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_DEPTH | Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_MULTISAMPLE);
            Glut.glutInitWindowPosition(50, 50);
            Glut.glutInitWindowSize( w_win, h_win);
            Glut.glutCreateWindow("FTGL TEST");
            Glut.glutDisplayFunc(display);
            Glut.glutKeyboardFunc(parsekey);
            Glut.glutSpecialFunc(parseSpecialKey);
            Glut.glutMouseFunc(mouse);
            Glut.glutMotionFunc(motion);
            Glut.glutReshapeFunc(myReshape);
            Glut.glutIdleFunc(display);

            myinit();
            mytest();

            Glut.glutMainLoop();

        }
    }
}
