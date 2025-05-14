namespace Queuing;

class Program
{
	static readonly int queryIntensity = 20;
	static readonly int serviceIntensity = 2;
	static void Main()
	{
		Server server = new(serviceIntensity);
		Client client = new(server);

		for (int id = 1; id <= 100; id++)
		{
			client.send(id);
			Thread.Sleep(1000 / queryIntensity);
		}
		Console.WriteLine("Всего заявок: {0}", server.requestedCount);
		Console.WriteLine("Обработано заявок: {0}", server.processedCount);
		Console.WriteLine("Отклонено заявок: {0}", server.rejectedCount);
	}
}