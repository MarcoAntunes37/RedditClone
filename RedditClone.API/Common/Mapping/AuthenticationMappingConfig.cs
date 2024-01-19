using RedditClone.Contracts.Login;
using RedditClone.Contracts.Register;
using Mapster;
using RedditClone.Application.Authentication.Commands.Register;
using RedditClone.Application.Authentication.Queries.Login;

namespace RedditClone.API.Common.Mapping;

public class AuthenticationMappingConfig : IRegister{
    public void Register(TypeAdapterConfig config){
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();
    }
}