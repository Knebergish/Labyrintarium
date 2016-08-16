using System;
using System.Data;

using Tao.OpenGl;
using Tao.DevIl;
using Tao.FreeGlut;

using TestOpenGL.Renders;

namespace TestOpenGL.DataIO
{
    class TexturesAssistant
    {
        delegate int updateVoid(Layer layer, int imageId);

        // Таблица подгруженных текстур блоков.
        private DataTable texturesDataTable;
        private string[] pathes;
        private string path;
        //-------------


        private TexturesAssistant()
        {
            //InitializeGraphics();

            texturesDataTable = new DataTable();
            texturesDataTable.Columns.Add("layer", typeof(Layer));
            texturesDataTable.Columns.Add("imageName", typeof(string));
            texturesDataTable.Columns.Add("textureId", Type.GetType("System.Int32"));
        }
        public TexturesAssistant(string path): this()
        {
            this.path = path;
            string[] namesLayers = Enum.GetNames(typeof(Layer));
            pathes = new string[namesLayers.Length];

            for (int i = 0; i < namesLayers.Length; i++)
                pathes[i] = this.path + "\\Textures\\" + namesLayers[i] + "s\\";
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


        private int LoadTexture(Layer layer, string imageName)
        {
            string path = pathes[(int)layer] + imageName + ".png";
            return (int)Program.MainThreadInvoke(
                //TODO: переделать весь класс на оперированием Texture, а не int.
                new Func<int>(() => { return GlobalData.LLL.LoadTextureFromFile(path).textureId; })
                );
        }

        private int SearchLoadedTexture(Layer layer, string imageName)
        {
            for (int i = 0; i < texturesDataTable.Rows.Count; i++)
            {
                if ((Layer)texturesDataTable.Rows[i]["layer"] == layer)
                {
                    if (texturesDataTable.Rows[i]["imageName"].ToString() == imageName)
                    {
                        return (int)texturesDataTable.Rows[i]["textureId"];
                    }
                }
            }
            return -1;
        }
        private void SaveTexture(Layer layer, string imageName, int textureId)
        {
            this.texturesDataTable.Rows.Add(layer, imageName, textureId);
        }


        public Texture GetTexture(Layer layer, string imageName)
        {
            int currentTextureId = SearchLoadedTexture(layer, imageName);

            if (currentTextureId == -1)
            {
                currentTextureId = LoadTexture(layer, imageName);
                SaveTexture(layer, imageName, currentTextureId);
            }

            return new Texture(currentTextureId);
        }
    }
}
