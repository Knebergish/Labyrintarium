using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestOpenGL.VisualObjects;

namespace TestOpenGL
{
    class Weapon : Item
    {
        int minDistance, maxDistance;

        public Weapon(int id, string name, string description, Texture texture, int level, int price, List<Part> parts, int minDistance, int maxDistance)
            : base(id, name, description, texture, level, price, parts)
        {
            this.minDistance = minDistance;
            this.maxDistance = maxDistance;
            
        }

        public int MaxDistance
        { get { return maxDistance; } }

        public int MinDistance
        { get { return minDistance; } }    
    }
}
