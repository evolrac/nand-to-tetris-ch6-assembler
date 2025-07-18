using Assembler;
using System.CommandLine;
using System.CommandLine.Parsing;

namespace CommandLine;

public class CommandLineApp : ICommandLineApp
{
    private readonly IParser _parser;

    public CommandLineApp(IParser parser)
    {
        _parser = parser;
    }

    public int Run(string[] args)
    {
        Console.Error.WriteLine("App started");
        RootCommand rootCommand = new("Commandline app for generating machine code from assembler");
        Option<FileInfo> inputFileOption = new("--Inputfile")
        {
            Description = "Path to the assembler input file"
        };
        rootCommand.Options.Add(inputFileOption);
        Option<FileInfo> outputFileOption = new("--Outputfile")
        {
            Description = "Path to the machine code output file"
        };
        rootCommand.Options.Add(outputFileOption);
        ParseResult parseResult = rootCommand.Parse(args);
        if (parseResult.GetValue(inputFileOption) is FileInfo inputParsedFile &&
            parseResult.GetValue(outputFileOption) is FileInfo outputParsedFile)
        {
            Console.WriteLine("Successfully parsed input and output file options.");
            try
            {
                string[] inputLines = File.ReadAllLines(inputParsedFile.FullName);
                List<string> machineCode = _parser.Parse(inputLines.ToList());
                File.WriteAllLines(outputParsedFile.FullName, machineCode);
                Console.WriteLine($"Machine code written to {outputParsedFile.FullName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing files: {ex.Message}");
                return 1;
            }
            return 0;
        }

        foreach (ParseError parseError in parseResult.Errors)
        {
            Console.Error.WriteLine(parseError.Message);
        }
        Console.Error.WriteLine("Failed to parse command line arguments.");
        return 1;
    }
}