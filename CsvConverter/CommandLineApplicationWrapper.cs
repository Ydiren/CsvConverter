using McMaster.Extensions.CommandLineUtils;

namespace CsvConverter;

public interface ICommandLineApplication
{
    void ShowHelp();
}

public class CommandLineApplicationWrapper : ICommandLineApplication
{
    private readonly CommandLineApplication _commandLineApplication;

    public CommandLineApplicationWrapper(CommandLineApplication commandLineApplication)
    {
        _commandLineApplication = commandLineApplication;

        _commandLineApplication.HelpOption();
    }

    public void ShowHelp()
    {
        _commandLineApplication.ShowHelp();
    }

}