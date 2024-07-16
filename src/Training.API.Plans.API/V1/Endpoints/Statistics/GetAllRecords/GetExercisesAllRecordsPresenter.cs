using Serilog;
using Training.API.Plans.API.V1.DTOs;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.UseCases;
using Training.Common.Hexagon.API;

namespace Training.API.Plans.API.V1.Endpoints.Statistics.GetAllRecords;

public class GetExercisesAllRecordsPresenter : ActionResultPresenterBase<GetExercisesAllRecordsUseCase>, GetExercisesAllRecordsUseCase.IOutput
{
    public GetExercisesAllRecordsPresenter(ILogger logger, IApiMapper mapper)
        : base(logger, mapper)
        { }

    public void NotFoundExerciseRecords(GetExercisesAllRecordsUseCase.Input input) => this.SetResult(ActionResultFactory.NotFound404("Exercise records not found!."));

    public void Success(IEnumerable<ExercisesRecordsModel> exercisesRecords) => this.SetResult(ActionResultFactory.Ok200(this.Mapper.Map<IEnumerable<ExercisesRecordsDto>>(exercisesRecords)));

    public void UnknownError(Exception exception) => this.SetException(exception);
}