using System.Net;
using System.Net.Sockets;
using System.Text;
using TCP_Test;
using TCP_Test.Scripts;

// Main Entry Point
var dankord = new Dankord();
dankord.Run();

public class Dankord
{
	public static DankordBase instance;

	public LaunchType launchType;

	private string[]? arguments;

	private void GetRunSettings()
	{
		arguments = Environment.GetCommandLineArgs();
		Console.WriteLine(arguments[1]);
		if (arguments == null)
		{
			Console.WriteLine("Select program type: Server [s] or Client [c]");
			arguments[1] = Console.ReadKey().KeyChar.ToString();
		}
		else
		{
			Console.WriteLine($"Console argument parsed: {arguments[1]}");
		}

		if (arguments[1] == "s")
			launchType = LaunchType.Server;
		else if (arguments[1] == "c")
			launchType = LaunchType.Client;
		else
			throw new ArgumentException("The launch argument may only be 's' or 'c'.");
	}

	public void Run()
	{
		GetRunSettings();

		string? input;
		string[]? inputArgs;

		if (launchType == LaunchType.Server)
			instance = new DankordServer(
					Name: "Dankuv super server",
					IP_Address: IPAddress.Any,
					Port: 25565
				);

		else if (launchType == LaunchType.Client)
			instance = new DankordClient(
					Name: "Danek",
					IP_Address: IPAddress.Parse("127.0.0.1"),
					Port: 25565
				);

		instance.Run();

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

				default:
					Console.WriteLine($"{inputArgs[0]} is not a valid command.");
					break;
			}
		} while (inputArgs[0] != "exit");
	}
}