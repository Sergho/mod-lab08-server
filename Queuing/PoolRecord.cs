namespace Queuing;

struct PoolRecord
{
	public Thread thread;
	public bool inUse;
}