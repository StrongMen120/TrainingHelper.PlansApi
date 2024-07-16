
using Microsoft.EntityFrameworkCore;
using NodaTime;
using NodaTime.Extensions;
using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Persistance.TrainingsDb;
using Training.API.Plans.Persistance.TrainingsDb.Entities;
using Training.Common.Hexagon.Core.Exceptions;
using Training.Common.Hexagon.Core.Repositories;
using Training.Common.Hexagon.Persistance;
using Training.Common.Hexagon.Persistance.Exceptions;

namespace Training.API.Plans.Persistance.Repositories;
internal class TestEmployersRepository : ITestEmployersRepository
{
    private IPersistanceMapper mapper;
    private TrainingDbContext TrainingDbContext;
    private readonly IAuthenticationDetailsProvider authenticationDetailsProvider;

    public TestEmployersRepository(IPersistanceMapper mapper, TrainingDbContext trainingDatabase, IAuthenticationDetailsProvider authenticationDetailsProvider)
    {
        this.mapper = mapper;
        this.authenticationDetailsProvider = authenticationDetailsProvider;
        this.TrainingDbContext = trainingDatabase;
    }

    public async Task<IEnumerable<TestEmployersModel>> SearchAsync(ITestEmployersRepository.SearchTestEmployers searchModel)
    {
        try
        {
            var entity = await this.TrainingDbContext.TestEmployers.Take(searchModel.count).ToListAsync();

            return this.mapper.Map<IEnumerable<TestEmployersModel>>(entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while searching Test Employers, see inter exception for details.", ex);
        }
    }

    public async Task<TestEmployersModel> CreateAsync(ITestEmployersRepository.CreateTestEmployers creationModel)
    {
        try
        {
            var entry = await this.TrainingDbContext.TestEmployers.AddAsync(new(){
                Address = creationModel.testEmployers.Address,
                DateOfBirth = creationModel.testEmployers.DateOfBirth,
                Department = creationModel.testEmployers.Department,
                Email = creationModel.testEmployers.Email,
                PhoneNumber = creationModel.testEmployers.PhoneNumber,
                FirstName = creationModel.testEmployers.FirstName,
                LastName = creationModel.testEmployers.LastName,
                HireDate = creationModel.testEmployers.HireDate,
                Id = default,
                JobTitle = creationModel.testEmployers.JobTitle,
                Password = creationModel.testEmployers.Password,
                Salary = creationModel.testEmployers.Salary
            });
            await this.TrainingDbContext.SaveChangesAsync();

            return this.mapper.Map<TestEmployersModel>(entry.Entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while create TestEmployers, see inter exception for details.", ex);
        }
    }

    public async Task<TestEmployersModel> UpdateAsync(ITestEmployersRepository.UpdateTestEmployers updateModel)
    {
        try
        {
            var entry = await this.TrainingDbContext.TestEmployers
                .FirstOrDefaultAsync(a => a.Id == updateModel.testEmployers.Id);
            if (entry == default) throw new NotFoundException($"TestEmploy with id: '{updateModel.testEmployers.Id}' not found!");

            entry.Address = updateModel.testEmployers.Address;
            entry.DateOfBirth = updateModel.testEmployers.DateOfBirth;
            entry.Department = updateModel.testEmployers.Department;
            entry.Email = updateModel.testEmployers.Email;
            entry.PhoneNumber = updateModel.testEmployers.PhoneNumber;
            entry.FirstName = updateModel.testEmployers.FirstName;
            entry.LastName = updateModel.testEmployers.LastName;
            entry.HireDate = updateModel.testEmployers.HireDate;
            entry.JobTitle = updateModel.testEmployers.JobTitle;
            entry.Password = updateModel.testEmployers.Password;
            entry.Salary = updateModel.testEmployers.Salary;
            this.TrainingDbContext.TestEmployers.Update(entry);
            await this.TrainingDbContext.SaveChangesAsync();
            return this.mapper.Map<TestEmployersModel>(entry);

        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while updated TestEmployers, see inter exception for details.", ex);
        }
    }

    public async Task<TestEmployersModel> DeleteByIdAsync(ITestEmployersRepository.DeleteTestEmployers id)
    {
        try
        {
            var entity = await this.TrainingDbContext.TestEmployers
                .FirstOrDefaultAsync(e => e.Id == id.Id);

            if (entity == default) throw new NotFoundException($"TestEmploye with id: '{id.Id}' not found!");

            this.TrainingDbContext.TestEmployers.Remove(entity);
            await this.TrainingDbContext.SaveChangesAsync();

            return this.mapper.Map<TestEmployersModel>(entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while deleted Plans, see inter exception for details.", ex);
        }
    }

    public async Task<TestEmployersModel> DeleteAsync(TestEmployersModel model)=> (await this.DeleteByIdAsync(new ITestEmployersRepository.DeleteTestEmployers(model.Id))) ?? throw new NotFoundException($"TestEmploy with id: '{model.Id}' not found!");
}
