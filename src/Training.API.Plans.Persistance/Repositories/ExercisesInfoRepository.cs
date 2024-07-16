using Microsoft.EntityFrameworkCore;
using NodaTime;
using NodaTime.Extensions;
using Training.API.Plans.Core.Abstraction.Repositories;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Persistance.TrainingsDb;
using Training.API.Plans.Persistance.TrainingsDb.Entities;
using Training.Common.Hexagon.Core.Exceptions;
using Training.Common.Hexagon.Persistance;
using Training.Common.Hexagon.Persistance.Exceptions;

namespace Training.API.Plans.Persistance.Repositories;
internal class ExercisesInfoRepository : IExercisesInfoRepository
{
    private IPersistanceMapper mapper;
    private TrainingDbContext TrainingDbContext;
    private readonly IAuthenticationDetailsProvider authenticationDetailsProvider;

    public ExercisesInfoRepository(IPersistanceMapper mapper, TrainingDbContext trainingDatabase, IAuthenticationDetailsProvider authenticationDetailsProvider)
    {
        this.mapper = mapper;
        this.authenticationDetailsProvider = authenticationDetailsProvider;
        this.TrainingDbContext = trainingDatabase;
    }

    public async Task<ExercisesInfoModel> CreateAsync(IExercisesInfoRepository.CreateExerciseInfo creationModel)
    {
        try
        {
            var userInfo = await this.authenticationDetailsProvider.GetUserDetails();

            var newExerciseInfo = new ExerciseInfoEntity
            {
                Identifier = default,
                Name = creationModel.Name,
                AuthorId = creationModel.AuthorId,
                Description = creationModel.Description,
                BodyElements = String.Join(";", creationModel.BodyElements),
                CreatedAt = SystemClock.Instance.InTzdbSystemDefaultZone().GetCurrentOffsetDateTime().LocalDateTime,
                CreatedBy = new() { FullName = userInfo.FullName, Id = userInfo.Id },
            };

            var entry = await this.TrainingDbContext.AddAsync(newExerciseInfo);
            await this.TrainingDbContext.SaveChangesAsync();
            
            return this.mapper.Map<ExercisesInfoModel>(entry.Entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while create Exercise Info, see inter exception for details.", ex);
        }
    }

    public async Task<ExercisesInfoModel> DeleteAsync(ExercisesInfoModel model) => (await this.DeleteByIdAsync(new IExercisesInfoRepository.DeleteExerciseInfo(model.Identifier))) ?? throw new NotFoundException($"Exercises Info with id: '{model.Identifier}' not found!");

    public async Task<ExercisesInfoModel> DeleteByIdAsync(IExercisesInfoRepository.DeleteExerciseInfo id)
    {
        try
        {
            var entity = await this.TrainingDbContext.ExercisesInfo
                .FirstOrDefaultAsync(e => e.Identifier == id.ExerciseId);

            if (entity == default) throw new NotFoundException($"Exercises Info with id: '{id.ExerciseId}' not found!");

            this.TrainingDbContext.ExercisesInfo.Remove(entity);
            await this.TrainingDbContext.SaveChangesAsync();

            return this.mapper.Map<ExercisesInfoModel>(entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while deleted Exercise Info, see inter exception for details.", ex);
        }
    }

    public async Task<ExercisesInfoModel?> FindAsync(IExercisesInfoRepository.GetById getModel)
    {
        try
        {
            var entity = await this.TrainingDbContext.ExercisesInfo
                .AsQueryable()
                .Include(e => e.Files).FirstOrDefaultAsync(e => e.Identifier == getModel.ExerciseId);
            return this.mapper.Map<ExercisesInfoModel>(entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while getting Exercises Info, see inter exception for details.", ex);
        }
    }

    public async Task<ExercisesInfoModel?> GetAsync(IExercisesInfoRepository.GetById getModel) => await this.FindAsync(getModel);

    public async Task<IEnumerable<ExercisesInfoModel>> SearchAsync(IExercisesInfoRepository.SearchAllExercisesInfo searchModel)
    {
        try
        {
            var entity = await this.TrainingDbContext.ExercisesInfo
                .AsQueryable()
                .Include(e => e.Files)
                .ToListAsync();

            return this.mapper.Map<IEnumerable<ExercisesInfoModel>>(entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while searching Exercise Info, see inter exception for details.", ex);
        }
    }

    public async Task<ExercisesInfoModel> UpdateAsync(IExercisesInfoRepository.UpdateExerciseInfo updateModel)
    {
        try
        {
            var userInfo = await this.authenticationDetailsProvider.GetUserDetails();
            
            var entry = await this.TrainingDbContext.ExercisesInfo
                .FirstOrDefaultAsync(a => a.Identifier == updateModel.ExerciseId);

            if (entry == default) throw new NotFoundException($"Exercises Info with id: '{updateModel.ExerciseId}' not found!");
            
            entry.Name = updateModel.Name;
            entry.BodyElements = String.Join(";", updateModel.BodyElements);
            entry.Description = updateModel.Description;
            entry.ModifiedBy = new() { FullName = userInfo.FullName, Id = userInfo.Id };
            entry.ModifiedAt = SystemClock.Instance.InTzdbSystemDefaultZone().GetCurrentOffsetDateTime().LocalDateTime;
            this.TrainingDbContext.ExercisesInfo.Update(entry);
            await this.TrainingDbContext.SaveChangesAsync();

            return this.mapper.Map<ExercisesInfoModel>(entry);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while create Exercise Info, see inter exception for details.", ex);
        }
    }

    public async Task<ExercisesInfoModel?> GetAsync(IExercisesInfoRepository.GetByName getModel) => await this.FindAsync(getModel);
    public async Task<ExercisesInfoModel?> FindAsync(IExercisesInfoRepository.GetByName getModel)
    {
        try
        {
            var entity = await this.TrainingDbContext.ExercisesInfo
                .AsQueryable()
                .Include(e => e.Files)
                .FirstOrDefaultAsync(e => e.Name == getModel.Name);
            return this.mapper.Map<ExercisesInfoModel>(entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while getting Exercises Info, see inter exception for details.", ex);
        }
    }

    public async Task<ExercisesInfoModel> UpdateAsync(IExercisesInfoRepository.UpdateAttachmentExerciseInfo updateModel)
    {
        try
        {
            var userInfo = await this.authenticationDetailsProvider.GetUserDetails();
            
            var entry = await this.TrainingDbContext.ExercisesInfo
                .AsQueryable()
                .Include(e => e.Files)
                .FirstOrDefaultAsync(a => a.Identifier == updateModel.ExerciseId);

            if (entry == default) throw new NotFoundException($"Exercises Info with id: '{updateModel.ExerciseId}' not found!");
            var files = entry.Files.ToList();
            files.Add(new()
            {
                Identifier = default,
                PhotoId = updateModel.photoId,
                ExerciseInfoIdentifier = updateModel.ExerciseId,
                ExerciseInfo = entry,
            });
            entry.Files = files;
            entry.ModifiedBy = new() { FullName = userInfo.FullName, Id = userInfo.Id };
            entry.ModifiedAt = SystemClock.Instance.InTzdbSystemDefaultZone().GetCurrentOffsetDateTime().LocalDateTime;
            this.TrainingDbContext.ExercisesInfo.Update(entry);
            await this.TrainingDbContext.SaveChangesAsync();

            return this.mapper.Map<ExercisesInfoModel>(entry);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while getting Exercises Info, see inter exception for details.", ex);
        }
    }

    public async Task<IEnumerable<FileModel>> SearchAsync(IExercisesInfoRepository.GetFilesByExerciseId searchModel)
    {
        try
        {
            var entity = await this.TrainingDbContext.ExercisesInfo
                .Where(e => e.Identifier == searchModel.ExerciseId).ToListAsync();

            return this.mapper.Map<IEnumerable<FileModel>>(entity);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while searching files, see inter exception for details.", ex);
        }
    }

    public async Task<ExercisesInfoModel> DeleteByIdAsync(IExercisesInfoRepository.DeleteAttachmentExerciseInfo id)
    {
        try
        {
            var entity = await this.TrainingDbContext.Files.FirstOrDefaultAsync(p => p.ExerciseInfoIdentifier == id.ExerciseId && p.PhotoId == id.Key);

            if (entity == default) throw new NotFoundException($"File with key: '{id.Key}' not found!");

            this.TrainingDbContext.Files.Remove(entity);
            await this.TrainingDbContext.SaveChangesAsync();
            var res = await this.FindAsync(new IExercisesInfoRepository.GetById(id.ExerciseId));
            return this.mapper.Map<ExercisesInfoModel>(res);
        }
        catch (Exception ex)
        {
            throw new PersistanceException("Unknown error ocurred while deleted Exercise Info, see inter exception for details.", ex);
        }
    }
    
}
