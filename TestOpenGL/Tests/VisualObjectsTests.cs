using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestOpenGL.Renders;
using TestOpenGL.VisualObjects;
using TestOpenGL.VisualObjects.ChieldsBeing;
using TestOpenGL.World;

namespace TestOpenGL.Tests
{
    /*[TestClass]
    class VisualObjectsTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            
        }
    }*/
    [TestClass]
    public class VisualObjectsTests
    {
        [TestMethod]
        public void TestBengs()
        {
            /*Program.L = new Level(10, 10, 5);
            Being b = new Bot(7, "testName", "testDescription", new Texture(13), 666, null);

            if (b.Alliance != 666)
                throw new Exception();

            if(b.IsSpawned)
                throw new Exception();

            //if(b.C != null)
            //    throw new Exception(b.C.Y.ToString());
                // Странно, вроде координаты стандартно не инициализируются, а они всё равно есть, нулевые.

            Program.L.GetMap<Being>().AddVO(new Bot(1, "", "", new Texture(1), 665, null), new Coord(1, 2, 3));

            if(b.SetNewCoord(new Coord(1, 2, 3)))
                throw new Exception();

            if (b.C == new Coord(1, 2, 3))
                throw new Exception();

            Program.L.GetMap<Being>().RemoveVO(new Coord(1, 2, 3));

            if(!b.Spawn(new Coord(1, 2, 0)))
                throw new Exception();

            if (b.C != new Coord(1, 2, 0)) 
                throw new Exception();

            if(!b.IsSpawned)
                throw new Exception();

            if(Program.L.GetMap<Being>().GetVO(new Coord(1,2,0)).visualObjectInfo.Name != "testName")
                throw new Exception();

            b.features.ActionPoints++;
            if(!b.Move(Direction.Right))
                throw new Exception();

            if (b.C != new Coord(2,2,0))
                throw new Exception();*/
        }
    }
}
