using System;
using Vosk;
using NAudio.Wave;
using System.Threading;

class Program
{
    static void ListAudioDevices()
    {
        for (int i = 0; i < WaveIn.DeviceCount; i++)
        {
            var caps = WaveIn.GetCapabilities(i);
            Console.WriteLine($"Устройство {i}: {caps.ProductName}");
        }
    }
    static void Main()
    {
        Console.WriteLine("=== Распознавание речи Vosk ===");

        // Укажите путь к вашей модели
        string modelPath = @"D:\vosk-model-ru-0.42";
        Program.ListAudioDevices();
        try
        {
            using (var recognizer = new SpeechRecognizerService(modelPath))
            {
                recognizer.OnResult += (text) =>
                {
                    if (!string.IsNullOrEmpty(text))
                        Console.WriteLine($"Распознано: {text}");
                };

                recognizer.OnPartialResult += (text) =>
                {
                    // Можно выводить частичные результаты, но они часто меняются
                    //Console.WriteLine($"... {text}");
                };

                Console.WriteLine("Нажмите Enter чтобы начать распознавание...");
                Console.ReadLine();

                recognizer.StartRecognition();

                Console.WriteLine("Слушаю... Нажмите Enter чтобы остановить.");
                Console.ReadLine();

                recognizer.StopRecognition();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}

public class SpeechRecognizerService : IDisposable
{
    private Model _model;
    private VoskRecognizer _recognizer;
    private WaveInEvent _waveIn;

    public event Action<string> OnResult;
    public event Action<string> OnPartialResult;

    public SpeechRecognizerService(string modelPath)
    {
        Console.WriteLine("Загрузка модели...");
        _model = new Model(modelPath);
        _recognizer = new VoskRecognizer(_model, 16000.0f);
        _recognizer.SetWords(true);

        SetupAudioInput();
        Console.WriteLine("Модель загружена успешно!");
    }

    private void SetupAudioInput()
    {
        _waveIn = new WaveInEvent();
        _waveIn.DeviceNumber = 0; // Используем устройство по умолчанию
        _waveIn.WaveFormat = new WaveFormat(16000, 2); // 16kHz, моно
        _waveIn.BufferMilliseconds = 100; // Буфер 100ms
        _waveIn.DataAvailable += OnDataAvailable;
    }

    public void StartRecognition()
    {
        if (_waveIn != null)
        {
            _waveIn.StartRecording();
        }
    }

    public void StopRecognition()
    {
        if (_waveIn != null)
        {
            _waveIn.StopRecording();
            // Получаем финальный результат
            string finalResult = _recognizer.FinalResult();
            if (!string.IsNullOrEmpty(finalResult))
            {
                Console.WriteLine($"Финальный результат: {finalResult}");
            }
        }
    }

    private void OnDataAvailable(object sender, WaveInEventArgs e)
    {
        try
        {
            if (_recognizer.AcceptWaveform(e.Buffer, e.BytesRecorded))
            {
                // Получен окончательный результат (после паузы)
                string result = _recognizer.Result();
                OnResult?.Invoke(ExtractTextFromJson(result));
            }
            else
            {
                // Частичный результат (распознавание в реальном времени)
                string partial = _recognizer.PartialResult();
                OnPartialResult?.Invoke(ExtractTextFromJson(partial));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при обработке аудио: {ex.Message}");
        }
    }

    private string ExtractTextFromJson(string json)
    {
        try
        {
            // Простой парсинг JSON для извлечения текста
            int textIndex = json.IndexOf("\"text\"") + 8;
            int endIndex = json.IndexOf("\"", textIndex);
            if (textIndex >= 8 && endIndex > textIndex)
            {
                return json.Substring(textIndex, endIndex - textIndex);
            }
        }
        catch
        {
            // В случае ошибки парсинга возвращаем пустую строку
        }
        return string.Empty;
    }

    public void Dispose()
    {
        _waveIn?.StopRecording();
        _waveIn?.Dispose();
        _recognizer?.Dispose();
        _model?.Dispose();
    }
}