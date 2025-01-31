using Bench;
using Microsoft.VisualBasic.Devices;
using System.Diagnostics;

class Program
{
	/// <summary>
	/// Точка входа.
	/// </summary>
	/// <param name="args">Аргументы.</param>
	private static void Main(string[] args)
	{
		var stopWatch = new Stopwatch();
		var benchParameters = new BenchParameters(150, 20, 30, 40, 40);
		var compassWrapper = new BenchWrapper();
		var benchBuilder = new BenchBuilder(compassWrapper);

		var streamWriter = new StreamWriter("log.txt", true);
		var count = 0;
		while (true)
		{
			stopWatch.Start();
			const double gigabyteInByte = 0.000000000931322574615478515625;
			compassWrapper.ConnectToKompas();
			benchBuilder.BuildBench(benchParameters);

			var computerInfo = new ComputerInfo();
			var usedMemory = (computerInfo.TotalPhysicalMemory
							  - computerInfo.AvailablePhysicalMemory)
							 * gigabyteInByte;
			var logEntry = $"{++count}\t{stopWatch.Elapsed:hh\\:mm\\:ss}\t{usedMemory}";
			streamWriter.WriteLine(logEntry);
			Console.WriteLine(logEntry);
			streamWriter.Flush();
			stopWatch.Reset();
		}
	}
}
