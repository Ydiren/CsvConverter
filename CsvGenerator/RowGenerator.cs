using Bogus;
using CsvGenerator.Models;

namespace CsvGenerator;

public interface IRowGenerator
{
    IEnumerable<CsvRow> GenerateCustomers(int count);
}

public class RowGenerator : IRowGenerator
{
    public IEnumerable<CsvRow> GenerateCustomers(int count)
    {
        var customers = new Faker<CsvRow>()
            .RuleFor(x => x.Name, f => f.Person.FullName)
            .RuleFor(x => x.AddressLine1, f => f.Address.StreetAddress())
            .RuleFor(x => x.AddressLine2, f =>  f.Address.City())
            .RuleFor(x => x.AddressLine3, f =>  f.Address.Country());
        
        return customers.Generate(count);
    }
}