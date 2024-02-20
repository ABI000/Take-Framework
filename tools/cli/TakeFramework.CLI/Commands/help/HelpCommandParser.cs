using System.CommandLine;

namespace TakeFramework.CLI.Help;

public static class HelpCommandParser
{
    public static readonly string DocsLink = "https://aka.ms/dotnet-help";

    public static readonly Argument<string> Argument = new(LocalizableStrings.CommandArgumentName)
    {
        Description = LocalizableStrings.CommandArgumentDescription,
        Arity = ArgumentArity.ZeroOrOne
    };

    private static readonly Command Command = ConstructCommand();

    public static Command GetCommand()
    {
        return Command;
    }

    private static Command ConstructCommand()
    {
        Command command = new("help", DocsLink, LocalizableStrings.AppFullName);

        // command.Arguments.Add(Argument);

        // command.SetAction(HelpCommand.Run);

        return command;
    }
}


