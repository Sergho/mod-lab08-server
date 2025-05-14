namespace Queuing;

class Program
{
	static readonly int queryIntensity = 20;
	static readonly int serviceIntensity = 2;
	static readonly int poolSize = 5;
	static void Main()
	{
		FlowAnalyzer analyzer = new(20, 2, 5);
		AnalysisResult expected = analyzer.getExpected();
		Console.WriteLine("Ожидаемая вероятность простоя системы: {0}", expected.downtimeProbability);
		Console.WriteLine("Ожидаемая вероятность отказа системы: {0}", expected.failureProbability);
		Console.WriteLine("Ожидаемая относительная пропускная способность: {0}", expected.relativeThroughput);
		Console.WriteLine("Ожидаемая абсолютная пропускная способность: {0}", expected.absoluteThroughput);
		Console.WriteLine("Ожидаемое среднее число занятых каналов: {0}", expected.averageUsedCount);
	}
}