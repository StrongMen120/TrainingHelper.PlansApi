
using NodaTime;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.Domain.Values;
using Training.Common.Hexagon.Core.Repositories;

namespace Training.API.Plans.Core.Abstraction.Repositories;

public interface ITestEmployersRepository  :
    ISearchRepository<ITestEmployersRepository.SearchTestEmployers, TestEmployersModel>,
    ICreateRepository<ITestEmployersRepository.CreateTestEmployers, TestEmployersModel>,
    IUpdateRepository<ITestEmployersRepository.UpdateTestEmployers, TestEmployersModel>,
    IDeleteRepository<ITestEmployersRepository.DeleteTestEmployers, TestEmployersModel>
{   

    #region Searches

    public record class SearchTestEmployers(int count);

    #endregion Searches

    #region Crete & Updates

    public record class CreateTestEmployers(TestEmployersModel testEmployers);
    public record class UpdateTestEmployers(TestEmployersModel testEmployers);

    #endregion Crete & Updates

    #region Delete

    public record class DeleteTestEmployers(long Id);

    #endregion Delete

    
}