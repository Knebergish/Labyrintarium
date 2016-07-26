using System.Collections.Generic;
using System.Data;

using TestOpenGL.Renders;
using TestOpenGL.Logic;

namespace TestOpenGL.Renders
{
    class GraphicObject : IPositionable, IRenderable
    {
        Position position;
        DataTable cellsDataTable;
        //-------------


        public GraphicObject(Layer layer)
        {
            cellsDataTable = new DataTable();
            cellsDataTable.Columns.Add("deltaCoord", typeof(UnsafeCoord));
            cellsDataTable.Columns.Add("deltaLayer", typeof(int));
            cellsDataTable.Columns.Add("modifyDepth", typeof(ModifyDepth));
            cellsDataTable.Columns.Add("texture", typeof(Texture));

            position = new Position(layer);
        }

        public Layer Layer
        { get { return position.Layer; } }

        public int PartLayer
        { get { return position.PartLayer; } }

        public Coord Coord
        { get { return position.Coord; } }

        
        //=============


        public void AddCell(UnsafeCoord deltaCoord, int deltaLayer, ModifyDepth modifyDepth, Texture texture)
        {
            cellsDataTable.Rows.Add(deltaCoord, deltaLayer, modifyDepth, texture);
        }

        public List<Cell> GetCells()
        {
            List<Cell> listCells = new List<Cell>();

            for(int i = 0; i < cellsDataTable.Rows.Count; i++)
            {
                UnsafeCoord dc = (UnsafeCoord)cellsDataTable.Rows[i][0];
                UnsafeCoord uc = new UnsafeCoord(position.Coord.X + dc.X, position.Coord.Y + dc.Y);
                if(uc.IsCorrect())
                {
                    Layer resultLayer = (Layer)((int)position.Layer + (int)(Layer)cellsDataTable.Rows[i][1]);
                    ModifyDepth modifyDepth = (ModifyDepth)cellsDataTable.Rows[i][2];
                    Texture texture = (Texture)cellsDataTable.Rows[i][3];

                    listCells.Add(
                        new Cell(
                            new Coord(uc),
                            Analytics.GetGlobalDepth(resultLayer, position.PartLayer, modifyDepth),
                            texture
                            ));
                }
            }

            return listCells;
        }

        public bool SetNewPosition(int newPartLayer, Coord newCoord)
        {
            position.SetNewPartLayer(newPartLayer);
            position.Coord = newCoord;
            return true;
        }
    }
}
