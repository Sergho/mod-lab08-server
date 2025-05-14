using System;
using System.Threading;

namespace Queuing;

class Server
{
	private PoolRecord[] pool;
	private object threadLock = new();
	private double serviceIntensity;
	private int poolSize;
	public int requestedCount = 0;
	public int processedCount = 0;
	public int rejectedCount = 0;

	public Server(double serviceIntensity, int poolSize)
	{
		pool = new PoolRecord[poolSize];
		this.serviceIntensity = serviceIntensity;
		this.poolSize = poolSize;
	}

	public void proc(object? sender, ProcEventArgs e)
	{
		lock (threadLock)
		{
			Console.WriteLine("Заявка с номером {0}", e.id);
			requestedCount++;
			for (int i = 0; i < poolSize; i++)
			{
				if (!pool[i].inUse)
				{
					pool[i].inUse = true;
					pool[i].thread = new Thread(new ParameterizedThreadStart(Answer));
					pool[i].thread.Start(e.id);
					processedCount++;
					return;
				}
			}
			rejectedCount++;
		}
	}
	public void Answer(object? arg)
	{
		int? id = (int?)arg;
		Console.WriteLine("Обработка заявки: {0}", id);
		Thread.Sleep((int)(1000 / serviceIntensity));
		for (int i = 0; i < poolSize; i++)
		{
			if (pool[i].thread == Thread.CurrentThread)
			{
				pool[i].inUse = false;
			}
		}
	}
	public int getUsedCount()
	{
		int counter = 0;
		foreach (PoolRecord record in pool)
		{
			if (record.inUse) counter++;
		}
		return counter;
	}
}