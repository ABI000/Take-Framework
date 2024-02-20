using System.CommandLine.Parsing;

namespace TakeFramework.CLI;


public abstract class CommandBase
{
    protected ParseResult _parseResult;

    protected CommandBase(ParseResult parseResult)
    {
        _parseResult = parseResult;
        ShowHelpOrErrorIfAppropriate(parseResult);
    }

    protected virtual void ShowHelpOrErrorIfAppropriate(ParseResult parseResult)
    {
        parseResult.ShowHelpOrErrorIfAppropriate();
    }

    public abstract int Execute();

}
