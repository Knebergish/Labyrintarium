using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.DevIl;

using TestOpenGL.VisualObjects;

namespace TestOpenGL.Logic
{
    class Painter
    {
        public Camera camera;
        private Form1 mainForm;

        //TODO: отрефакторить эту хрень и метод Redraw.
        delegate void updateVoid();

        // Поток перерисоки дисплея.
        System.Threading.Thread RedrawDisplay;
        ManualResetEvent isNextRedraw = new ManualResetEvent(false);
        ManualResetEvent isTest = new ManualResetEvent(false);


        public Painter(Form1 F)
        {
            this.mainForm = F;
            camera = new Camera(20, 20);

            InitializeGraphics();

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
                            for (int i = 0; i < Program.L.LengthZ; i++)
                            {
                                if (Program.L.GetBlock(new Coord(x + this.camera.ShiftX, y + this.camera.ShiftY, i)) != null)
                                    this.DrawObject(new Coord(x, y), Program.L.GetBlock(new Coord(x + this.camera.ShiftX, y + this.camera.ShiftY, i)).texture);//this.GetBlock(new Coord(x + this.camera.ShiftX, y + this.camera.ShiftY, i.texture)));
                            }
                        }
                    }

                    foreach (Being B in Program.L.GetAllBeings())
                        if (B.isSpawned)
                            if (Analytics.IsInCamera(B.C, camera))
                                this.DrawObject(new Coord(B.C.X - this.camera.ShiftX, B.C.Y - this.camera.ShiftY), B.texture);

                    foreach (Decal d in Program.L.GetAllDecals())
                        if (Analytics.IsInCamera(d.C, camera))//if (Math.Abs(this.camera.ShiftX - d.C.X) < this.camera.Width && Math.Abs(this.camera.ShiftY - d.C.Y) < this.camera.Height)
                            this.DrawObject(new Coord(d.C.X - this.camera.ShiftX, d.C.Y - this.camera.ShiftY), d.texture);

                    this.DrawObject(new Coord(Program.GCycle.sight.AimCoord.X - this.camera.ShiftX, Program.GCycle.sight.AimCoord.Y - this.camera.ShiftY), Program.GCycle.sight.aimDecal.texture);

                    this.mainForm.AnT.SwapBuffers();
                };
                if (this.mainForm.InvokeRequired)
                    this.mainForm.Invoke(del);
                else
                {
                    del();
                }

                System.Threading.Thread.Sleep(16);
            }
        }

        /// <summary>
        /// Инициализация графической системы.
        /// </summary>
        private void InitializeGraphics()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            // инициализация библиотеки GLUT 
            Glut.glutInit();

            // инициализация режима окна 
            Glut.glutInitDisplayMode(Glut.GLUT_RGBA | Glut.GLUT_DOUBLE);

            // инициализация библиотеки openIL
            Il.ilInit();
            Il.ilEnable(Il.IL_ORIGIN_SET);

            // устанавливаем цвет очистки окна 
            Gl.glClearColor(255, 255, 255, 1);

            // устанавливаем порт вывода, основываясь на размерах элемента управления AnT 
            Gl.glViewport(0, 0, this.mainForm.AnT.Width, this.mainForm.AnT.Height);

            // устанавливаем проекционную матрицу 
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            // очищаем ее 
            Gl.glLoadIdentity();



            // теперь необходимо корректно настроить 2D ортогональную проекцию 
            // в зависимости от того, какая сторона больше 
            // мы немного варьируем то, как будут сконфигурированы настройки проекции 
            if (this.mainForm.AnT.Width <= this.mainForm.AnT.Height)
                Glu.gluOrtho2D(0.0, camera.Width, 0.0, camera.Height * (float)this.mainForm.AnT.Height / (float)this.mainForm.AnT.Width);
            else
                Glu.gluOrtho2D(0.0, camera.Width * (float)this.mainForm.AnT.Width / (float)this.mainForm.AnT.Height, 0.0, camera.Height);

            // переходим к объектно-видовой матрице 
            Gl.glMatrixMode(Gl.GL_MODELVIEW);

            ////////////////////////////////////////////////////////////////////////////////////////
            //Включение поддержки прозрачности текстур
            Gl.glEnable(Gl.GL_ALPHA_TEST);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glColor4f(1f, 1f, 1f, 0.75f);
            ////////////////////////////////////////////////////////////////////////////////////////

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
        }

        private void DrawObject(Coord C, Texture texture)
        {
            int size = 1;
            // включаем режим текстурирования
            Gl.glEnable(Gl.GL_TEXTURE_2D);

            // включаем режим текстурирования
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture.textureId);

            Gl.glBegin(Gl.GL_QUADS);

            Gl.glTexCoord2f(0.0f + C.X, 0.0f + C.Y);
            Gl.glVertex2d((double)C.X, (double)C.Y);

            Gl.glTexCoord2f(0.0f + C.X, 0.0f + C.Y + size);
            Gl.glVertex2d((double)C.X, (double)C.Y + size);

            Gl.glTexCoord2f(0.0f + C.X + size, 0.0f + C.Y + size);
            Gl.glVertex2d((double)C.X + size, (double)C.Y + size);

            Gl.glTexCoord2f(0.0f + C.X + size, 0.0f + C.Y);
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
