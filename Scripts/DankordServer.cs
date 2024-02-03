using System.Net.Sockets;
using System.Net;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TCP_Test.Scripts
{
	public class DankordServer(string Name, IPAddress IP_Address, int Port) : DankordBase(Name, IP_Address, Port)
	{
		public List<DankordClient>? ClientPool;

		public override async void Run()
		{
			Console.WriteLine("Running as Server...");
			var ipEndPoint = new IPEndPoint(IP_Address, Port);
			TcpListener listener = new(ipEndPoint);

			try
			{
				listener.Start();
				Console.WriteLine("Listener started, awaiting connections...");
				while (true)
				{
					tcpClient = await listener.AcceptTcpClientAsync();
					Console.WriteLine("Accepted client connection!");
					tcpClient.ReceiveTimeout = 30000;
					tcpClient.SendTimeout = 30000;
					Console.WriteLine("Sending Message...");
					///await SendMessage("Hi");
					Console.ReadLine();
				}
			}
			finally
			{
				listener.Stop();
				Console.WriteLine("Client disconnected.");
			}
		}

		public override async void SendMessage(string message)
		{
			await using NetworkStream stream = tcpClient.GetStream();
			var messageInBytes = Encoding.UTF8.GetBytes(message);
			await stream.WriteAsync(messageInBytes);
			Console.WriteLine("Message sent to client!");
		}

		public override async void ReceiveMessage()
		{
			foreach (DankordClient client in ClientPool)
			{
				//SendMessage();
			}
		}

		public override async void Connect()
		{

		}

		public override async void Disconnect()
		{

		}
	}
}