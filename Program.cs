using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Xml;
using System.Net.Security;
using System.Runtime.CompilerServices;



// Variables -------------------------------------------
string[] arguments = Environment.GetCommandLineArgs();
// -----------------------------------------------------

// Main

LaunchType launchType;

if (arguments == null)
{
	Console.WriteLine("Select program type: Server [s] or Client [c]");
	launchType = SelectProcessType(Console.ReadKey().KeyChar.ToString());
}
else
	launchType = SelectProcessType(args[0]);

try
{
	switch (launchType)
	{
		case LaunchType.Server:
			RunServer();
			break;
		case LaunchType.Client:
			RunClient();
			break;
		default:
			Console.WriteLine("Indubitably incorrect");
			break;
	}
}
catch (Exception e)
{
	Console.WriteLine(e.Message);
}

string input;
string[] inputArgs;

do
{
	input = Console.ReadLine().Trim();
	inputArgs = input.Split(" ");
	switch (inputArgs[0])
	{
		case "run":
			break;
		case "send":

			break;
	}
} while (inputArgs[0] != "exit");









// Functions

LaunchType SelectProcessType(string arg)
{
	return arg switch
	{
		"s" => LaunchType.Server,
		"c" => LaunchType.Client,
		_ => throw new ArgumentException("SPATNY ARGUMENT!!!!!!!!!!!!!!!!!!!!!! ZABIJ SE"),
	};
}

async void RunServer()
{
	Console.WriteLine("Running as Server...");
	var ipEndPoint = new IPEndPoint(IPAddress.Any, 25565);
	TcpListener listener = new(ipEndPoint);

	try
	{
		listener.Start();
		Console.WriteLine("Listener started, awaiting connections...");
		while (true)
		{
			using TcpClient handler = await listener.AcceptTcpClientAsync();
			Console.WriteLine("Client requesting to connect...");
			handler.ReceiveTimeout = 30000;
			handler.SendTimeout = 30000;
			Console.WriteLine("Accepting request...");
			await using NetworkStream stream = handler.GetStream();
			var message = $"📅 {DateTime.Now} 🕛";
			var dateTimeBytes = Encoding.UTF8.GetBytes(message);
			await stream.WriteAsync(dateTimeBytes);
			Console.WriteLine("Message sent to client!");
			Console.ReadLine();
		}

		//Console.WriteLine($"Danek: \"{message}\"");
		// Sample output:
		//     Sent message: "📅 8/22/2022 9:07:17 AM 🕛"
	}
	finally
	{
		listener.Stop();
		Console.WriteLine("Client disconnected.");
	}
}

async void RunClient()
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

	while (true)
	{
		Console.WriteLine("Receiving message from server...");
		await using NetworkStream stream = client.GetStream();

		var buffer = new byte[1_024];
		int received = await stream.ReadAsync(buffer);
		string message = Encoding.UTF8.GetString(buffer);
		Console.WriteLine(message);
	}
	client.Close();
	Console.WriteLine("Disconnected from server.");
}

enum LaunchType
{
	Server,
	Client
}