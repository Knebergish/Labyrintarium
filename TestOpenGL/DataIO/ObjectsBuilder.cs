using System.Collections.Generic;
using System.Data;
using System;

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
        //-------------


        private ObjectsBuilder() { }
        public ObjectsBuilder(DataBaseIO DBIO, TexturesAssistant TA)
        {
            this.DBIO = DBIO;
            this.TA = TA;
        }
        //=============

        public VisualObject GetVO<T>(int index) where T : VisualObject
        {
            switch(typeof(T).Name)
            {
                case "Background":
                    return GetBackground(index);

                case "Block":
                    return GetBlock(index);

                case "Decal":
                    return GetDecal(index);

                case "Bot":
                    return GetBot(index);

                case "Gamer":
                    return GetGamer(index);

                case "Item":
                    return GetItem(index);

                default:
                    return null;
            }
        }

        public Background GetBackground(int num)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM Backgrounds WHERE Backgrounds.id = " + num);
            return new Background(
                num,
                (string)dt.Rows[0]["name"],
                (string)dt.Rows[0]["description"],
                (bool)dt.Rows[0]["passableness"],
                TA.GetTexture(TypeVisualObject.Background, dt.Rows[0]["imageId"].ToString())
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
                TA.GetTexture(TypeVisualObject.Block, dt.Rows[0]["imageId"].ToString())
                );
        }

        public Decal GetDecal(int num)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM Decals WHERE Decals.id = " + num);
            return new Decal(
                num,
                (string)dt.Rows[0]["name"],
                (string)dt.Rows[0]["description"],
                TA.GetTexture(TypeVisualObject.Decal, dt.Rows[0]["imageId"].ToString())
                );
        }

        public Being GetGamer(int num)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM Beings WHERE Beings.id = " + num);
            return new Gamer(
                num,
                (string)dt.Rows[0]["name"],
                (string)dt.Rows[0]["description"],
                TA.GetTexture(TypeVisualObject.Being, dt.Rows[0]["imageId"].ToString()),
                0
                );
        }
        
        public Being GetBot(int num)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM Beings WHERE Beings.id = " + num);
            return new Bot(
                num,
                (string)dt.Rows[0]["name"],
                (string)dt.Rows[0]["description"],
                TA.GetTexture(TypeVisualObject.Being, dt.Rows[0]["imageId"].ToString()),
                0,
                null
                );
        }

        public Item GetItem(int num)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM Items WHERE Items.id = " + num);
            DataTable dt2 = DBIO.ExecuteSQL("SELECT * FROM Parts WHERE Parts.idItem = " + num);
            List<Part> lp = new List<Part>();
            for (int i = 0; i < dt2.Rows.Count; i++ )
                lp.Add((Part)int.Parse(dt2.Rows[i]["part"].ToString()));

            return new Item(
                num,
                (string)dt.Rows[0]["name"],
                (string)dt.Rows[0]["description"],
                TA.GetTexture(TypeVisualObject.Item, dt.Rows[0]["imageId"].ToString()),
                int.Parse(dt.Rows[0]["level"].ToString()),
                int.Parse(dt.Rows[0]["price"].ToString()),
                lp
                );
        }
    }
}
