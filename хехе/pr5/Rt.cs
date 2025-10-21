using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr5
{
    class Rt
    {
        static void rut()
        {
            int rows = 10;
            int cols = 10;

            Random rand = new Random();
            double[,] matrix = new double[rows, cols];

            // Заполнение матрицы случайными числами
            Console.WriteLine("\nИсходная матрица:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = Math.Round(rand.NextDouble() * 20 - 10, 2);
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }

            // Подсчет результатов
            double sumGreaterThanTwo = 0;
            int negativeCount = 0;
            double maxElement = matrix[0, 0];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    double element = matrix[i, j];

                    if (element >= 2)
                        sumGreaterThanTwo += element;
                    if (element < 0)
                        negativeCount++;
                    if (element > maxElement)
                        maxElement = element;
                }
            }

            // Вывод результатов
            Console.WriteLine($"\nРезультаты анализа матрицы:");
            Console.WriteLine($"1. Сумма элементов ≥ 2: {sumGreaterThanTwo:F2}");
            Console.WriteLine($"2. Количество отрицательных элементов: {negativeCount}");
            Console.WriteLine($"3. Максимальный элемент: {maxElement:F2}");
        }
    }
}
