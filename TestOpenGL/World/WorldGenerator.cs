using System.Collections.Generic;

namespace TestOpenGL.World
{
	class WorldGenerator
	{
		const int SIZE_X = 5;
		const int SIZE_Y = 5;

		int[,] map;
		List<int[,]> maps;
		int[,] numbers;
		int count;

		/// <summary>
		/// Считает все возможные варианты заполнения матрицы map числами из numbers
		/// В numbers хранятся доступные числа и их количества (в первой колонке - число, во второй - количество)
		/// </summary>
		public void GenerateNumericMap()
		{
			map = new int[SIZE_X, SIZE_Y];
			maps = new List<int[,]>();
			numbers = new int[3, 2] {
				{ 0, int.MaxValue },
				{ 1, 2 },
				{ 2, 2 }
			};
			count = 0;

			NextGeneration(0, 0);

			System.Random rnd = new System.Random();
			ShowArray(maps[rnd.Next(0, maps.Count)]);

			System.Windows.Forms.MessageBox.Show("Count: " + count.ToString());
		}

		public void NextGeneration(int startX, int startY)
		{
			int nextX = startX;
			int nextY = startY;
			int nextNumber;

			// Высчитываем позицию следующего элемента
			if (++nextY == map.GetLength(1))
			{
				nextX++;
				nextY = 0;
			}

			for (int index = 0; index < numbers.GetLength(0); index++)
			{
				// Если число доступно
				if (numbers[index, 1] > 0)
				{
					nextNumber = numbers[index, 0];
					numbers[index, 1]--;
				}
				else
				{
					continue;
				}


				map[startX, startY] = nextNumber;

				int c = 0;
				for (int i = 1; i < numbers.GetLength(0); i++)
				{
					c += numbers[i, 1];
				}
				if (c == 0)
					maps.Add((int[,])map.Clone());

				// Если дальше идти некуда, засчитываем расстановку
				if (nextX == map.GetLength(0))
				{
					count++;
					//ShowArray();
				}
				// Иначе расставляем дальше
				else
				{
					NextGeneration(nextX, nextY);
				}

				// Возвращаем забранное число
				numbers[index, 1]++;
			}
		}

		public void ShowArray(int[,] map)
		{
			string s = "";
			for (int j = 0; j < map.GetLength(1); j++)
			{
				for (int i = 0; i < map.GetLength(0); i++)
					s += map[i, j].ToString() + " ";
				s += "\n";
			}
			System.Windows.Forms.MessageBox.Show(s);
		}
	}
}
