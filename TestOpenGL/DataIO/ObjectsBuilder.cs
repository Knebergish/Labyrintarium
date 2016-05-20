using System.Data;
using System.Data.SQLite;

using TestOpenGL.VisualObjects;
using TestOpenGL.DataIO;

namespace TestOpenGL.DataIO
{
    /// <summary>
    /// Класс для получения различных игровых объектов (блоков, сущностей...) из базы данных и подгрузки изображений к ним.
    /// </summary>
    class ObjectsBuilder
    {
        private DataBaseIO DBIO;
        private TexturesAssistant TA;
        
        private ObjectsBuilder() { }
        public ObjectsBuilder(DataBaseIO DBIO, TexturesAssistant TA)
        {
            this.DBIO = DBIO;
            this.TA = TA;
        }

        public Attack GetAttack(int num)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM Attacks WHERE Attacks.id = " + num);
            return new Attack(
                num,
                (string)dt.Rows[0]["name"],
                (string)dt.Rows[0]["description"],
                TA.GetTexture(TypeVisualObject.Decal, int.Parse(dt.Rows[0]["imageId"].ToString())),
                (Feature)int.Parse(dt.Rows[0]["profilingFeature"].ToString()),
                (double)dt.Rows[0]["coefficient"],
                int.Parse(dt.Rows[0]["minDistance"].ToString()),
                int.Parse(dt.Rows[0]["maxDistance"].ToString()),
                int.Parse(dt.Rows[0]["timePause"].ToString())
                );
        }

        public Block GetBlock(int num)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM Blocks WHERE Blocks.id = " + num);
            return new Block(
                num,
                (string)dt.Rows[0]["name"],
                (string)dt.Rows[0]["description"],
                (bool)dt.Rows[0]["passableness"],
                (bool)dt.Rows[0]["transparency"],
                (bool)dt.Rows[0]["permeability"],
                TA.GetTexture(TypeVisualObject.Block, int.Parse(dt.Rows[0]["imageId"].ToString()))
                );
        }

        public Decal GetDecal(int num)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM Decals WHERE Decals.id = " + num);
            return new Decal(
                num,
                (string)dt.Rows[0]["name"],
                (string)dt.Rows[0]["description"],
                TA.GetTexture(TypeVisualObject.Decal, int.Parse(dt.Rows[0]["imageId"].ToString()))
                );
        }
        public Decal GetDecal(int num, Coord C)
        {
            Decal D = GetDecal(num);
            D.C = C;
            return D;
        }

        public Being GetGamer(int num)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM Beings WHERE Beings.id = " + num);
            return new Gamer(
                num,
                (string)dt.Rows[0]["name"],
                (string)dt.Rows[0]["description"],
                TA.GetTexture(TypeVisualObject.Being, int.Parse(dt.Rows[0]["imageId"].ToString()))
                );
        }
        public Being GetBot(int num)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM Beings WHERE Beings.id = " + num);
            return new Bot(
                num,
                (string)dt.Rows[0]["name"],
                (string)dt.Rows[0]["description"],
                TA.GetTexture(TypeVisualObject.Being, int.Parse(dt.Rows[0]["imageId"].ToString()))
                );
        }
    }
}
