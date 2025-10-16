//using System;
//using System.Diagnostics;
//using System.IO;
//using System.Reflection;
//using System.Globalization;

//class Program
//{
//    static void Main(string[] args)
//    {
//        try
//        {
//            // Получение относительного пути к скрипту Python
//            string pythonScriptPath = FindPythonScript("PythonApplication", "main.py");

//            // Проверка наличия скрипта Python
//            if (string.IsNullOrEmpty(pythonScriptPath) || !File.Exists(pythonScriptPath))
//            {
//                Console.WriteLine($"Ошибка: скрипт Python не найден по пути: {pythonScriptPath}");
//                return;
//            }

//            // Путь к интерпретатору Python в виртуальном окружении
//            string pythonInterpreterPath = FindPythonInterpreter("PythonApplication", "myenv", "Scripts", "python.exe");

//            // Проверка наличия интерпретатора Python
//            if (string.IsNullOrEmpty(pythonInterpreterPath) || !File.Exists(pythonInterpreterPath))
//            {
//                Console.WriteLine($"Ошибка: интерпретатор Python не найден по пути: {pythonInterpreterPath}");
//                return;
//            }

//            // Числа для сложения
//            double num1 = 5.5;
//            double num2 = 4.5;

//            // Преобразование чисел в строки с использованием invariant culture (точка как десятичный разделитель)
//            string num1Str = num1.ToString(CultureInfo.InvariantCulture);
//            string num2Str = num2.ToString(CultureInfo.InvariantCulture);

//            // Начало измерения времени выполнения
//            Stopwatch stopwatch = new Stopwatch();
//            stopwatch.Start();

//            // Настройка процесса для вызова скрипта Python
//            ProcessStartInfo start = new ProcessStartInfo();
//            start.FileName = pythonInterpreterPath;
//            start.Arguments = $"{pythonScriptPath} {num1Str} {num2Str}";
//            start.WorkingDirectory = Path.GetDirectoryName(pythonScriptPath);
//            start.UseShellExecute = false;
//            start.RedirectStandardOutput = true;
//            start.RedirectStandardError = true; // Захват стандартного вывода ошибок
//            start.CreateNoWindow = true;

//            using (Process process = Process.Start(start))
//            {
//                using (StreamReader reader = process.StandardOutput)
//                {
//                    string result = reader.ReadToEnd();
//                    Console.WriteLine($"Результат: {result}");
//                }

//                using (StreamReader errorReader = process.StandardError)
//                {
//                    string error = errorReader.ReadToEnd();
//                    if (!string.IsNullOrEmpty(error))
//                    {
//                        Console.WriteLine($"Ошибка: {error}");
//                    }
//                }

//                process.WaitForExit();
//                int exitCode = process.ExitCode;
//                Console.WriteLine($"Процесс завершился с кодом: {exitCode}");
//            }

//            // Остановка измерения времени выполнения
//            stopwatch.Stop();
//            TimeSpan ts = stopwatch.Elapsed;
//            Console.WriteLine($"Время выполнения: {ts.TotalMilliseconds} мс");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Исключение: {ex.Message}");
//            Console.WriteLine($"Стек вызовов: {ex.StackTrace}");
//        }
//    }

//    static string FindPythonScript(string directory, string scriptName)
//    {
//        return FindFile(directory, scriptName);
//    }

//    static string FindPythonInterpreter(string directory, string venvDirectory, string scriptsDirectory, string pythonExe)
//    {
//        return FindFile(directory, Path.Combine(venvDirectory, scriptsDirectory, pythonExe));
//    }

//    static string FindFile(string directory, string fileName)
//    {
//        // Получение начального каталога сборки
//        string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

//        while (currentDirectory != null)
//        {
//            string foundPath = SearchDirectory(currentDirectory, directory, fileName);
//            if (foundPath != null)
//            {
//                return foundPath;
//            }

//            // Переход в родительский каталог
//            currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
//        }

//        // Если файл не найден, вернуть null
//        return null;
//    }

//    static string SearchDirectory(string baseDirectory, string targetDirectory, string fileName)
//    {
//        // Получение полного пути к целевой директории
//        string targetPath = Path.Combine(baseDirectory, targetDirectory);

//        if (Directory.Exists(targetPath))
//        {
//            string filePath = Path.Combine(targetPath, fileName);
//            if (File.Exists(filePath))
//            {
//                return filePath;
//            }

//            // Рекурсивный поиск в подкаталогах
//            foreach (string subDirectory in Directory.GetDirectories(targetPath))
//            {
//                string foundPath = SearchDirectory(subDirectory, string.Empty, fileName);
//                if (foundPath != null)
//                {
//                    return foundPath;
//                }
//            }
//        }

//        return null;
//    }
//}