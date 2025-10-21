namespace pr5
{
    using System;

    class Program
    {

        static void Pr1()
        {
            Random rand = new Random(); 
            int N = 10; // Размер массива
            int[] Arr = new int[N]; // объявление массива с N количеством чисел

            // Заполнение массива случайными числами
            Console.WriteLine("Исходный массив:");
            for (int i = 0; i < N; i++)
            {
                Arr[i] = rand.Next(-100, 101); // Числа от -100 до 100
                Console.WriteLine(Arr[i]);
            }
            Console.WriteLine(" ");

            // Замена элементов на сумму последующих  :)>
            for (int i = 0; i < N - 1; i++)
            {
                int sum = 0;
                for (int j = i + 1; j < N; j++)
                {
                    sum += Arr[j];
                }
                Arr[i] = sum;
            }

            // Вывод результата
            Console.WriteLine("Массив после замены:");
            foreach (int num in Arr)
            {
                Console.WriteLine(num);
            }
        }
        static void Pr2() 
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
                Console.WriteLine(" ");
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

            // Вывод выводов
            Console.WriteLine($"\nРезультаты анализа матрицы:");
            Console.WriteLine($"1. Сумма элементов ≥ 2: {sumGreaterThanTwo:F2}");
            Console.WriteLine($"2. Количество отрицательных элементов: {negativeCount}");
            Console.WriteLine($"3. Максимальный элемент: {maxElement:F2}");
        }
        static void Main()
        {
            Console.WriteLine("какое задание?: ");
            string Vibor = Console.ReadLine();
            switch (Vibor)
            {
                case "1" or "первое": Pr1(); break;
                case "2" or "второе": Pr2(); break;
                default: Console.WriteLine("чего ты хочешь? дурак!");break;
            }
        }
    }
}