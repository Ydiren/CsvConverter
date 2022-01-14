using Common.Models;

namespace CsvConverter.Csv.Factories;

internal interface IPersonDetailsFactory
{
    PersonDetail Create(CsvRow row);
}

internal class PersonDetailsFactory : IPersonDetailsFactory
{
    public PersonDetail Create(CsvRow row)
    {
        return new PersonDetail
               {
                   Name = row.Name,
                   Address = new AddressDetail
                             {
                                 Line1 = row.AddressLine1,
                                 Line2 = row.AddressLine2,
                                 Line3 = row.AddressLine3
                             }
               };
    }
}