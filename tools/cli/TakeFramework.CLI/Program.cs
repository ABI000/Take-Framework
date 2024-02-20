using System.CommandLine;
using System.CommandLine.Invocation;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand();

        return await rootCommand.InvokeAsync(args);
    }
}
