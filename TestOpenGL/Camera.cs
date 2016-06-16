using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestOpenGL.Logic;
using TestOpenGL.VisualObjects;

namespace TestOpenGL
{
    class Camera
    {
        Level l;
        // Размеры отображаемой части поля
        int width, height;
        // Текущее смещение камеры относительно блока (0, 0)
        int shiftX, shiftY;
        // Позиция прицела
        public EventDelegate changeCameraPosition;
        
        Being looking;

        public Camera(int width, int height)
        {
            this.l = Program.L;
            this.width = width;
            this.height = height;

            Look();
        }

        public int Height
        { get { return height; } }
        public int Width
        { get { return width; } }
        public int MinX
        { get { return shiftX; } }
        public int MaxX
        { get { return shiftX + Width - 1; } }
        public int MinY
        { get { return shiftY; } }
        public int MaxY
        { get { return shiftY + Height - 1; } }
        
        public void SetLookingBeing(Being b)
        {
            looking = b;
            Look();
            if (b != null)
            {
                b.eventsBeing.EventBeingChangeCoord += new EventDelegate(Look);
            }
        }

        public void Look()
        {
            if (looking != null)
            {
                shiftX = looking.C.X - width / 2;
                shiftX = shiftX < 0 ? 0 : shiftX;
                shiftX = shiftX > l.LengthX - width ? l.LengthX - width : shiftX;
                shiftY = looking.C.Y - height / 2;
                shiftY = shiftY < 0 ? 0 : shiftY;
                shiftY = shiftY > l.LengthY - height ? l.LengthY - height : shiftY;
            }
            else
            {
                shiftX = 0;
                shiftY = 0;
            }

            if (changeCameraPosition != null)
                changeCameraPosition();

            return;
        }
    }

}
