using Serilog;
using Training.API.Plans.API.V1.DTOs;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.UseCases;
using Training.Common.Hexagon.API;

namespace Training.API.Plans.API.V1.Endpoints.Statistics.Create;

public class CreateExercisesRecordsPresenter : ActionResultPresenterBase<CreateExercisesRecordsUseCase>, CreateExercisesRecordsUseCase.IOutput
{
    public CreateExercisesRecordsPresenter(ILogger logger, IApiMapper mapper)
        : base(logger, mapper)
    { }

    public void NotFoundExercise(CreateExercisesRecordsUseCase.Input input) => this.SetResult(ActionResultFactory.NotFound404("Exercise not found!."));

    public void Success(ExercisesRecordsModel exercisesRecords) => this.SetResult(ActionResultFactory.Ok200(this.Mapper.Map<ExercisesRecordsDto>(exercisesRecords)));

    public void UnknownError(Exception exception) => this.SetException(exception);
}