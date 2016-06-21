using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestOpenGL.VisualObjects;

namespace TestOpenGL.Logic
{
    /// <summary>
    /// Организует работу с блоками на карте.
    /// </summary>
    /*class MapBlocks
    {
        // Максимальные измерения карты (LengthZ для глубины карты блоков)
        private int lengthX, lengthY, lengthZ;

        // Карта блоков на уровне
        private Block[, ,] mapBlocks;

        public MapBlocks(int lengthX, int lengthY, int lengthZ)
        {
            this.lengthX = lengthX;
            this.lengthY = lengthY;
            this.lengthZ = lengthZ;

            mapBlocks = new Block[this.lengthX, this.lengthY, this.lengthZ];
        }

        public bool IsPassable(Coord C)
        {
            for (int i = 0; i < this.lengthZ; i++)
            {
                if (this.GetBlock(new Coord(C.X, C.Y, i)) != null)
                    if (!this.GetBlock(new Coord(C.X, C.Y, i)).Passableness)
                        return false;
            }
            return true;
        }

        public bool IsPermeable(Coord C)
        {
            for (int i = 0; i < this.lengthZ; i++)
            {
                if (this.GetBlock(new Coord(C.X, C.Y, i)) != null)
                    if (!this.GetBlock(new Coord(C.X, C.Y, i)).Permeability)
                        return false;
            }
            return true;
        }


        /// <summary>
        /// Возвращает объект блока из указанной ячейки и слоя карты блоков.
        /// </summary>
        /// <param name="x">Первая координата блока.</param>
        /// <param name="y">Вторая координата блока.</param>
        /// <param name="z">Глубина блока.</param>
        /// <returns>Объект блока, если по указанным координатам блок есть, и null, если блока нету или координаты некорректы.</returns>
        /// 
        public Block GetBlock(Coord C)
        {
            return this.mapBlocks[C.X, C.Y, C.Z];
        }

        /// <summary>
        /// Установка одного блока на карту с перерисовкой карты.
        /// </summary>
        public void SetBlock(Block B, Coord C)
        {
            this.mapBlocks[C.X, C.Y, C.Z] = B;
        }

        /// <summary>
        /// Установка блоков на карту из буферной структуры с перерисовкой карты в конце.
        /// </summary>
        /// <param name="BS">Буферная структура, содержащая блоки и координаты их установки.</param>
        public void SetBlocks(VisualObjectStructure<Block> blocksStructure)
        {
            Coord C;

            while (blocksStructure.Count > 0)
            {
                C = blocksStructure.PopCoord();

                this.mapBlocks[C.X, C.Y, C.Z] = null;
                this.mapBlocks[C.X, C.Y, C.Z] = blocksStructure.PopObject();
            }
        }

        //TODO: Флаги в параметрах - зло. Переделать. Возможно, стоит создать новое перечисление.
        /// <summary>
        /// Проверяет ячейку на проходимость для сущностей.
        /// </summary>
        /// <param name="C"> Объект с координатами проверяемой ячейки.</param>
        /// <param name="type"> true - учитываются сущности, false - не учитываются. </param>
        /// <returns></returns>
        public bool IsPassable(Coord C, bool type)
        {
            for (int i = 0; i < this.lengthZ; i++)
            {
                if (this.GetBlock(new Coord(C.X, C.Y, i)) != null)
                    if (!this.GetBlock(new Coord(C.X, C.Y, i)).Passableness)
                        return false;
            }

            return true;
        }
    }*/
}
