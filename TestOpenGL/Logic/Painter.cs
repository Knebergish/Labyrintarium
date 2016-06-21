using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

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

        delegate void updateVoid();

        System.Threading.Thread RedrawDisplay;
        ManualResetEvent isNextRedraw = new ManualResetEvent(false);
        ManualResetEvent isTest = new ManualResetEvent(false);

        public Tao.Platform.Windows.SimpleOpenGlControl GlControl
        {
            get { return Program.mainForm.AnT; }
        }
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

        public Painter(Camera camera)
        {
            this.camera = camera;

            SettingVisibleAreaSize();
            RedrawDisplay = new System.Threading.Thread(Redraw);
            RedrawDisplay.Start(this.isNextRedraw);
            StartRedraw();
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
            Stopwatch sw = new Stopwatch();
            ManualResetEvent MRE = (ManualResetEvent)state;
            int zShift;
            int millisecond = 0;
            int pauseMillisecond = 0;
            int FPS = 60;
            while (true)
            {
                zShift = 0;
                MRE.WaitOne();

                updateVoid del = delegate
                {
                    sw.Start();
                    Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

                    // Фоны
                    zShift = 0;
                    foreach (Background b in Program.L.GetMap<Background>().GetAllVO())
                        if (Analytics.IsInCamera(b.C, this.Camera))
                            this.DrawObject(new Coord(b.C.X - Camera.MinX, b.C.Y - Camera.MinY, b.C.Z), b.texture, zShift);
                    // Конец фонов


                    // Блоки
                    zShift += Program.L.LengthZ;
                    foreach(Block b in Program.L.GetMap<Block>().GetAllVO())
                        if(Analytics.IsInCamera(b.C, this.Camera))
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

                    Program.mainForm.AnT.SwapBuffers();
                    sw.Stop();
                    millisecond = (int)sw.ElapsedMilliseconds;
                    Program.mainForm.SetFPS((int)(1000 / ((pauseMillisecond + millisecond) == 0 ? 1 : (pauseMillisecond + millisecond))));
                    sw.Reset();
                };
                Program.mainForm.Invoke(del);
                pauseMillisecond = (int)(1000 / FPS - millisecond);
                pauseMillisecond = pauseMillisecond > 0 ? pauseMillisecond : 0;
                System.Threading.Thread.Sleep(pauseMillisecond);
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

            Camera.Look();
        }

        private void DrawObject(Coord C, Texture texture, int zShift)
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
