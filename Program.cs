using System;

namespace hierarchical_clustering
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] coordinates = new double[16, 2] { {10,17},{10,16},{11,13},{10,12},
                                               {14,11},{16,10},{15,7}, {15,6},
                                               {12,4},  {10,5},  {6,7}, {5,8},
                                               {3,10},   {2,10}, {3,14},{2,13}
                                              };
            calculateFunc(coordinates);

        }
        public static void calculateFunc(double[,] coordinates)
        {   
            //Массив расстояний, на 1 меньше, чем всего координат, потому что последняя строка учитывается во всех предудыущих расстояниях
            int k = coordinates.Length/2;
			double[,] distance = new double[k - 1, k - 1];

			for (int i = 1; i <= k - 1; i++)
			{
				for (int j = 1; j <= k - 1; j++)
				{
					distance[i - 1, j - 1] = Math.Sqrt(Math.Pow((coordinates[j, 0] - coordinates[i - 1, 0]), 2) +
												Math.Pow((coordinates[j, 1] - coordinates[i - 1, 1]), 2));
				}
			}
            //Обнуляем все что лежит под главной диагональю
			for (int i = 1; i < k - 1; i++)
			{
				for (int j = 0; j < k - 1; j++)
				{
					if (j < i)
					{
						distance[i, j] = 0;
					}
				}
			}
            //Массив минимальных расстояний
			double[] distanceMin = new double[k - 1];
            //Массив индексов минимальных значений
			int[] indexMin = new int[k - 1];
			for (int i = 0; i < k - 1; i++)
			{
				distanceMin[i] = 1000;
				for (int j = 0; j < k - 1; j++)
				{
					if ((distance[i, j] < distanceMin[i]) & distance[i, j] != 0)
					{
						distanceMin[i] = distance[i, j];
						indexMin[i] = j + 1;
					}
				}
            }

			Console.WriteLine("Пары ближайших точек");
			double[,] newCoordinates = new double[(indexMin.Length + 1) / 2, 2];
			for (int i = 1; i <= distanceMin.Length; i++)
			{
				if ((i - 1) % 2 == 0)
				{
					Console.WriteLine(indexMin[i - 1] + "-" + (i-1));
                    //Считаю новые координаты точек, чтобы использовать их для будующего слива в пары
                    newCoordinates[(i - 1) / 2, 0] = (coordinates[indexMin[i - 1], 0] + coordinates[i - 1, 0]);
                    newCoordinates[(i - 1) / 2, 1] = (coordinates[indexMin[i - 1], 1] + coordinates[i - 1, 1]);
				}
			}
            //Проверка на вызов функции, вызываем если число пар больше 1
			if (newCoordinates.Length / 2>1)
			{
				calculateFunc(newCoordinates);
            }
        }
    }
}
