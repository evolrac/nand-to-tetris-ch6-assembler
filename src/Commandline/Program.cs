
using Microsoft.Extensions.DependencyInjection;
using Assembler;
using CommandLine;

class Program
{
    static int Main(string[] args)
    {
        var commandLineApp = StartUp();
        return commandLineApp.Run(args);
    }

    static CommandLine.ICommandLineApp StartUp()
    {
        var services = new ServiceCollection();
        services.AddSingleton<ICodeGenerator, CodeGenerator>();
        services.AddSingleton<IParser, Parser>();
        services.AddSingleton<CommandLine.ICommandLineApp, CommandLine.CommandLineApp>();
        var serviceProvider = services.BuildServiceProvider();

        var commandLineApp = serviceProvider.GetService<ICommandLineApp>();
        if (commandLineApp == null)
        {
            Console.Error.WriteLine("Failed to create CommandLineApp instance.");
            Environment.Exit(1);
        }
        return commandLineApp;
    }
}

