using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Diagnostics;
using System.Linq;

using TestOpenGL.Logic;
using TestOpenGL.PhisicalObjects;


namespace TestOpenGL.Renders
{
    class Painter : IRenderManager
    {
        int maxFPS;
        int actualFPS;
        int pauseMillisecond;

        //List<IRenderable> listRenderObjects;

        Thread RenderThread;
        ManualResetEvent isNextRender;
        ManualResetEvent isStopRender;

        //TODO: вот как-то она тут не в тему, но куда её убрать?..
        //Camera camera;

        event ADelegate<int> changeActualFPSEvent;
        //-------------


        public Painter()
        {
            maxFPS = 60;
            actualFPS = 0;
            pauseMillisecond = 0;
            
            //listRenderObjects = new List<IRenderable>();

            RenderThread = new Thread(Render);
            RenderThread.Name = "RenderThread";

            isNextRender = new ManualResetEvent(false);
            isStopRender = new ManualResetEvent(true);

            RenderThread.Start();
            //StartRender();
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

        ManualResetEvent IRenderManager.IsStopRender
        { get { return isStopRender; } }
        //=============


        void IRenderManager.SetMaxFPS(int maxFPS)
        {
            this.maxFPS = maxFPS > 0 && maxFPS < 1000 ? maxFPS : 60;
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
            isStopRender.Reset();
            isNextRender.Set();
        }
        void StopRender()
        {
            isNextRender.Reset();
            isStopRender.Set();
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

                    DrawFrame(GlobalData.WorldData?.RendereableObjectsContainer.GetAllRendereableObjects()); 

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
            List<IRenderable> renderList = new List<IRenderable>(listRenderObject);
            //renderList.RemoveAll((IRenderable rend) => rend == null);

            List<Cell> cellsList = new List<Cell>();

            var sort1 = from obj in renderList
                        orderby obj.Coord.Y descending, obj.PartLayer ascending
                         select obj;

            GlobalData.LLL.ClearScreen();

            

            foreach (IRenderable ro in sort1)
            {
                cellsList.AddRange(ro?.GetCells());
            }




            for (int i = cellsList.Count - 1; i >= 0; i--)
                if (!Analytics.IsInCamera(cellsList[i].C, GlobalData.WorldData.Camera))
                    cellsList.RemoveAt(i);

            

            var sort = from cell in cellsList
                       orderby cell.GlobalDepth ascending, cell.C.Y descending
                       select cell;

            foreach (Cell cell in sort)
                DrawCell(cell);

            GlobalData.LLL.RedrawScreed();
        }

        void DrawCell(Cell cell)
        {
            int deltaX = GlobalData.WorldData.Camera?.MinX ?? 0;
            int deltaY = GlobalData.WorldData.Camera?.MinY ?? 0;

            GlobalData.LLL.DrawCell(cell.Texture, cell.C.X - deltaX, cell.C.Y - deltaY);
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
