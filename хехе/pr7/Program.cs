namespace pr7
{
    using System;
    using System.IO;
    using System.Text;

    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== РАБОТА С ФАЙЛАМИ ===");
            Console.WriteLine("1. Байт-ориентированная запись и чтение");
            Console.WriteLine("2. Символьная запись и чтение");
            Console.WriteLine("3. Произвольный доступ к файлу");
            Console.WriteLine("4. Копирование файла");
            Console.Write("Выберите режим работы: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ByteOrientedFileOperations();
                    break;
                case "2":
                    CharacterOrientedFileOperations();
                    break;
                case "3":
                    RandomAccessFileOperations();
                    break;
                case "4":
                    FileCopyOperations();
                    break;
                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        // 1. Байт-ориентированные операции с файлом
        static void ByteOrientedFileOperations()
        {
            Console.WriteLine("\n--- Байт-ориентированные операции ---");

            FileStream fs = null;

            try
            {
                // Запись в файл
                fs = new FileStream("byte_data.dat", FileMode.Create, FileAccess.Write);

                string text = "Hello, FileStream! Привет, файловый поток!";
                byte[] data = Encoding.UTF8.GetBytes(text);

                // Записываем длину данных
                fs.WriteByte((byte)data.Length);
                // Записываем сами данные
                fs.Write(data, 0, data.Length);

                Console.WriteLine("Данные записаны в файл.");
                fs.Close();

                // Чтение из файла
                fs = new FileStream("byte_data.dat", FileMode.Open, FileAccess.Read);

                // Читаем длину данных
                int length = fs.ReadByte();
                // Читаем данные
                byte[] readData = new byte[length];
                int bytesRead = fs.Read(readData, 0, length);

                string readText = Encoding.UTF8.GetString(readData, 0, bytesRead);
                Console.WriteLine($"Прочитанные данные: {readText}");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Файл не найден: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка ввода/вывода: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Общая ошибка: {ex.Message}");
            }
            finally
            {
                fs?.Close();
            }
        }

        // 2. Символьные операции с файлом
        static void CharacterOrientedFileOperations()
        {
            Console.WriteLine("\n--- Символьные операции ---");

            StreamWriter writer = null;
            StreamReader reader = null;

            try
            {
                // Запись в файл
                writer = new StreamWriter("text_data.txt", false, Encoding.UTF8);

                Console.WriteLine("Введите текст для записи в файл (для завершения введите 'STOP'):");

                string line;
                do
                {
                    line = Console.ReadLine();
                    if (line != "STOP")
                    {
                        writer.WriteLine(line);
                    }
                } while (line != "STOP");

                Console.WriteLine("Данные записаны в файл.");
                writer.Close();

                // Чтение из файла
                reader = new StreamReader("text_data.txt", Encoding.UTF8);

                Console.WriteLine("\nСодержимое файла:");
                int lineNumber = 1;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine($"{lineNumber}: {line}");
                    lineNumber++;
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка ввода/вывода: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Общая ошибка: {ex.Message}");
            }
            finally
            {
                writer?.Close();
                reader?.Close();
            }
        }

        // 3. Произвольный доступ к файлу
        static void RandomAccessFileOperations()
        {
            Console.WriteLine("\n--- Произвольный доступ к файлу ---");

            FileStream fs = null;

            try
            {
                // Создаем файл и записываем алфавит
                fs = new FileStream("random_access.dat", FileMode.Create, FileAccess.Write);

                for (char c = 'A'; c <= 'Z'; c++)
                {
                    fs.WriteByte((byte)c);
                }

                Console.WriteLine("Алфавит записан в файл.");
                fs.Close();

                // Произвольный доступ для чтения
                fs = new FileStream("random_access.dat", FileMode.Open, FileAccess.Read);

                // Читаем буквы в произвольном порядке
                Console.WriteLine("\nЧтение букв в произвольном порядке:");

                // Первая буква
                fs.Seek(0, SeekOrigin.Begin);
                Console.WriteLine($"Позиция 0: {(char)fs.ReadByte()}");

                // Пятая буква
                fs.Seek(4, SeekOrigin.Begin);
                Console.WriteLine($"Позиция 4: {(char)fs.ReadByte()}");

                // Последняя буква
                fs.Seek(-1, SeekOrigin.End);
                Console.WriteLine($"Последняя позиция: {(char)fs.ReadByte()}");

                // Четные позиции
                Console.WriteLine("\nБуквы на четных позициях:");
                for (int i = 0; i < 26; i += 2)
                {
                    fs.Seek(i, SeekOrigin.Begin);
                    Console.Write($"{(char)fs.ReadByte()} ");
                }
                Console.WriteLine();

                // Обратный порядок
                Console.WriteLine("\nБуквы в обратном порядке:");
                for (int i = 25; i >= 0; i--)
                {
                    fs.Seek(i, SeekOrigin.Begin);
                    Console.Write($"{(char)fs.ReadByte()} ");
                }
                Console.WriteLine();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка ввода/вывода: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Общая ошибка: {ex.Message}");
            }
            finally
            {
                fs?.Close();
            }
        }

        // 4. Копирование файла
        static void FileCopyOperations()
        {
            Console.WriteLine("\n--- Копирование файла ---");

            FileStream source = null;
            FileStream destination = null;

            try
            {
                // Создаем исходный файл с данными
                source = new FileStream("source_file.txt", FileMode.Create, FileAccess.Write);
                string sourceText = "Это исходный файл для демонстрации копирования.\n" +
                                  "Вторая строка файла.\n" +
                                  "Третья строка файла.";
                byte[] sourceData = Encoding.UTF8.GetBytes(sourceText);
                source.Write(sourceData, 0, sourceData.Length);
                source.Close();

                Console.WriteLine("Исходный файл создан.");

                // Копируем файл
                source = new FileStream("source_file.txt", FileMode.Open, FileAccess.Read);
                destination = new FileStream("destination_file.txt", FileMode.Create, FileAccess.Write);

                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                {
                    destination.Write(buffer, 0, bytesRead);
                }

                Console.WriteLine("Файл успешно скопирован.");

                // Показываем содержимое скопированного файла
                destination.Close();
                source.Close();

                StreamReader reader = new StreamReader("destination_file.txt", Encoding.UTF8);
                Console.WriteLine("\nСодержимое скопированного файла:");
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
                reader.Close();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Файл не найден: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка ввода/вывода: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Общая ошибка: {ex.Message}");
            }
            finally
            {
                source?.Close();
                destination?.Close();
            }
        }
    }
}
