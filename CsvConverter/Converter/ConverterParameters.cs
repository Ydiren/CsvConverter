namespace CsvConverter.Converter;

public class ConverterParameters
{
    public ConverterParameters(string input, string inputType, string output, string outputType)
    {
        Input = input;
        InputType = inputType;
        Output = output;
        OutputType = outputType;
    }

    public string Input { get; }
    public string InputType { get; }
    public string Output { get; }
    public string OutputType { get; }
}