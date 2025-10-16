import pyaudio
import json
from vosk import Model, KaldiRecognizer

# Укажите путь к вашей распакованной модели
model = Model("vosk-model-small-ru-0.22")
recognizer = KaldiRecognizer(model, 16000)

# Настройка и запуск микрофона
p = pyaudio.PyAudio()
stream = p.open(format=pyaudio.paInt16, channels=1, rate=16000, input=True, frames_per_buffer=8192)
stream.start_stream()

print("Говорите...")

while True:
    data = stream.read(4096, exception_on_overflow=False)
    if recognizer.AcceptWaveform(data):
        # Получение и вывод окончательного результата
        result = json.loads(recognizer.Result())
        text = result["text"]
        if text:  # Проверка на пустую строку
            print("Распознано:", text)
