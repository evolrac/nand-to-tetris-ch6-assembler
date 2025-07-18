using Assembler;

namespace CommandLine;

public interface ICommandLineApp
{
    int Run(string[] args);
}
