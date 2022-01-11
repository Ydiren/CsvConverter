using System.Collections.Generic;
using Bogus;
using Common.Models;

namespace CsvConverter.Tests.MockData;

class MockDataGenerator
{
    public IEnumerable<CsvRow> GenerateCustomerCsvRows(int count)
    {
        var customers = new Faker<CsvRow>().RuleFor(x => x.Name,
                                                    f => f.Person.FullName)
                                           .RuleFor(x => x.AddressLine1,
                                                    f => f.Address.StreetAddress())
                                           .RuleFor(x => x.AddressLine2,
                                                    f => f.Address.City())
                                           .RuleFor(x => x.AddressLine3,
                                                    f => f.Address.Country());

        return customers.Generate(count);
    }

    public IEnumerable<PersonDetail> GeneratePeopleDetails(int count)
    {
        var peopleDetails = new Faker<PersonDetail>().RuleFor(x => x.Name,
                                                              f => f.Person.FullName)
                                                     .RuleForType(typeof(AddressDetail),
                                                                  f => new AddressDetail
                                                                       {
                                                                           Line1 = f.Address.StreetAddress(),
                                                                           Line2 = f.Address.City(),
                                                                           Line3 = f.Address.Country()
                                                                       });
        return peopleDetails.Generate(count);
    }
}