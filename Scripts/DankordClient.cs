using System.Net.Sockets;
using System.Net;
using System.Text;

namespace TCP_Test.Scripts
{
	public class DankordClient(string Name, IPAddress IP_Address, int Port) : DankordBase(Name, IP_Address, Port)
	{
		public override async void Run()
		{
			Console.WriteLine("Running as Client...");
			var ipAddress = IPAddress.Parse("185.82.239.12");
			var localAdress = IPAddress.Parse("127.0.0.1");
			var ipEndPoint = new IPEndPoint(localAdress, 25565);

			Console.WriteLine("Creating new Client...");
			using TcpClient client = new();
			client.ReceiveTimeout = 30000;
			client.SendTimeout = 30000;
			Console.WriteLine("Awaiting server...");
			await client.ConnectAsync(ipEndPoint);
			Console.WriteLine("Connected to server!");

			await using NetworkStream stream = client.GetStream();
			Console.WriteLine("Receiving message from server...");

			var buffer = new byte[1_024];
			int received = await stream.ReadAsync(buffer);
			Console.WriteLine("Received message from server!");
			string message = Encoding.UTF8.GetString(buffer);
			Console.WriteLine(message);

			client.Close();
			Console.WriteLine("Disconnected from server.");
		}

		public override async void SendMessage(string message)
		{

		}

		public override async void ReceiveMessage()
		{

		}

		public override async void Connect()
		{

		}

		public override async void Disconnect()
		{

		}
	}
}