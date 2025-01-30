using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.VisualBasic.Devices;

public class StressTest
{
    public void Run()
    {
        using (var streamWriter = new StreamWriter("log.txt"))
        {
            var startTime = DateTime.Now;  // Время начала теста
            var count = 0;
            var elapsedSeconds = 0;
            const double gigabyteInByte = 0.000000000931322574615478515625;
            var currentProcess = Process.GetCurrentProcess();
            var computerInfo = new ComputerInfo();

            while (true)
            {
                elapsedSeconds = (int)(DateTime.Now - startTime).TotalSeconds; // Округляем время до целых секунд

                var usedMemory = (computerInfo.TotalPhysicalMemory - computerInfo.AvailablePhysicalMemory) * gigabyteInByte;
                var cpuUsage = currentProcess.TotalProcessorTime.TotalMilliseconds;

                string logEntry = $"{++count}\t{elapsedSeconds} sec\t{usedMemory:F3} GB\t{cpuUsage} ms CPU";
                Console.WriteLine(logEntry);
                streamWriter.WriteLine(logEntry);
                streamWriter.Flush();

                // Задержка 100 мс (чтобы было 10 записей в секунду)
                Thread.Sleep(100);
            }
        }
    }
}
