using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Bench;
using Microsoft.VisualBasic.Devices;

public class StressTest
{
    public void Run()
    {
        var builder = new BenchBuilder(new BenchWrapper());
        var parameters = new BenchParameters(150, 17, 30, 35, 45);

        // Очищаем файл перед началом работы
        using (var streamWriter = new StreamWriter("log.txt", append: false))
        {
            streamWriter.AutoFlush = true;

            var count = 0;
            const double gigabyteInByte = 0.000000000931322574615478515625;
            var computerInfo = new ComputerInfo();
            var startTime = DateTime.Now;

            while (true)
            {
                var stopwatch = Stopwatch.StartNew();

                // Высчитываем текущее время работы программы
                TimeSpan elapsedTime = DateTime.Now - startTime;
                int elapsedSeconds = (int)elapsedTime.TotalSeconds;

                double usedMemory = (computerInfo.TotalPhysicalMemory - computerInfo.AvailablePhysicalMemory) * gigabyteInByte;
                string logEntry = $"{++count} 00:00:{elapsedSeconds:D2} {usedMemory:F9}";

                // Вывод в консоль и запись в файл
                Console.WriteLine(logEntry);
                Console.Out.Flush();

                streamWriter.WriteLine(logEntry);

                // Ограничение скорости (ждем 100 мс перед следующей итерацией)
                Thread.Sleep(100);

                stopwatch.Stop();
            }
        }
    }
}