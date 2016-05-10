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
        
        private ObjectsBuilder()
        {
        }
        public ObjectsBuilder(DataBaseIO DBIO, TexturesAssistant TA)
        {
            this.DBIO = DBIO;
            this.TA = TA;
        }

        public Attack GetAttack(int num)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM Attacks WHERE Attacks.id = " + num);
            Attack A = new Attack();
            A.id = num;
            A.texture = TA.GetTexture(TypeVisualObject.Decal, int.Parse(dt.Rows[0]["imageId"].ToString()));
            A.visualObjectInfo.name = (string)dt.Rows[0]["name"];
            A.visualObjectInfo.description = (string)dt.Rows[0]["description"];
            A.coefficient = (double)dt.Rows[0]["coefficient"];
            A.profilingFeature = (Feature)int.Parse(dt.Rows[0]["profilingFeature"].ToString()); ;
            A.minDistance = int.Parse(dt.Rows[0]["minDistance"].ToString());
            A.maxDistance = int.Parse(dt.Rows[0]["maxDistance"].ToString());
            A.timePause = int.Parse(dt.Rows[0]["timePause"].ToString());
            return A;
            
            /*return new Attack(
                num,
                TA.GetTexture(TypeVisualObject.Decal, int.Parse(dt.Rows[0]["imageId"].ToString())),
                (string)dt.Rows[0]["name"],
                (string)dt.Rows[0]["description"],
                (Feature)dt.Rows[0]["profilingFeature"],
                (double)dt.Rows[0]["coefficient"],
                (int)dt.Rows[0]["minDistance"],
                (int)dt.Rows[0]["maxDistance"],
                (int)dt.Rows[0]["timePause"]
                );*/
        }

        public Block GetBlock(int num)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM Blocks WHERE Blocks.id = " + num);
            Block B = new Block();
            B.id = num;
            B.visualObjectInfo.name = (string)dt.Rows[0]["name"];
            B.visualObjectInfo.description = (string)dt.Rows[0]["description"];
            B.passableness = (bool)dt.Rows[0]["passableness"];
            B.transparency = (bool)dt.Rows[0]["transparency"];
            B.permeability = (bool)dt.Rows[0]["permeability"];
            B.texture = TA.GetTexture(TypeVisualObject.Block, int.Parse(dt.Rows[0]["imageId"].ToString()));
            return B;
        }

        public Decal GetDecal(int num)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM Decals WHERE Decals.id = " + num);
            Decal D = new Decal();
            D.id = num;
            D.visualObjectInfo.name = (string)dt.Rows[0]["name"];
            D.visualObjectInfo.description = (string)dt.Rows[0]["description"];
            D.texture = TA.GetTexture(TypeVisualObject.Decal, int.Parse(dt.Rows[0]["imageId"].ToString()));
            return D;
        }

        private Being GetBeing(Being B, int num)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM Beings WHERE Beings.id = " + num);
            
            B.id = num;
            B.visualObjectInfo.name = (string)dt.Rows[0]["name"];
            B.visualObjectInfo.description = (string)dt.Rows[0]["description"];
            B.texture = TA.GetTexture(TypeVisualObject.Being, int.Parse(dt.Rows[0]["imageId"].ToString()));
            return B;
        }
        public Being GetGamer(int num)
        {
            Being B = new Gamer();
            return GetBeing(B, num);
        }
        public Being GetBot(int num)
        {
            Being B = new Bot();
            return GetBeing(B, num);
        }
    }
}
