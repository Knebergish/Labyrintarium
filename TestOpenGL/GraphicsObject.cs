using System.Collections.Generic;
using System.Data;

using TestOpenGL.Renders;
using TestOpenGL.Logic;

namespace TestOpenGL
{
    class GraphicObject : IPositionable, IRenderable
    {
        Layer layer;
        int partLayer;
        Coord coord;
        DataTable cellsDataTable;
        //-------------


        public GraphicObject(Layer layer)
        {
            cellsDataTable = new DataTable();
            cellsDataTable.Columns.Add("deltaCoord", typeof(UnsafeCoord));
            cellsDataTable.Columns.Add("deltaLayer", typeof(int));
            cellsDataTable.Columns.Add("modifyDepth", typeof(ModifyDepth));
            cellsDataTable.Columns.Add("texture", typeof(Texture));

            this.layer = layer;
        }

        public int PartLayer
        { get { return partLayer; } }

        public Coord Coord
        { get { return coord; } }

        public Layer Layer
        { get { return layer; } }
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
                UnsafeCoord uc = new UnsafeCoord(coord.X + dc.X, coord.Y + dc.Y);
                if(uc.IsCorrect())
                {
                    Layer resultLayer = (Layer)((int)layer + (int)(Layer)cellsDataTable.Rows[i][1]);
                    ModifyDepth modifyDepth = (ModifyDepth)cellsDataTable.Rows[i][2];
                    Texture texture = (Texture)cellsDataTable.Rows[i][3];

                    listCells.Add(
                        new Cell(
                            new Coord(uc),
                            Analytics.GetGlobalDepth(resultLayer, partLayer, modifyDepth),
                            texture
                            ));
                }
            }

            return listCells;
        }

        public bool SetNewPosition(int newPartLayer, Coord newCoord)
        {
            partLayer = newPartLayer;
            coord = newCoord;
            return true;
        }
    }
}
