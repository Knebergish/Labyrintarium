using System.Collections.Generic;
using System.Data;

using TestOpenGL.Renders;


namespace TestOpenGL.Renders
{
    class GraphicObjectsPack : IRenderable
    {
        DataTable graphicObjectsDataTable;
        IPositionable positionObject;
        //-------------


        public GraphicObjectsPack()
        {
            graphicObjectsDataTable = new DataTable();
            graphicObjectsDataTable.Columns.Add("key", typeof(string));
            graphicObjectsDataTable.Columns.Add("changePartLayer", typeof(ChangePartLayer));
            graphicObjectsDataTable.Columns.Add("graphicObject", typeof(GraphicObject));
        }
        public GraphicObjectsPack(IPositionable positionObject)
            : this()
        {
            PositionObject = positionObject;
        }

        public IPositionable PositionObject
        {
            get { return positionObject; }
            set
            {
                positionObject = value;

                if (positionObject != null)
                    UpdatePosition();
            }
        }
        //=============


        public List<Cell> GetCells()
        {
            List<Cell> lc = new List<Cell>();

            if (positionObject != null)
                for (int i = 0; i < graphicObjectsDataTable.Rows.Count; i++)
                    lc.AddRange(((GraphicObject)graphicObjectsDataTable.Rows[i]["graphicObject"]).GetCells());

            return lc;
        }

        public void AddGraphicObject(string key, ChangePartLayer changePartLayer, GraphicObject graphicObject)
        {
            graphicObjectsDataTable.Rows.Add(key, changePartLayer, graphicObject);

            UpdatePosition();
        }
        public void RemoveGraphicObject(string key)
        {
            for (int i = 0; i < graphicObjectsDataTable.Rows.Count; i++)
                if (graphicObjectsDataTable.Rows[i]["key"].ToString() == key)
                {
                    graphicObjectsDataTable.Rows.RemoveAt(i);
                    break;
                }
        }
        public void UpdatePosition()
        {
            if (positionObject == null)
                return;

            for (int i = 0; i < graphicObjectsDataTable.Rows.Count; i++)
            {
                if ((ChangePartLayer)graphicObjectsDataTable.Rows[i]["changePartLayer"] == ChangePartLayer.Yes)
                    ((GraphicObject)graphicObjectsDataTable.Rows[i]["graphicObject"]).SetNewPosition
                        (
                        ((GraphicObject)graphicObjectsDataTable.Rows[i]["graphicObject"]).PartLayer,
                        positionObject.Coord
                        );
                else
                    ((GraphicObject)graphicObjectsDataTable.Rows[i]["graphicObject"]).SetNewPosition
                            (
                            positionObject.PartLayer,
                            positionObject.Coord
                            );
            }
        }

        public List<GraphicObject> GetAllGraphicObjects()
        {
            List<GraphicObject> lgo = new List<GraphicObject>();

            for (int i = 0; i < graphicObjectsDataTable.Rows.Count; i++)
                lgo.Add((GraphicObject)graphicObjectsDataTable.Rows[i]["graphicObject"]);

            return lgo;
        }
    }
}
