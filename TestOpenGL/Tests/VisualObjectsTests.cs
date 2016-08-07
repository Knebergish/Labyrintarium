using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestOpenGL.Renders;
using TestOpenGL.PhisicalObjects;
using TestOpenGL.PhisicalObjects.ChieldsBeing;
using TestOpenGL.World;

namespace TestOpenGL
{
    [TestClass]
    public class VisualObjectsTests
    {
        [TestMethod]
        public void TestGraphicsObject()
        {
            Program.L = new Level(5, 5, new int[] { 5, 5, 5, 5, 5 });
            
            GraphicObject go = new GraphicObject(Layer.Block);
            if (go.Layer != Layer.Block)
                throw new Exception("Не учитывается слой, переданный в конструкторе.");

            if(!go.SetNewPosition(3, new Coord(1, 2)))
                throw new Exception("Не работает установка новой позиции.");

            if (go.PartLayer != 3 || go.Coord.X != 1 || go.Coord.Y != 2)
                throw new Exception("Некорректно работает установка новой позиции.");

            go.AddCell(new UnsafeCoord(0, 0), 0, ModifyDepth.None, new Texture(1));
            go.AddCell(new UnsafeCoord(0, 1), 0, ModifyDepth.ToLayer, new Texture(2));
            go.AddCell(new UnsafeCoord(1, 0), 0, ModifyDepth.ToPartLayer, new Texture(3));
            go.AddCell(new UnsafeCoord(0, -1), 0, ModifyDepth.UnderLayer, new Texture(4));
            go.AddCell(new UnsafeCoord(-1, 1), 0, ModifyDepth.UnderPartLayer, new Texture(5));

            List<Cell> lc = go.GetCells();
            if (lc[0].C.X != 1 || lc[0].C.Y != 2 || lc[0].GlobalDepth != 8 || lc[0].Texture.textureId != 1)
                throw new Exception("Ошибка в клетке 1.");
            if (lc[1].C.X != 1 || lc[1].C.Y != 3 || lc[1].GlobalDepth != 10.5 || lc[1].Texture.textureId != 2)
                throw new Exception("Ошибка в клетке 2.");
            if (lc[2].C.X != 2 || lc[2].C.Y != 2 || lc[2].GlobalDepth != 8 || lc[2].Texture.textureId != 3)
                throw new Exception("Ошибка в клетке 3.");
            if (lc[3].C.X != 1 || lc[3].C.Y != 1 || lc[3].GlobalDepth != 8 || lc[3].Texture.textureId != 4)
                throw new Exception("Ошибка в клетке 4.");
            if (lc[4].C.X != 0 || lc[4].C.Y != 3 || lc[4].GlobalDepth != 8 || lc[4].Texture.textureId != 5)
                throw new Exception("Ошибка в клетке 5.");
        }

        [TestMethod]
        public void TestBeing()
        {
            //Block b = new Block()
        }
    }
}
