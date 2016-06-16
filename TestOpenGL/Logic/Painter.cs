using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.DevIl;

using TestOpenGL.VisualObjects;

namespace TestOpenGL.Logic
{
    /// <summary>
    /// Рисует игровое поля на переданной форме.
    /// </summary>
    class Painter
    {
        Camera camera;

        private Tao.Platform.Windows.SimpleOpenGlControl glControl;

        public Tao.Platform.Windows.SimpleOpenGlControl GlControl
        {
            get { return glControl; }
        }

        //TODO: отрефакторить эту хрень и метод Redraw.
        delegate void updateVoid();

        System.Threading.Thread RedrawDisplay;
        ManualResetEvent isNextRedraw = new ManualResetEvent(false);
        ManualResetEvent isTest = new ManualResetEvent(false);

        public Camera Camera
        {
            get { return camera; }
            set 
            {
                if (value != null)
                {
                    StopRedraw();
                    camera = value;
                    StartRedraw();
                }
            }
        }

        public Painter(int startingWidthCamera, int startingHeightCamera)
        {
            camera = new Camera(startingWidthCamera, startingHeightCamera);

        }

        public void SetGlControl(Tao.Platform.Windows.SimpleOpenGlControl GlControl)
        {
            this.glControl = GlControl;

            
            SettingVisibleAreaSize();

            RedrawDisplay = new System.Threading.Thread(Redraw);
            RedrawDisplay.Start(this.isNextRedraw);
        }

        public void StartRedraw()
        {
            this.isNextRedraw.Set();
        }
        public void StopRedraw()
        {
            this.isNextRedraw.Reset();
            //TODO: возможно, сюда надо вставит короткую поточную паузу, чтобы поток перерисовки успел остановиться.
        }

        // На самом деле я не до конца понимаю, как это работает. Но говорят, что это норма.

        /// <summary>
        /// Метод перерисовки изображения на экране
        /// </summary>
        /// <param name="state"></param>
        public void Redraw(object state)
        {
            ManualResetEvent MRE = (ManualResetEvent)state;
            while (true)
            {
                MRE.WaitOne();

                updateVoid del = delegate
                {
                    Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

                    for (int x = 0; x < this.camera.Width; x++)
                    {
                        for (int y = 0; y < this.camera.Height; y++)
                        {
                            if (Program.L.MapBackgrounds.GetBackground(new Coord(x + this.camera.MinX, y + this.camera.MinY)) != null)
                                this.DrawObject(new Coord(x, y), Program.L.MapBackgrounds.GetBackground(new Coord(x + this.camera.MinX, y + this.camera.MinY)).texture);
                        }
                    }

                    for (int x = 0; x < this.camera.Width; x++)
                    {
                        for (int y = 0; y < this.camera.Height; y++)
                        {
                            for (int i = 0; i < Program.L.LengthZ; i++)
                            {
                                if (Program.L.MapBlocks.GetBlock(new Coord(x + this.camera.MinX, y + this.camera.MinY, i)) != null)
                                    this.DrawObject(new Coord(x, y), Program.L.MapBlocks.GetBlock(new Coord(x + this.camera.MinX, y + this.camera.MinY, i)).texture);
                            }
                        }
                    }

                    foreach (Being B in Program.L.MapBeings.GetAllBeings())
                        if (B.isSpawned)
                            if (Analytics.IsInCamera(B.C, camera))
                            {
                                this.DrawObject(new Coord(B.C.X - this.camera.MinX, B.C.Y - this.camera.MinY), B.texture);
                                foreach(Item i in B.inventory.GetEquipmentItems())
                                {
                                    this.DrawObject(new Coord(B.C.X - this.camera.MinX, B.C.Y - this.camera.MinY), i.texture);
                                }
                            }

                    foreach (Decal d in Program.L.MapDecals.GetAllDecals())
                        if (Analytics.IsInCamera(d.C, camera))
                            this.DrawObject(new Coord(d.C.X - this.camera.MinX, d.C.Y - this.camera.MinY), d.texture);

                    this.DrawObject(new Coord(Program.GCycle.sight.AimCoord.X - this.camera.MinX, Program.GCycle.sight.AimCoord.Y - this.camera.MinY), Program.GCycle.sight.aimDecal.texture);

                    this.GlControl.SwapBuffers();
                };
                this.GlControl.Invoke(del);
                System.Threading.Thread.Sleep(16);
            }
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
        }

        /// <summary>
        /// Инициализация графической системы.
        /// </summary>

        private void DrawObject(Coord C, Texture texture)
        {
            int size = 1;
            // включаем режим текстурирования
            Gl.glEnable(Gl.GL_TEXTURE_2D);

            // включаем режим текстурирования
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture.textureId);

            Gl.glBegin(Gl.GL_QUADS);

            Gl.glTexCoord2f(0.0f, 0.0f);
            Gl.glVertex2d((double)C.X, (double)C.Y);

            Gl.glTexCoord2f(0.0f, 0.0f + size);
            Gl.glVertex2d((double)C.X, (double)C.Y + size);

            Gl.glTexCoord2f(0.0f + size, 0.0f + size);
            Gl.glVertex2d((double)C.X + size, (double)C.Y + size);

            Gl.glTexCoord2f(0.0f + size, 0.0f);
            Gl.glVertex2d((double)C.X + size, (double)C.Y);

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
