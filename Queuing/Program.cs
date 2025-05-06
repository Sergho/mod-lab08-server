namespace Queuing;

class Program
{
	static void Main()
	{
		Server server = new();
		Client client = new(server);

		for (int id = 1; id <= 100; id++)
		{
			client.send(id);
			Thread.Sleep(50);
		}
		Console.WriteLine("Всего заявок: {0}", server.requestedCount);
		Console.WriteLine("Обработано заявок: {0}", server.processedCount);
		Console.WriteLine("Отклонено заявок: {0}", server.rejectedCount);
	}
}