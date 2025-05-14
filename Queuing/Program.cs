namespace Queuing;

class Program
{
	static readonly int queryIntensity = 20;
	static readonly int serviceIntensity = 2;
	static readonly int poolSize = 5;
	static void Main()
	{
		FlowAnalyzer analyzer = new(20, 2, 5);
		Console.WriteLine("Ожидаемая вероятность простоя системы: {0}", analyzer.getExpectedDowntimeProbability());
		Console.WriteLine("Ожидаемая вероятность отказа системы: {0}", analyzer.getExpectedFailureProbability());
		Console.WriteLine("Ожидаемая относительная пропускная способность: {0}", analyzer.getExpectedRelativeThroughput());
		Console.WriteLine("Ожидаемая абсолютная пропускная способность: {0}", analyzer.getExpectedAbsoluteThroughput());
		Console.WriteLine("Ожидаемое среднее число занятых каналов: {0}", analyzer.getExpectedAverageUsed());
	}
}