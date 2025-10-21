namespace pr6
{
    using System;
    using System.Linq;

    struct NOTE
    {
        public string FirstName;
        public string LastName;
        public string PhoneNumber;
        public int[] Birthday; // [день, месяц, год]

        // Метод для вывода информации о человеке
        public void DisplayInfo()
        {
            Console.WriteLine($"Фамилия: {LastName}");
            Console.WriteLine($"Имя: {FirstName}");
            Console.WriteLine($"Телефон: {PhoneNumber}");
            Console.WriteLine($"Дата рождения: {Birthday[0]:00}.{Birthday[1]:00}.{Birthday[2]}");
        }

        // Метод для получения даты рождения в формате DateTime для сравнения
        public DateTime GetBirthDate()
        {
            return new DateTime(Birthday[2], Birthday[1], Birthday[0]);
        }
    }

    class Program
    {
        static void Main()
        {
            NOTE[] notes = new NOTE[8];

            Console.WriteLine("Введите данные для 8 человек:");

            // Ввод данных
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine($"\n--- Человек {i + 1} ---");

                Console.Write("Фамилия: ");
                notes[i].LastName = Console.ReadLine();

                Console.Write("Имя: ");
                notes[i].FirstName = Console.ReadLine();

                Console.Write("Номер телефона: ");
                notes[i].PhoneNumber = Console.ReadLine();

                notes[i].Birthday = new int[3];

                Console.Write("День рождения: ");
                notes[i].Birthday[0] = int.Parse(Console.ReadLine());

                Console.Write("Месяц рождения: ");
                notes[i].Birthday[1] = int.Parse(Console.ReadLine());

                Console.Write("Год рождения: ");
                notes[i].Birthday[2] = int.Parse(Console.ReadLine());
            }

            // Сортировка по датам рождения
            notes = notes.OrderBy(n => n.GetBirthDate()).ToArray();

            Console.WriteLine("\n=== Отсортированный список по датам рождения ===");
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine($"{i + 1}. {notes[i].LastName} {notes[i].FirstName} - " +
                                $"{notes[i].Birthday[0]:00}.{notes[i].Birthday[1]:00}.{notes[i].Birthday[2]}");
            }

            // Поиск по номеру телефона
            Console.Write("\nВведите номер телефона для поиска: ");
            string searchPhone = Console.ReadLine();

            bool found = false;
            foreach (var note in notes)
            {
                if (note.PhoneNumber == searchPhone)
                {
                    Console.WriteLine("\n=== Найденная запись ===");
                    note.DisplayInfo();
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine("\nЧеловек с таким номером телефона не найден.");
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
