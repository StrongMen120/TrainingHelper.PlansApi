using Serilog;
using Training.API.Plans.API.V1.DTOs;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.UseCases;
using Training.Common.Hexagon.API;

namespace Training.API.Plans.API.V1.Endpoints.DoneExercise.Create;

public class CreateDoneExercisePresenter : ActionResultPresenterBase<CreateDoneExerciseUseCase>, CreateDoneExerciseUseCase.IOutput
{
    public CreateDoneExercisePresenter(ILogger logger, IApiMapper mapper)
        : base(logger, mapper)
    { }
    public void NotFoundExercise(CreateDoneExerciseUseCase.Input DoneExercise) => this.SetResult(ActionResultFactory.NotFound404("Exercise not found!."));

    public void Success(IEnumerable<DoneExercisesModel> DoneExercises)=> this.SetResult(ActionResultFactory.Ok200(this.Mapper.Map<IEnumerable<DoneExercisesModel>>(DoneExercises)));

    public void UnknownError(Exception exception) => this.SetException(exception);
}