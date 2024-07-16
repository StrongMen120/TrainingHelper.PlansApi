using Serilog;
using Training.API.Plans.API.V1.DTOs;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Core.UseCases;
using Training.Common.Hexagon.API;

namespace Training.API.Plans.API.V1.Endpoints.ExercisesInfo.GetAll;

public class GetAllExercisesInfoPresenter : ActionResultPresenterBase<GetAllExercisesInfoUseCase>, GetAllExercisesInfoUseCase.IOutput
{
    public GetAllExercisesInfoPresenter(ILogger logger, IApiMapper mapper)
        : base(logger, mapper)
    { }

    public void Success(IEnumerable<ExercisesInfoModel> driver) => this.SetResult(ActionResultFactory.Ok200(this.Mapper.Map<IEnumerable<ExercisesInfoDto>>(driver)));

    public void UnknownError(Exception exception) => this.SetException(exception);
}