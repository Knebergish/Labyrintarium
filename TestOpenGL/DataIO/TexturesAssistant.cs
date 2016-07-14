using System;
using System.Data;

using Tao.OpenGl;
using Tao.DevIl;
using Tao.FreeGlut;

using TestOpenGL.VisualObjects;

namespace TestOpenGL.DataIO
{
    class TexturesAssistant
    {
        delegate int updateVoid(TypeVisualObject tvo, int imageId);

        // Таблица подгруженных текстур блоков.
        private DataTable texturesDataTable;
        private string[] pathes;
        private string path;
        //-------------


        private TexturesAssistant()
        {
            InitializeGraphics();

            texturesDataTable = new DataTable();
            this.texturesDataTable.Columns.Add("type", typeof(TypeVisualObject));
            this.texturesDataTable.Columns.Add("imageName", typeof(string));
            this.texturesDataTable.Columns.Add("textureId", System.Type.GetType("System.Int32"));
        }
        public TexturesAssistant(string path): this()
        {
            this.path = path;
            string[] namesTypes = Enum.GetNames(typeof(TypeVisualObject));
            pathes = new string[namesTypes.Length];

            for (int i = 0; i < namesTypes.Length; i++)
                pathes[i] = this.path + "\\Textures\\" + namesTypes[i] + "s\\";
        }
        //=============


        private void InitializeGraphics()
        {
            // Тупой копипаст из интернет-урока. Я в этом не разбираюсь, и вы смиритесь.

            

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

        private int LoadTextureFromFile(TypeVisualObject tvo, string imageName)//, int imageId)
        {
            string url = pathes[(int)tvo] + imageName + ".png";
            int image = 0;
            // создаем изображение с идентификатором imageId
            Il.ilGenImages(1, out image);
            // делаем изображение текущим
            Il.ilBindImage(image);

            // адрес изображения полученный с помощью окна выбра файла

            uint texObject = 0;
            if (Il.ilLoadImage(url))
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
                switch (tvo)
                {
                    case TypeVisualObject.Background:
                        Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
                        Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
                        break;

                    default:
                        Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_CLAMP_TO_EDGE);
                        Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_CLAMP_TO_EDGE);
                        break;
                }

                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);//
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
                Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_REPLACE);

                // создаем RGBA текстуру
                Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, width, height, 0, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, Il.ilGetData());

                // очищаем память
                Il.ilDeleteImages(1, ref image);

                // Удаление текстуры, когда дойдём до освобождения памяти.
                //if(texObject == 1)
                //    Gl.glDeleteTextures(1, ref texObject);
            }

            // возвращаем идентификатор текстурного объекта
            return (int)texObject;
        }

        public int LoadTexture(TypeVisualObject tvo, string imageName)
        {
            return (int)Program.mainForm.Invoke(
            new Func<int>(() => LoadTextureFromFile(tvo, imageName))
            );
        }

        private int SearchLoadedTexture(TypeVisualObject type, string imageName)
        {
            for (int i = 0; i < this.texturesDataTable.Rows.Count; i++)
            {
                if ((TypeVisualObject)this.texturesDataTable.Rows[i]["type"] == type)
                {
                    if (this.texturesDataTable.Rows[i]["imageName"].ToString() == imageName)
                    {
                        return (int)this.texturesDataTable.Rows[i]["textureId"];
                    }
                }
            }
            return -1;
        }
        private void SaveTexture(TypeVisualObject type, string imageName, int textureId)
        {
            this.texturesDataTable.Rows.Add(type, imageName, textureId);
        }


        public Texture GetTexture(TypeVisualObject tvo, string imageName)
        {
            int currentTextureId = SearchLoadedTexture(tvo, imageName);

            if (currentTextureId == -1)
            {
                currentTextureId = LoadTexture(tvo, imageName);
                SaveTexture(tvo, imageName, currentTextureId);
            }

            return new Texture() { textureId = currentTextureId };
        }
    }
}
