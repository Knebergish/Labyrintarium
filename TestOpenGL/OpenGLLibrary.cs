using Tao.OpenGl;
using Tao.DevIl;
using Tao.FreeGlut;
using Tao.Platform.Windows;

using TestOpenGL.Renders;


namespace TestOpenGL
{
    class OpenGLLibrary : ILowLevelLibraryble
    {
        SimpleOpenGlControl glControl;
        //-------------


        public OpenGLLibrary()
        {
            glControl = new SimpleOpenGlControl();
            glControl.SizeChanged += (sender, e) => 
            {
                GlobalData.LLL.SettingVisibleAreaSize();
            };
            glControl.InitializeContexts();

            Initialize();
        }

        public SimpleOpenGlControl GlControl
        { get { return glControl; } }
        //=============


        void Initialize()
        {
            // инициализация библиотеки GLUT 
            Glut.glutInit();

            // инициализация режима окна 
            Glut.glutInitDisplayMode(Glut.GLUT_RGBA | Glut.GLUT_DOUBLE);

            // инициализация библиотеки openIL
            Il.ilInit();
            Il.ilEnable(Il.IL_ORIGIN_SET);

            // устанавливаем цвет очистки окна
            Gl.glClearColor(255, 255, 255, 1);

            //Включение поддержки прозрачности текстур
            Gl.glEnable(Gl.GL_ALPHA_TEST);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glColor4f(1f, 1f, 1f, 0.75f);

            //Gl.glEnable(Gl.GL_DEPTH_TEST);

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
        }

        Texture ILowLevelLibraryble.LoadTextureFromFile(string path)
        {
            int image = 0;
            // создаем изображение с идентификатором imageId
            Il.ilGenImages(1, out image);
            // делаем изображение текущим
            Il.ilBindImage(image);

            uint texObject = 0;
            if (Il.ilLoadImage(path))
            {
                // сохраняем размеры изображения
                int width = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
                int height = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);

                // определяем число бит на пиксель
                int bitspp = Il.ilGetInteger(Il.IL_IMAGE_BITS_PER_PIXEL);

                // генерируем текстурный объект
                Gl.glGenTextures(1, out texObject);

                // устанавливаем режим упаковки пикселей
                Gl.glPixelStorei(Gl.GL_UNPACK_ALIGNMENT, 1);

                // создаем привязку к только что созданной текстуре
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, texObject);


                // устанавливаем режим фильтрации и повторения текстуры
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_NEAREST);//Gl.GL_NEAREST
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_NEAREST);//Gl.GL_LINEAR

                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_CLAMP_TO_EDGE);
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_CLAMP_TO_EDGE);

                Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_REPLACE);

                // создаем RGBA текстуру
                Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, width, height, 0, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, Il.ilGetData());

                // очищаем память
                Il.ilDeleteImages(1, ref image);

                // Удаление текстуры, когда дойдём до освобождения памяти.
                //if(texObject == 1)
                //    Gl.glDeleteTextures(1, ref texObject);
            }
            else
                System.Windows.Forms.MessageBox.Show("Ошибка загрузки изображения по пути: " + path);

            // возвращаем идентификатор текстурного объекта
            return new Texture((int)texObject);
        }

        void ILowLevelLibraryble.DrawCell(Texture texture, int xCoord, int yCoord)
        {
            int size = 1;
            // включаем режим текстурирования
            Gl.glEnable(Gl.GL_TEXTURE_2D);

            // включаем режим текстурирования
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture.textureId);

            Gl.glBegin(Gl.GL_QUADS);

            Gl.glTexCoord2d(0.0, 0.0);
            Gl.glVertex2d((double)xCoord, (double)yCoord);

            Gl.glTexCoord2d(0.0, 0.0 + size);
            Gl.glVertex2d((double)xCoord, (double)yCoord + size);

            Gl.glTexCoord2d(0.0 + size, 0.0 + size);
            Gl.glVertex2d((double)xCoord + size, (double)yCoord + size);

            Gl.glTexCoord2d(0.0 + size, 0.0);
            Gl.glVertex2d((double)xCoord + size, (double)yCoord);

            Gl.glEnd();

            // отключаем режим текстурирования
            Gl.glDisable(Gl.GL_TEXTURE_2D);
        }

        //TODO: Достаточно неявно обращается к GlobalData.WorldData.Camera.Width/Height. Формализовать?
        void SettingVisibleAreaSize()//(double width, double height)
        {
            Gl.glViewport(0, 0, glControl.Width, glControl.Height);
            // устанавливаем проекционную матрицу 
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            // очищаем ее 
            Gl.glLoadIdentity();

            Glu.gluOrtho2D(
                0.0, 
                GlobalData.WorldData?.Camera?.Width ?? 10, 
                0.0,
                GlobalData.WorldData?.Camera?.Height ?? 10);

            // теперь необходимо корректно настроить 2D ортогональную проекцию 
            // в зависимости от того, какая сторона больше 
            // мы немного варьируем то, как будут сконфигурированы настройки проекции 
            /*if (GlControl.Width <= GlControl.Height)
                Glu.gluOrtho2D(0.0, cW, 0.0, cH * (float)GlControl.Height / (float)GlControl.Width);
            else
                Glu.gluOrtho2D(0.0, cW * (float)GlControl.Width / (float)GlControl.Height, 0.0, cH);*/

            // переходим к объектно-видовой матрице 
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
        }
        void ILowLevelLibraryble.SettingVisibleAreaSize()//(double width, double height)
        {
            SettingVisibleAreaSize();
        }

        void ILowLevelLibraryble.ClearScreen()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
        }

        void ILowLevelLibraryble.RedrawScreed()
        {
            glControl.SwapBuffers();
        }
    }
}
