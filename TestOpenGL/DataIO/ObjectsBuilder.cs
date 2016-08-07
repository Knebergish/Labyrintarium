using System;
using System.Collections.Generic;
using System.Data;

using TestOpenGL.Renders;
using TestOpenGL.PhisicalObjects;
using TestOpenGL.PhisicalObjects.ChieldsItem;
using TestOpenGL.Logic;


namespace TestOpenGL.DataIO
{
    /// <summary>
    /// Класс для получения различных игровых объектов (блоков, сущностей...) из базы данных и подгрузки изображений к ним.
    /// </summary>
    class ObjectsBuilder
    {
        private DataBaseIO DBIO;
        private TexturesAssistant TA;
        Func<GraphicObject, ObjectInfo, int, Section, List<Section>, IStateble, int, int, int, int, Weapon> constructWeaponFunc;
        
        //-------------


        private ObjectsBuilder() { }
        public ObjectsBuilder(DataBaseIO DBIO, TexturesAssistant TA)
        {
            this.DBIO = DBIO;
            this.TA = TA;
        }
        //=============

        public GraphicObject GetGraphicObject(int num, Layer layer)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM Cells WHERE Cells.idGraphicObject = " + num);
            GraphicObject go = new GraphicObject(layer);

            for(int i = 0; i < dt.Rows.Count; i++)
                go.AddCell(
                    new UnsafeCoord(
                        int.Parse(dt.Rows[i]["deltaX"].ToString()), 
                        int.Parse(dt.Rows[i]["deltaY"].ToString())), 
                    int.Parse(dt.Rows[i]["deltaLayer"].ToString()), 
                    (ModifyDepth)int.Parse(dt.Rows[i]["modifyDepth"].ToString()), 
                    TA.GetTexture(layer, dt.Rows[i]["imageName"].ToString()));

            return go;
        }

        public IStateble GetStates(int index)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM States WHERE States.id = " + index);

            if (dt.Rows.Count == 0)
                return null;

            double[] statesArray = new double[dt.Columns.Count - 1];
            for (int i = 0; i < dt.Columns.Count - 1; i++)
                statesArray[i] = double.Parse(dt.Rows[0][i + 1].ToString());

            return new States(statesArray);
        }

        /*public IInventoryble GetInventory(int index)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM Cells WHERE Cells.idGraphicObject = " + index);
        }*/

        public Background GetBackground(int num)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM Backgrounds WHERE Backgrounds.id = " + num);

            GraphicObjectsPack gop = new GraphicObjectsPack();
            gop.AddGraphicObject(
                "main", 
                ChangePartLayer.Yes, 
                GetGraphicObject(int.Parse(dt.Rows[0]["idGraphicObject"].ToString()), Layer.Background)
                );

            ObjectInfo oi = new ObjectInfo(
                num,
                (string)dt.Rows[0]["name"],
                (string)dt.Rows[0]["description"]
                );

            return new Background(
                gop,
                oi,
                (bool)dt.Rows[0]["passableness"]
                );
        }

        public Block GetBlock(int num)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM Blocks WHERE Blocks.id = " + num);

            GraphicObjectsPack gop = new GraphicObjectsPack();
            gop.AddGraphicObject(
                "main",
                ChangePartLayer.Yes,
                GetGraphicObject(int.Parse(dt.Rows[0]["idGraphicObject"].ToString()), Layer.Block)
                );

            ObjectInfo oi = new ObjectInfo(
                num,
                (string)dt.Rows[0]["name"],
                (string)dt.Rows[0]["description"]
                );

            return new Block(
                gop,
                oi,
                (bool)dt.Rows[0]["passableness"],
                (bool)dt.Rows[0]["transparency"],
                (bool)dt.Rows[0]["permeability"]
                );
        }

        public Decal GetDecal(int num)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM Decals WHERE Decals.id = " + num);

            GraphicObjectsPack gop = new GraphicObjectsPack();
            gop.AddGraphicObject(
                "main",
                ChangePartLayer.Yes,
                GetGraphicObject(int.Parse(dt.Rows[0]["idGraphicObject"].ToString()), Layer.Decal)
                );

            return new Decal(
                gop
                );
        }

        //TODO: считывать инвентарь и характеристики.
        public Being GetBeing(int num)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM Beings WHERE Beings.id = " + num);

            GraphicObjectsPack gop = new GraphicObjectsPack();
            gop.AddGraphicObject(
                "main",
                ChangePartLayer.Yes,
                GetGraphicObject(int.Parse(dt.Rows[0]["idGraphicObject"].ToString()), Layer.Being)
                );

            ObjectInfo oi = new ObjectInfo(
                num,
                (string)dt.Rows[0]["name"],
                (string)dt.Rows[0]["description"]
                );

            return new Being(
                gop,
                oi,
                int.Parse(dt.Rows[0]["alliance"].ToString())
                );
        }

        public Item GetFuncItem(int num)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM Items WHERE Items.id = " + num);

            DataTable dt2 = DBIO.ExecuteSQL("SELECT * FROM ClosedSections WHERE ClosedSections.idItem = " + num);
            List<Section> ls = new List<Section>();
            for (int i = 0; i < dt2.Rows.Count; i++)
                ls.Add((Section)int.Parse(dt2.Rows[i]["section"].ToString()));

            ObjectInfo oi = new ObjectInfo(
                num,
                (string)dt.Rows[0]["name"],
                (string)dt.Rows[0]["description"]
                );

            return new Item(
                GetGraphicObject(int.Parse(dt.Rows[0]["idGraphicObject"].ToString()), Layer.Item),
                oi,
                int.Parse(dt.Rows[0]["price"].ToString()),
                (Section)int.Parse(dt.Rows[0]["section"].ToString()),
                ls,
                GetStates(int.Parse(dt.Rows[0]["states"].ToString()))
                );
        }

        public Item GetItem(int num)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM Items WHERE Items.id = " + num);

            DataTable dt2 = DBIO.ExecuteSQL("SELECT * FROM ClosedSections WHERE ClosedSections.idItem = " + num);
            List<Section> ls = new List<Section>();
            for (int i = 0; i < dt2.Rows.Count; i++)
                ls.Add((Section)int.Parse(dt2.Rows[i]["section"].ToString()));
            
            ObjectInfo oi = new ObjectInfo(
                num,
                (string)dt.Rows[0]["name"],
                (string)dt.Rows[0]["description"]
                );

            return new Item(
                GetGraphicObject(int.Parse(dt.Rows[0]["idGraphicObject"].ToString()), Layer.Item),
                oi,
                int.Parse(dt.Rows[0]["price"].ToString()),
                (Section)int.Parse(dt.Rows[0]["section"].ToString()),
                ls,
                GetStates(int.Parse(dt.Rows[0]["states"].ToString()))
                );
        }

        public Armor GetArmor(int num)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM Armors WHERE Armors.id = " + num);

            return new Armor(
                GetItem(int.Parse(dt.Rows[0]["idItem"].ToString())),
                int.Parse(dt.Rows[0]["level"].ToString())
                );
        }
        public Shield GetShield(int num)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM Shields WHERE Shields.id = " + num);

            return new Shield(
                GetItem(int.Parse(dt.Rows[0]["idItem"].ToString())),
                int.Parse(dt.Rows[0]["level"].ToString())
                );
        }
        public Weapon GetWeapon(int num)
        {
            DataTable dt = DBIO.ExecuteSQL("SELECT * FROM Weapons WHERE Weapons.id = " + num);

            return new Weapon(
                GetItem(int.Parse(dt.Rows[0]["idItem"].ToString())),
                int.Parse(dt.Rows[0]["level"].ToString()),
                int.Parse(dt.Rows[0]["minDistance"].ToString()),
                int.Parse(dt.Rows[0]["maxDistance"].ToString()),
                int.Parse(dt.Rows[0]["damage"].ToString())
                );
        }
    }
}
