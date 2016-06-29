using System.Threading;
using System.Diagnostics;

using Tao.OpenGl;

using TestOpenGL.VisualObjects;

namespace TestOpenGL.Logic
{
    class Painter
    {
        public event IntEventDelegate EventFPSUpdate;

        delegate void updateVoid();

        System.Threading.Thread RenderThread;
        ManualResetEvent isNextRender = new ManualResetEvent(false);

        Camera camera; //TODO: вот как-то она тут не в тему, но куда её убрать?..

        int maxFPS;
        int pauseMillisecond;




        public Painter(Camera camera)
        {
            maxFPS = 60;
            pauseMillisecond = 0;

            this.camera = camera;


            SettingVisibleAreaSize();

            RenderThread = new System.Threading.Thread(Render);
            RenderThread.Start(this.isNextRender);
            StartRender();
        }

        public Tao.Platform.Windows.SimpleOpenGlControl GlControl
        {
            get { return Program.mainForm.GlControl; }
        }

        public Camera Camera
        {
            get { return camera; }
            set 
            {
                if (value != null)
                {
                    StopRender();
                    camera = value;
                    StartRender();
                }
            }
        }

        public int MaxFPS
        {
            get { return maxFPS; }
            set { maxFPS = value > 0 && value < 1000 ? value : 60; }
        }

        
        void FPSUpdate(int newValue)
        {
            //TODO: разобраться, что это за херню мне предложила VS.
            EventFPSUpdate?.Invoke(newValue);
        }
        void ProcessingFPS(int renderElapsedTime) //TODO: херовое название, придумать получше.
        {
            FPSUpdate((int)(1000 / ((pauseMillisecond + renderElapsedTime) == 0 ? 1 : (pauseMillisecond + renderElapsedTime))));

            pauseMillisecond = (int)(1000 / MaxFPS - renderElapsedTime);
            pauseMillisecond = pauseMillisecond > 0 ? pauseMillisecond : 0;
        }

        public void StartRender()
        {
            this.isNextRender.Set();
        }
        public void StopRender()
        {
            this.isNextRender.Reset();
            Thread.Sleep(1);
        }

        public void SettingVisibleAreaSize()
        {
            Gl.glViewport(0, 0, this.GlControl.Width, this.GlControl.Height);
            // устанавливаем проекционную матрицу 
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            // очищаем ее 
            Gl.glLoadIdentity();

            // теперь необходимо корректно настроить 2D ортогональную проекцию 
            // в зависимости от того, какая сторона больше 
            // мы немного варьируем то, как будут сконфигурированы настройки проекции 
            if (this.GlControl.Width <= this.GlControl.Height)
                Glu.gluOrtho2D(0.0, camera.Width, 0.0, camera.Height * (float)this.GlControl.Height / (float)this.GlControl.Width);
            else
                Glu.gluOrtho2D(0.0, camera.Width * (float)this.GlControl.Width / (float)this.GlControl.Height, 0.0, camera.Height);

            // переходим к объектно-видовой матрице 
            Gl.glMatrixMode(Gl.GL_MODELVIEW);

            Camera.Look();
        }

        void Render(object state)
        {
            Stopwatch sw = new Stopwatch();
            ManualResetEvent nextRender = (ManualResetEvent)state;
            ManualResetEvent nextFrame = new ManualResetEvent(false);

            while (true)
            {
                nextRender.WaitOne();

                nextFrame.Reset();


                updateVoid del = delegate
                {
                    sw.Start();

                    DrawFrame();

                    sw.Stop();
                    ProcessingFPS((int)sw.ElapsedMilliseconds);
                    System.Windows.Forms.MessageBox.Show(sw.ElapsedTicks.ToString());
                    sw.Reset();

                    nextFrame.Set();
                };
                Program.mainForm.Invoke(del);


                Thread.Sleep(pauseMillisecond);
                nextFrame.WaitOne();
            }
        }

        void DrawFrame()
        {
            int zShift;

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            // Фоны
            zShift = 0;
            foreach (Background b in Program.L.GetMap<Background>().GetAllVO())
                if (Analytics.IsInCamera(b.C, this.Camera))
                    this.DrawObject(new Coord(b.C.X - Camera.MinX, b.C.Y - Camera.MinY, b.C.Z), b.texture, zShift);
            // Конец фонов


            // Блоки
            zShift += Program.L.LengthZ;
            foreach (Block b in Program.L.GetMap<Block>().GetAllVO())
                if (Analytics.IsInCamera(b.C, this.Camera))
                    this.DrawObject(new Coord(b.C.X - Camera.MinX, b.C.Y - Camera.MinY, b.C.Z), b.texture, zShift);
            // Конец блоков


            // Сущности
            zShift += Program.L.LengthZ;
            foreach (Being b in Program.L.GetMap<Being>().GetAllVO())
                if (b.isSpawned)
                    if (Analytics.IsInCamera(b.C, this.Camera))
                        this.DrawObject(new Coord(b.C.X - Camera.MinX, b.C.Y - Camera.MinY), b.texture, zShift);
            // Конец сущностей

            // Декали
            zShift += Program.L.LengthZ;
            foreach (Decal d in Program.L.GetMap<Decal>().GetAllVO())
                if (Analytics.IsInCamera(d.C, this.Camera))
                    this.DrawObject(new Coord(d.C.X - this.camera.MinX, d.C.Y - this.camera.MinY, d.C.Z), d.texture, zShift);
            // Конец декалей

            // Прицел
            zShift += Program.L.LengthZ;
            this.DrawObject(new Coord(camera.Sight.C.X - this.camera.MinX, camera.Sight.C.Y - this.camera.MinY), camera.Sight.AimDecal.texture, zShift);

            Program.mainForm.GlControl.SwapBuffers();
        }

        void DrawObject(Coord C, Texture texture, int zShift)
        {
            int size = 1;
            // включаем режим текстурирования
            Gl.glEnable(Gl.GL_TEXTURE_2D);

            // включаем режим текстурирования
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture.textureId);

            Gl.glBegin(Gl.GL_QUADS);

            Gl.glTexCoord2f(0.0f, 0.0f);
            Gl.glVertex3d((double)C.X, (double)C.Y, (double)(C.Z + zShift) / 1000);

            Gl.glTexCoord2f(0.0f, 0.0f + size);
            Gl.glVertex3d((double)C.X, (double)C.Y + size, (double)(C.Z + zShift) / 1000);

            Gl.glTexCoord2f(0.0f + size, 0.0f + size);
            Gl.glVertex3d((double)C.X + size, (double)C.Y + size, (double)(C.Z + zShift) / 1000);

            Gl.glTexCoord2f(0.0f + size, 0.0f);
            Gl.glVertex3d((double)C.X + size, (double)C.Y, (double)(C.Z + zShift) / 1000);

            Gl.glEnd();

            // отключаем режим текстурирования
            Gl.glDisable(Gl.GL_TEXTURE_2D);
        }

        /*public void DrawColor(Coord C, double colorA, double colorB, double colorC)
        {
            Gl.glColor3d(colorA, colorB, colorC);

            Gl.glBegin(Gl.GL_QUADS);

            Gl.glVertex2d((double)C.X, (double)C.Y);
            Gl.glVertex2d((double)C.X, (double)C.Y);
            Gl.glVertex2d((double)C.X, (double)C.Y);
            Gl.glVertex2d((double)C.X, (double)C.Y);

            Gl.glEnd();

            Gl.glColor3f(0, 222, 0);
            Gl.glRasterPos2f(0.4f + C.X, 0.3f + C.Y);
            //Glut.glutBitmapCharacter(Glut.GLUT_BITMAP_TIMES_ROMAN_10, '0');
        }*/
    }
}
