namespace Queuing;

class Client
{
	private Server server;
	public event EventHandler<ProcEventArgs> request;

	public Client(Server server)
	{
		this.server = server;
		request += server.proc;
	}
	public void send(int id)
	{
		ProcEventArgs args = new();
		args.id = id;
		onProc(args);
	}
	protected virtual void onProc(ProcEventArgs e)
	{
		EventHandler<ProcEventArgs> handler = request;
		if (handler != null)
		{
			handler(this, e);
		}
	}
}