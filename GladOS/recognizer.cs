//using Microsoft.Speech.Recognition;
//using System.Globalization;

//class recognizer
//{
//    static void Main()
//    {
//        CultureInfo culture = new CultureInfo("ru-ru");
//        SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(culture);

//        // Загрузка грамматики или использование свободного диктовки
//        Grammar dictationGrammar = new DictationGrammar();
//        recognizer.LoadGrammar(dictationGrammar);

//        recognizer.SpeechRecognized += (s, e) =>
//        {
//            Console.WriteLine($"Распознано: {e.Result.Text}");
//        }

//        recognizer.SetInputToDefaultAudioDevice();
//        recognizer.RecognizeAsync(RecognizeMode.Multiple);
//        Console.ReadLine();
//    }
//}
