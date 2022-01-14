using System.Text;
using CsvConverter.Repositories;
using McMaster.Extensions.CommandLineUtils;

namespace CsvConverter;

public interface ICustomHelpTextGenerator
{
    void AddToCommandLine(CommandLineApplication application);
}

internal class CustomHelpTextGenerator : ICustomHelpTextGenerator
{
    private readonly IReaderRepository _readerRepository;
    private readonly IWriterRepository _writerRepository;

    public CustomHelpTextGenerator(IReaderRepository readerRepository,
                                   IWriterRepository writerRepository)
    {
        _readerRepository = readerRepository;
        _writerRepository = writerRepository;
    }

    public void AddToCommandLine(CommandLineApplication application)
    {
        var inputTypes = new StringBuilder(_readerRepository.SupportedTypes.Count());
        inputTypes.AppendJoin(", ", _readerRepository.SupportedTypes);
        var outputTypes = new StringBuilder(_writerRepository.SupportedTypes.Count());
        outputTypes.AppendJoin(", ", _writerRepository.SupportedTypes);

        application.ExtendedHelpText = @$"
    Accepted Input Types:
        {inputTypes}

    Accepted Output Types:
        {outputTypes}";
    }
}