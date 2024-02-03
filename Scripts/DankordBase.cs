using System.Net;
using System.Net.Sockets;

namespace TCP_Test.Scripts
{
	public abstract class DankordBase(string Name, IPAddress IP_Address, int Port)
	{
		public string Name = Name;
		public IPAddress IP_Address = IP_Address;
		public int Port = Port;

		public TcpClient tcpClient;

		public abstract void Run();

		public abstract void SendMessage(string message);

		public abstract void ReceiveMessage();

		public abstract void Connect();

		public abstract void Disconnect();
	}
}
