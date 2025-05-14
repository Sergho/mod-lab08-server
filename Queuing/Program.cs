using ScottPlot;

namespace Queuing;

class Program
{
	static readonly string[] names = ["Вероятность простоя системы", "Вероятность отказа системы", "Относительная пропускная способность", "Абсолютная пропускная способность", "среднее число занятых каналов"];
	static readonly string dataFilename = "data.txt";
	static readonly string graphDirname = "result";
	static readonly int poolSize = 5;
	static readonly double serviceIntesity = 100;
	static readonly double minQueryIntensity = 50;
	static readonly double maxQueryIntensity = 1000;
	static readonly double queryIntensityStep = 50;
	static void Main()
	{
		File.WriteAllText(dataFilename, "");
		for (double i = minQueryIntensity; i <= maxQueryIntensity; i += queryIntensityStep)
		{
			FlowAnalyzer analyzer = new(i, serviceIntesity, poolSize);
			Console.WriteLine("Query intensity: {0}, Service intensity: {1}", i, serviceIntesity);

			AnalysisResult actual = analyzer.getActual();
			AnalysisResult expected = analyzer.getExpected();

			string result = string.Format("{0:F2} {1:F2}: ", i, serviceIntesity);
			result += string.Format("{0:F5} {1:F5}; ", actual.downtimeProbability, expected.downtimeProbability);
			result += string.Format("{0:F5} {1:F5}; ", actual.failureProbability, expected.failureProbability);
			result += string.Format("{0:F5} {1:F5}; ", actual.relativeThroughput, expected.relativeThroughput);
			result += string.Format("{0:F5} {1:F5}; ", actual.absoluteThroughput, expected.absoluteThroughput);
			result += string.Format("{0:F5} {1:F5}\n", actual.averageUsedCount, expected.averageUsedCount);

			File.AppendAllText(dataFilename, result);
		}

		for (int i = 0; i < names.Length; i++)
		{
			Plot plot = new();
			plot.Title(names[i]);
			plot.XLabel("Интенсивность потока заявок");
			plot.XLabel(names[i]);

			string[] lines = File.ReadAllLines(dataFilename);
			double[] x = lines.Select(line =>
			{
				string str = line.Split(":")[0].Split(" ")[0];
				return double.Parse(str);
			}).ToArray();

			double[] actualY = lines.Select(line =>
			{
				string str = line.Split(":")[1].Split("; ")[i].Trim().Split(" ")[0];
				return double.Parse(str);
			}).ToArray();

			double[] expectedY = lines.Select(line =>
			{
				string str = line.Split(":")[1].Split("; ")[i].Trim().Split(" ")[1];
				return double.Parse(str);
			}).ToArray();

			var actualScatter = plot.Add.Scatter(x, actualY);
			actualScatter.LegendText = "Actual";
			actualScatter.Color = Colors.Blue;
			actualScatter.MarkerSize = 7;
			actualScatter.LineWidth = 2;

			var expectedScatter = plot.Add.Scatter(x, expectedY);
			expectedScatter.LegendText = "Expected";
			expectedScatter.Color = Colors.Red;
			expectedScatter.MarkerSize = 7;
			expectedScatter.LineWidth = 2;

			plot.ShowLegend();

			Directory.CreateDirectory(graphDirname);
			plot.SavePng($"{graphDirname}/p-{i + 1}.png", 1200, 800);
		}
	}
}