using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Diagnostics;
using System.Linq;

using Tao.OpenGl;

using TestOpenGL.Logic;
using Tao.Platform.Windows;

namespace TestOpenGL.Renders
{
    class Painter : IRenderManager
    {
        SimpleOpenGlControl glControl;
        List<IRenderable> listRenderObjects;

        int maxFPS;
        int actualFPS;
        int pauseMillisecond;

        Thread RenderThread;
        ManualResetEvent isNextRender = new ManualResetEvent(false);

        //TODO: вот как-то она тут не в тему, но куда её убрать?..
        Camera camera;

        event ADelegate<int> changeActualFPSEvent;
        //-------------


        public Painter(SimpleOpenGlControl glControl)
        {
            maxFPS = 60;
            pauseMillisecond = 0;
            
            listRenderObjects = new List<IRenderable>();

            this.glControl = glControl;
            this.glControl.SizeChanged += (object sender, EventArgs e) => SettingVisibleAreaSize();

            SettingVisibleAreaSize();

            RenderThread = new Thread(Render);
            RenderThread.Name = "RenderThread";
            RenderThread.Start();
            StartRender();
        }

        event ADelegate<int> IRenderManager.ChangeActualFPSEvent
        {
            add { changeActualFPSEvent += value; }
            remove { changeActualFPSEvent -= value; }
        }

        int IRenderManager.MaxFPS
        { get { return maxFPS; } }
        int IRenderManager.ActualFPS
        { get { return ActualFPS; } }

        int ActualFPS
        {
            get { return actualFPS; }
            set
            {
                actualFPS = value;
                changeActualFPSEvent?.Invoke(actualFPS);
            }
        }
        //=============


        public void SetGLControl(SimpleOpenGlControl glControl)
        {
            this.glControl = glControl;
            this.glControl.SizeChanged += (object sender, EventArgs e) => SettingVisibleAreaSize();
        }

        private void GlControl_SizeChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void IRenderManager.SetCamera(Camera camera)
        {
            StopRender();
            this.camera = camera;
            this.camera.ChangeSizeEvent += SettingVisibleAreaSize;
            StartRender();
        }
        void IRenderManager.SetMaxFPS(int maxFPS)
        {
            this.maxFPS = maxFPS > 0 && maxFPS < 1000 ? maxFPS : 60;
        }

        void IRenderManager.AddRenderObject(IRenderable renderObject)
        {
            listRenderObjects.Add(renderObject);
        }
        void IRenderManager.RemoveRenderObject(IRenderable renderObject)
        {
            listRenderObjects.Remove(renderObject);
        }

        void IRenderManager.StartRender()
        {
            StartRender();
        }
        void IRenderManager.StopRender()
        {
            StopRender();
        }

        void StartRender()
        {
            isNextRender.Set();
        }
        void StopRender()
        {
            isNextRender.Reset();
            Thread.Sleep(1);
        }

        void CalculateActualFPS(int renderElapsedTime) //TODO: херовое название, придумать получше.
        {
            ActualFPS = (int)(1000 / ((pauseMillisecond + renderElapsedTime) == 0 ? 1 : (pauseMillisecond + renderElapsedTime)));

            pauseMillisecond = (int)(1000 / maxFPS - renderElapsedTime);
            pauseMillisecond = pauseMillisecond > 0 ? pauseMillisecond : 0;
        }

        void Render()
        {
            Stopwatch sw = new Stopwatch();

            while (true)
            {
                isNextRender.WaitOne();
                isNextRender.Reset();

                Action del = delegate
                {
                    sw.Start();

                    DrawFrame(listRenderObjects); 

                    sw.Stop();
                    CalculateActualFPS((int)sw.ElapsedMilliseconds);
                    sw.Reset();

                    isNextRender.Set();
                };
                Program.MainThreadInvoke(del);

                Thread.Sleep(pauseMillisecond);
            }
        }
        void DrawFrame(List<IRenderable> listRenderObject)
        {
            List<IRenderable> lGO = new List<IRenderable>();
            List<Cell> lC = new List<Cell>();

            lGO.AddRange(listRenderObject); //TODO: Проверить, может и без этого норм.

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);


            foreach(IRenderable ro in lGO)
            {
                lC.AddRange(ro.GetCells());
            }

            // Поддержка шейдеров
            //foreach (Func<List<Cell>> func in shadersList)
            //    lC.AddRange(func());

            for (int i = lC.Count - 1; i >= 0; i--)
                if (!Analytics.IsInCamera(lC[i].C, camera))
                    lC.RemoveAt(i);

            var sort = from cell in lC
                   orderby cell.GlobalDepth
                   select cell;

            foreach (Cell cell in sort)
                DrawCell(cell);

            

            // Прицел
            // DrawCell(camera.Sight.AimDecal.GraphicsObject.GetCells()[0]);

            Program.mainForm.GlControl.SwapBuffers();
        }
        void DrawCell(Cell cell)
        {
            int deltaX = camera?.MinX ?? 0;
            int deltaY = camera?.MinY ?? 0;

            int size = 1;
            // включаем режим текстурирования
            Gl.glEnable(Gl.GL_TEXTURE_2D);

            // включаем режим текстурирования
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, cell.Texture.textureId);

            Gl.glBegin(Gl.GL_QUADS);

            Gl.glTexCoord2d(0.0, 0.0);
            Gl.glVertex2d((double)cell.C.X - deltaX, (double)cell.C.Y - deltaY);

            Gl.glTexCoord2d(0.0, 0.0 + size);
            Gl.glVertex2d((double)cell.C.X - deltaX, (double)cell.C.Y + size - deltaY);

            Gl.glTexCoord2d(0.0 + size, 0.0 + size);
            Gl.glVertex2d((double)cell.C.X + size - deltaX, (double)cell.C.Y + size - deltaY);

            Gl.glTexCoord2d(0.0 + size, 0.0);
            Gl.glVertex2d((double)cell.C.X + size - deltaX, (double)cell.C.Y - deltaY);

            Gl.glEnd();

            // отключаем режим текстурирования
            Gl.glDisable(Gl.GL_TEXTURE_2D);
        }

        void SettingVisibleAreaSize()
        {
            int cW = camera?.Width ?? 10;
            int cH = camera?.Height ?? 10;

            Gl.glViewport(0, 0, glControl.Width, glControl.Height);
            // устанавливаем проекционную матрицу 
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            // очищаем ее 
            Gl.glLoadIdentity();

            Glu.gluOrtho2D(0.0, cW, 0.0, cH);

            // теперь необходимо корректно настроить 2D ортогональную проекцию 
            // в зависимости от того, какая сторона больше 
            // мы немного варьируем то, как будут сконфигурированы настройки проекции 
            /*if (GlControl.Width <= GlControl.Height)
                Glu.gluOrtho2D(0.0, cW, 0.0, cH * (float)GlControl.Height / (float)GlControl.Width);
            else
                Glu.gluOrtho2D(0.0, cW * (float)GlControl.Width / (float)GlControl.Height, 0.0, cH);*/

            // переходим к объектно-видовой матрице 
            Gl.glMatrixMode(Gl.GL_MODELVIEW);

            camera?.Look();
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
