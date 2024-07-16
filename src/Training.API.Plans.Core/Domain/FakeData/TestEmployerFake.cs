using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;

namespace Training.API.Plans.Core.Domain.FakeData;

public class TestEmployerFake
{
    private static Faker<TestEmployersModel> FakerTestEmployers()
    {
        var fakeData = new Faker<TestEmployersModel>("en")
            .CustomInstantiator(f => new TestEmployersModel(
                f.IndexVariable++,
                f.Name.FirstName(),
                f.Name.LastName(),
                f.Internet.Email(),
                f.Phone.PhoneNumber(),
                f.Date.Recent(),
                f.Commerce.Department(),
                f.Name.JobTitle(),
                f.Commerce.Price(),
                f.Date.Recent(),
                $"{f.Address.ZipCode()},{f.Address.StreetName()},{f.Address.City()},{f.Address.Country()}",
                f.Internet.Password()
            ));
        return fakeData;
    }
    public static List<TestEmployersModel> FakeListEmployersModel(int countFakeData)
    {
        var fakeData = FakerTestEmployers().Generate(countFakeData);
        return fakeData;
    }
    public static TestEmployersModel FakeEmployersModel()
    {
        var fakeData = FakerTestEmployers();
        return fakeData;
    }
}