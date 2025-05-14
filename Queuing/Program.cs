namespace Queuing;

class Program
{
	static readonly string filename = "data.txt";
	static readonly int poolSize = 5;
	static readonly double serviceIntesity = 100;
	static readonly double minQueryIntensity = 50;
	static readonly double maxQueryIntensity = 1000;
	static readonly double queryIntensityStep = 50;
	static void Main()
	{
		File.WriteAllText(filename, "");
		for (double i = minQueryIntensity; i <= maxQueryIntensity; i += queryIntensityStep)
		{
			FlowAnalyzer analyzer = new(i, serviceIntesity, poolSize);
			Console.WriteLine("Query intensity: {0}, Service intensity: {1}", i, serviceIntesity);

			AnalysisResult actual = analyzer.getActual();
			AnalysisResult expected = analyzer.getExpected();

			string result = string.Format("{0:F2} {0:F2}: ", i, serviceIntesity);
			result += string.Format("{0:F5} {0:F5}, ", actual.downtimeProbability, expected.downtimeProbability);
			result += string.Format("{0:F5} {0:F5}, ", actual.failureProbability, expected.failureProbability);
			result += string.Format("{0:F5} {0:F5}, ", actual.relativeThroughput, expected.relativeThroughput);
			result += string.Format("{0:F5} {0:F5}, ", actual.absoluteThroughput, expected.absoluteThroughput);
			result += string.Format("{0:F5} {0:F5}", actual.averageUsedCount, expected.averageUsedCount);

			File.AppendAllText(filename, result);
		}
	}
}