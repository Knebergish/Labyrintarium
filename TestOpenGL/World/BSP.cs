using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOpenGL.PhisicalObjects;

namespace TestOpenGL.World
{
	class BSP
	{
	}

	class Leaf
	{
		static Random rnd = new Random();
		private const int MIN_LEAF_SIZE = 5;

		public int y, x, width, height; // the position and size of this Leaf

		public Leaf leftChild; // the Leaf's left child Leaf
		public Leaf rightChild; // the Leaf's right child Leaf
		public Rectangle room; // the room that is inside this Leaf
		public List<Rectangle> halls; // hallways to connect this Leaf to other Leafs

		public Leaf(int X, int Y, int Width, int Height)
		{
			// initialize our leaf
			x = X;
			y = Y;
			width = Width;
			height = Height;
		}

		public bool split()
		{
			// begin splitting the leaf into two children
			if (leftChild != null || rightChild != null)
				return false; // we're already split! Abort!

			// determine direction of split
			// if the width is >25% larger than height, we split vertically
			// if the height is >25% larger than the width, we split horizontally
			// otherwise we split randomly
			bool splitH = rnd.NextDouble() > 0.5;
			if (width > height && width / height >= 1.25)
				splitH = false;
			else if (height > width && height / width >= 1.25)
				splitH = true;

			int max = (splitH ? height : width) - MIN_LEAF_SIZE; // determine the maximum height or width
			if (max <= MIN_LEAF_SIZE)
				return false; // the area is too small to split any more...

			int split = rnd.Next(MIN_LEAF_SIZE, max); // determine where we're going to split

			// create our left and right children based on the direction of the split
			if (splitH)
			{
				leftChild = new Leaf(x, y, width, split);
				rightChild = new Leaf(x, y + split, width, height - split);
			}
			else
			{
				leftChild = new Leaf(x, y, split, height);
				rightChild = new Leaf(x + split, y, width - split, height);
			}
			return true; // split successful!
		}

		static public List<Leaf> Activate()
		{
			const int MAX_LEAF_SIZE = 10;

			List<Leaf> leafs = new List<Leaf>();

			//Leaf l; // helper Leaf

			// first, create a Leaf to be the 'root' of all Leafs.
			Leaf root = new Leaf(0, 0, 20, 20);
			leafs.Add(root);

			bool did_split = true;
			// we loop through every Leaf in our Vector over and over again, until no more Leafs can be split.
			while (did_split)
			{
				did_split = false;
				//foreach(Leaf l in leafs)
				for (int i = 0; i < leafs.Count; i++)
				{
					Leaf l = leafs[i];
					if (l.leftChild == null && l.rightChild == null) // if this Leaf is not already split...
					{
						// if this Leaf is too big, or 75% chance...
						if (l.width > MAX_LEAF_SIZE || l.height > MAX_LEAF_SIZE || rnd.NextDouble() > 0.25)
						{
							if (l.split()) // split the Leaf!
							{
								// if we did split, push the child leafs to the Vector so we can loop into them next
								leafs.Add(l.leftChild);
								leafs.Add(l.rightChild);
								did_split = true;
							}
						}
					}
				}
			}

			root.createRooms();

			return leafs;
			/*return new List<Leaf>(leafs.FindAll(
				leaf => leaf.leftChild == null && leaf.rightChild == null
				));*/
		}


		public void createRooms()
		{
			// this function generates all the rooms and hallways for this Leaf and all of its children.
			if (leftChild != null || rightChild != null)
			{
				// this leaf has been split, so go into the children leafs
				if (leftChild != null)
				{
					leftChild.createRooms();
				}
				if (rightChild != null)
				{
					rightChild.createRooms();
				}

				// if there are both left and right children in this Leaf, create a hallway between them
				if (leftChild != null && rightChild != null)
				{
					createHall(leftChild.getRoom(), rightChild.getRoom());
				}
			}
			else
			{
				// this Leaf is the ready to make a room
				Tuple<int, int> roomSize;
				Tuple<int, int> roomPos;
				// the room can be between 3 x 3 tiles to the size of the leaf - 2.
				roomSize = new Tuple<int, int>(rnd.Next(3, width), rnd.Next(3, height));
				// place the room within the Leaf, but don't put it right 
				// against the side of the Leaf (that would merge rooms together)
				roomPos = new Tuple<int, int>(rnd.Next(1, width - roomSize.Item1), rnd.Next(1, height - roomSize.Item2));
				room = new Rectangle(x + roomPos.Item1, y + roomPos.Item2, roomSize.Item1, roomSize.Item2);
			}
		}

		public Rectangle getRoom()
		{
			// iterate all the way through these leafs to find a room, if one exists.
			if (room != null)
				return room;
			else
			{
				Rectangle lRoom = null;
				Rectangle rRoom = null;
				if (leftChild != null)
				{
					lRoom = leftChild.getRoom();
				}
				if (rightChild != null)
				{
					rRoom = rightChild.getRoom();
				}
				if (lRoom == null && rRoom == null)
					return null;
				else if (rRoom == null)
					return lRoom;
				else if (lRoom == null)
					return rRoom;
				else if (rnd.NextDouble() > 0.5)
					return lRoom;
				else
					return rRoom;
			}
		}

		public void createHall(Rectangle l, Rectangle r)
		{
			// now we connect these two rooms together with hallways.
			// this looks pretty complicated, but it's just trying to figure out which point is where and then either draw a straight line, or a pair of lines to make a right-angle to connect them.
			// you could do some extra logic to make your halls more bendy, or do some more advanced things if you wanted.

			halls = new List<Rectangle>();

			Tuple<int, int> point1 = new Tuple<int, int>(rnd.Next(l.left() + 1, l.right() - 2), rnd.Next(l.top() + 1, l.bottom() - 2));
			Tuple<int, int> point2 = new Tuple<int, int>(rnd.Next(r.left() + 1, r.right() - 2), rnd.Next(r.top() + 1, r.bottom() - 2));

			int w = point2.Item1 - point1.Item1;
			int h = point2.Item2 - point1.Item2;

			if (w < 0)
			{
				if (h < 0)
				{
					if (rnd.NextDouble() < 0.5)
					{
						halls.Add(new Rectangle(point2.Item1, point1.Item2, Math.Abs(w), 1));
						halls.Add(new Rectangle(point2.Item1, point2.Item2, 1, Math.Abs(h)));
					}
					else
					{
						halls.Add(new Rectangle(point2.Item1, point2.Item2, Math.Abs(w), 1));
						halls.Add(new Rectangle(point1.Item1, point2.Item2, 1, Math.Abs(h)));
					}
				}
				else if (h > 0)
				{
					if (rnd.NextDouble() < 0.5)
					{
						halls.Add(new Rectangle(point2.Item1, point1.Item2, Math.Abs(w), 1));
						halls.Add(new Rectangle(point2.Item1, point1.Item2, 1, Math.Abs(h)));
					}
					else
					{
						halls.Add(new Rectangle(point2.Item1, point2.Item2, Math.Abs(w), 1));
						halls.Add(new Rectangle(point1.Item1, point1.Item2, 1, Math.Abs(h)));
					}
				}
				else // if (h == 0)
				{
					halls.Add(new Rectangle(point2.Item1, point2.Item2, Math.Abs(w), 1));
				}
			}
			else if (w > 0)
			{
				if (h < 0)
				{
					if (rnd.NextDouble() < 0.5)
					{
						halls.Add(new Rectangle(point1.Item1, point2.Item2, Math.Abs(w), 1));
						halls.Add(new Rectangle(point1.Item1, point2.Item2, 1, Math.Abs(h)));
					}
					else
					{
						halls.Add(new Rectangle(point1.Item1, point1.Item2, Math.Abs(w), 1));
						halls.Add(new Rectangle(point2.Item1, point2.Item2, 1, Math.Abs(h)));
					}
				}
				else if (h > 0)
				{
					if (rnd.NextDouble() < 0.5)
					{
						halls.Add(new Rectangle(point1.Item1, point1.Item2, Math.Abs(w), 1));
						halls.Add(new Rectangle(point2.Item1, point1.Item2, 1, Math.Abs(h)));
					}
					else
					{
						halls.Add(new Rectangle(point1.Item1, point2.Item2, Math.Abs(w), 1));
						halls.Add(new Rectangle(point1.Item1, point1.Item2, 1, Math.Abs(h)));
					}
				}
				else // if (h == 0)
				{
					halls.Add(new Rectangle(point1.Item1, point1.Item2, Math.Abs(w), 1));
				}
			}
			else // if (w == 0)
			{
				if (h < 0)
				{
					halls.Add(new Rectangle(point2.Item1, point2.Item2, 1, Math.Abs(h)));
				}
				else if (h > 0)
				{
					halls.Add(new Rectangle(point1.Item1, point1.Item2, 1, Math.Abs(h)));
				}
			}
		}

		public static void Fill<T>(Rectangle rectangle, Func<T> getT) where T : PhisicalObject
		{
			for (int x = rectangle.x; x < rectangle.x + rectangle.width; x++)
				for (int y = rectangle.y; y < rectangle.y + rectangle.height; y++)
				{
					getT().Spawn(0, new Coord(x, y));
				}
		}
	}





	class Rectangle
	{
		public int x, y, width, height;

		public Rectangle(int x, int y, int width, int height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		public int left()
		{
			return x;
		}

		public int right()
		{
			return x + width;
		}

		public int top()
		{
			return y;
		}

		public int bottom()
		{
			return y + height;
		}
	}
}
