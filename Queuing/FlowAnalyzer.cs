namespace Queuing;

class FlowAnalyzer
{
	private double queryIntensity;
	private double serviceIntensity;
	private double reducedIntesity;
	private int poolSize;

	public FlowAnalyzer(double queryIntensity, double serviceIntensity, int poolSize)
	{
		this.queryIntensity = queryIntensity;
		this.serviceIntensity = serviceIntensity;
		reducedIntesity = queryIntensity / serviceIntensity;
		this.poolSize = poolSize;
	}
	public AnalysisResult getExpected()
	{
		AnalysisResult result = new();
		result.downtimeProbability = getExpectedDowntimeProbability();
		result.failureProbability = getExpectedFailureProbability();
		result.relativeThroughput = getExpectedRelativeThroughput();
		result.absoluteThroughput = getExpectedAbsoluteThroughput();
		result.averageUsedCount = getExpectedAverageUsedCount();
		return result;
	}
	private double getExpectedDowntimeProbability()
	{
		double sum = 0;
		for (int i = 0; i <= poolSize; i++)
		{
			int factorial = 1;
			for (int j = 1; j <= i; j++) factorial *= j;
			sum += Math.Pow(reducedIntesity, i) / factorial;
		}
		return Math.Pow(sum, -1);
	}
	private double getExpectedFailureProbability()
	{
		double downTimeProbability = getExpectedDowntimeProbability();
		int factorial = 1;
		for (int i = 1; i <= poolSize; i++) factorial *= i;
		return Math.Pow(reducedIntesity, poolSize) / factorial * downTimeProbability;
	}
	private double getExpectedRelativeThroughput()
	{
		double failureProbability = getExpectedFailureProbability();
		return 1 - failureProbability;
	}
	private double getExpectedAbsoluteThroughput()
	{
		double relative = getExpectedRelativeThroughput();
		return queryIntensity * relative;
	}
	private double getExpectedAverageUsedCount()
	{
		double throughput = getExpectedAbsoluteThroughput();
		return throughput / serviceIntensity;
	}
}