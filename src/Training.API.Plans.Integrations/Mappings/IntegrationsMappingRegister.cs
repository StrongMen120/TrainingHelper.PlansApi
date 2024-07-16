using Mapster;
using Training.API.Plans.Core.Domain;
using Training.API.Users.Integration.Model;

namespace Training.API.Plans.Integrations.Mappings;

public sealed class IntegrationsMappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserDto, UserInfoModel>()
            .MapToConstructor(true)
            .Map(d => d.Id, s => s.Identifier)
            .Map(d => d.FullName, s => $"{s.FirstName} {s.SecondName}")
            .ShallowCopyForSameType(false);
    }
}