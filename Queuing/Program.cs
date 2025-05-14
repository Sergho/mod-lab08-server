namespace Queuing;

class Program
{
	static void Main()
	{
		FlowAnalyzer analyzer = new(20, 2, 5);
		AnalysisResult expected = analyzer.getExpected();
		Console.WriteLine("Ожидаемая вероятность простоя системы: {0}", expected.downtimeProbability);
		Console.WriteLine("Ожидаемая вероятность отказа системы: {0}", expected.failureProbability);
		Console.WriteLine("Ожидаемая относительная пропускная способность: {0}", expected.relativeThroughput);
		Console.WriteLine("Ожидаемая абсолютная пропускная способность: {0}", expected.absoluteThroughput);
		Console.WriteLine("Ожидаемое среднее число занятых каналов: {0}", expected.averageUsedCount);

		Console.WriteLine("\n");
		AnalysisResult actual = analyzer.getActual();
		Console.WriteLine("Фактическая вероятность простоя системы: {0}", actual.downtimeProbability);
		Console.WriteLine("Фактическая вероятность отказа системы: {0}", actual.failureProbability);
		Console.WriteLine("Фактическая относительная пропускная способность: {0}", actual.relativeThroughput);
		Console.WriteLine("Фактическая абсолютная пропускная способность: {0}", actual.absoluteThroughput);
		Console.WriteLine("Фактическое среднее число занятых каналов: {0}", actual.averageUsedCount);
	}
}