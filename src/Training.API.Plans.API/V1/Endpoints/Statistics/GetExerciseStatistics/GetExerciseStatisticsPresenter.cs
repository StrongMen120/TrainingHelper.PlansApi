using Serilog;
using Training.API.Plans.API.V1.DTOs;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.UseCases;
using Training.Common.Hexagon.API;

namespace Training.API.Plans.API.V1.Endpoints.Statistics.GetExerciseStatistics;

public class GetExerciseStatisticsPresenter : ActionResultPresenterBase<GetExercisesStatisticsUseCase>, GetExercisesStatisticsUseCase.IOutput
{
    public GetExerciseStatisticsPresenter(ILogger logger, IApiMapper mapper)
        : base(logger, mapper)
        { }

    public void NotFoundExercise(GetExercisesStatisticsUseCase.Input input) => this.SetResult(ActionResultFactory.NotFound404($"Exercise witch id:{input.ExerciseId} not found!."));

    public void Success(IEnumerable<StatisticsModel> statistics) => this.SetResult(ActionResultFactory.Ok200(this.Mapper.Map<IEnumerable<StatisticsDto>>(statistics)));

    public void UnknownError(Exception exception) => this.SetException(exception);
}