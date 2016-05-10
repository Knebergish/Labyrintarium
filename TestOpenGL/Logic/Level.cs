using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

using Tao.OpenGl;
using Tao.FreeGlut;
//using Tao.Platform.Windows;
//using Tao.Sdl;
using Tao.DevIl;

using TestOpenGL;
using TestOpenGL.VisualObjects;

namespace TestOpenGL.Logic
{
    /// <summary>
    /// Самый главный и самый кривой игровой класс, отвечающий за игровой мир.
    /// </summary>
    class Level
    {
        // Максимальные измерения карты (LengthZ для глубины карты блоков)
        private int lengthX, lengthY, lengthZ;

        public Camera camera;

        // Карта блоков на уровне
        private Block[,,] mapLevel;
        // Список сущностей на карте
        private List<Being> listBeing;

        // Список эффектов/атак и прочей хрени
        private List<Decal> listDecals;

        private Form1 mainForm;

        //TODO: отрефакторить эту хрень и метод Redraw.
        delegate void updateVoid();

        
        // Поток перерисоки дисплея.
        System.Threading.Thread RedrawDisplay;
        ManualResetEvent isNextRedraw = new ManualResetEvent(false);
        ManualResetEvent isTest = new ManualResetEvent(false);
        // Ненавижу потоки. Всем сердцем.


        //..... поток паузы. Я ненавижу себя. И потоки. И не знаю, что ненавижу больше.
        System.Threading.Thread ThreadPause;
        int ms;
        bool flag;
        /////////////////////////////////////////////////////////////////////////

        public void Pause(int milliseconds)
        {
            ms = milliseconds;
            flag = true;
            isTest.Set();
            while (flag);
        }
        private void TPause()
        {
            while(true)
            {
                isTest.WaitOne();
                System.Threading.Thread.Sleep(ms);
                ms = 0;
                flag = false;
                isTest.Reset();
            }
        }

        /// <summary>
        /// Инициализация класса работы с игровой картой
        /// </summary>
        /// <param name="LengthX"> Ширина игровой карты.</param>
        /// <param name="LengthY"> Выстока игровой карты.</param>
        /// <param name="LengthZ"> Глубина игровой карты.</param>
        /// <param name="F"> Ссылка на визуальную форму с компонентом вывода изображения.</param>
        public Level(int LengthX, int LengthY, int LengthZ, Form1 F)
        {
            

            this.lengthX = LengthX;
            this.lengthY = LengthY;
            this.lengthZ = LengthZ;

            this.mainForm = F;

            mapLevel = new Block[this.lengthX, this.lengthY, this.lengthZ];
            listBeing = new List<Being>();
            listDecals = new List<Decal>();

            camera = new Camera(this, 20, 20);

            InitializeGraphics();

            

            RedrawDisplay = new System.Threading.Thread(Redraw);
            ThreadPause = new Thread(TPause);

            RedrawDisplay.Start(this.isNextRedraw);
            ThreadPause.Start();
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

        public int LengthX
        { get { return lengthX; } }
        public int LengthY
        { get { return lengthY; } }
        public int LengthZ
        { get { return lengthZ; } }


        public void StartRedraw()
        {
            this.isNextRedraw.Set();
        }
        public void StopRedraw()
        {
            this.isNextRedraw.Reset();
        }


        

        //TODO: Хрень, мне это не нравится. Подумать над переделкой системы декалирования.
        public void AddDecal(Decal d)
        {
            listDecals.Add(d);
        }
        public void AddDecals(VisualObjectStructure<Decal> decalStructure)
        {
            Decal d;
            while (decalStructure.Count != 0)
            {
                d = decalStructure.PopObject();
                d.C = decalStructure.PopCoord();
                listDecals.Add(d);
            }
        }
        public int CountDecals
        {
            get { return listDecals.Count; }
        }
        public void ClearDecals()
        {
            listDecals.Clear();
        }


        /// <summary>
        /// Возвращает объект блока из указанной ячейки и слоя карты блоков.
        /// </summary>
        /// <param name="x">Первая координата блока.</param>
        /// <param name="y">Вторая координата блока.</param>
        /// <param name="z">Глубина блока.</param>
        /// <returns>Объект блока, если по указанным координатам блок есть, и null, если блока нету или координаты некорректы.</returns>
        /// 
        public Block GetBlock(Coord C)
        {
            return this.mapLevel[C.X, C.Y, C.Z];
        }

        /// <summary>
        /// Установка одного блока на карту с перерисовкой карты.
        /// </summary>
        public void SetBlock(Block B, Coord C)
        {
            this.mapLevel[C.X, C.Y, C.Z] = B;
        }
        /// <summary>
        /// Установка блоков на карту из буферной структуры с перерисовкой карты в конце.
        /// </summary>
        /// <param name="BS">Буферная структура, содержащая блоки и координаты их установки.</param>
        public void SetBlocks(VisualObjectStructure<Block> blocksStructure)
        {
            Coord C;

            while (blocksStructure.Count > 0)
            {
                C = blocksStructure.PopCoord();

                this.mapLevel[C.X, C.Y, C.Z] = null;
                this.mapLevel[C.X, C.Y, C.Z] = blocksStructure.PopObject();
            }
        }

        //TODO: Даже не знаю, должны ли быть эти методы тут, а если и должны, то в таком ли виде?
        /// <summary>
        /// Поиск сущности по переданным координатам в массиве сущностей (возвращает только живых)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Being GetBeing(Coord C)
        {
            for (int i = 0; i < listBeing.Count; i++)
                if ((listBeing[i].C.X == C.X && listBeing[i].C.Y == C.Y) && listBeing[i].isSpawned)
                    return listBeing[i];
            return null;
        }
        public Being GetBeing(int num)
        {
            if (num >= 0 && num < this.listBeing.Count)
                return this.listBeing[num];
            return null;
        }
        /// <summary>
        /// Добавление сущности в очередь.
        /// </summary>
        /// <param name="B"> Объект сущности (обязательны корректные координаты в ней)</param>
        public bool AddBeing(Being B)
        {
            if (this.GetBeing(B.C) == null)
            {
                this.listBeing.Add(B);
                return true;
            }
            else return false;
        }
        public void RemoveBeing(Coord C)
        {
            this.listBeing.Remove(this.GetBeing(C));
        }
        public int CountBeings
        {
            get { return this.listBeing.Count; }
        }
        public void ClearDeadBeings()
        {
            for (int i = listBeing.Count - 1; i >= 0; i--)
            {
                if (!listBeing[i].isSpawned)
                    listBeing.RemoveAt(i);
            }
        }

        //TODO: Флаги в параметрах - зло. Переделать. Возможно, стоит создать новое перечисление.
        /// <summary>
        /// Проверяет ячейку на проходимость для сущностей.
        /// </summary>
        /// <param name="C"> Объект с координатами проверяемой ячейки.</param>
        /// <param name="type"> true - учитываются сущности, false - не учитываются. </param>
        /// <returns></returns>
        public bool IsPassable(Coord C, bool type)
        {
            for (int i = 0; i < this.lengthZ; i++)
            {
                if (this.GetBlock(new Coord(C.X, C.Y, i)) != null)
                    if (!this.GetBlock(new Coord(C.X, C.Y, i)).passableness)
                        return false;
            }

            if (type)
                if (this.GetBeing(C) != null)
                    return false;

            return true;
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
                            for (int i = 0; i < this.lengthZ; i++)
                            {
                                if (this.GetBlock(new Coord(x + this.camera.ShiftX, y + this.camera.ShiftY, i)) != null)
                                    this.DrawObject(new Coord(x, y), this.GetBlock(new Coord(x + this.camera.ShiftX, y + this.camera.ShiftY, i)).texture);//this.GetBlock(new Coord(x + this.camera.ShiftX, y + this.camera.ShiftY, i.texture)));
                            }
                        }
                    }

                    foreach (Being B in listBeing)
                        if (B.isSpawned)
                            if(Analytics.IsInCamera(B.C, camera))
                                this.DrawObject(new Coord (B.C.X - this.camera.ShiftX,  B.C.Y - this.camera.ShiftY ), B.texture);
                    
                    foreach(Decal d in listDecals)
                        if (Analytics.IsInCamera(d.C, camera))//if (Math.Abs(this.camera.ShiftX - d.C.X) < this.camera.Width && Math.Abs(this.camera.ShiftY - d.C.Y) < this.camera.Height)
                            this.DrawObject(new Coord ( d.C.X - this.camera.ShiftX, d.C.Y - this.camera.ShiftY), d.texture);
                    
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
                //await Task.Delay(16);
            }
        }

        /////////////////////////////////////////Приватные/////////////////////////////////////
        public void DrawColor(Coord C, double colorA, double colorB, double colorC)
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
    }


}

