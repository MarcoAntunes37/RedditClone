namespace RedditClone.Application.Comment.Queries.GetCommunityById;

using RedditClone.Application.Community.Results.GetCommunityByIdResult;
using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Application.Community.Queries.GetCommunitiesById;
using RedditClone.Domain.CommunityAggregate;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Helpers;
using Serilog;

public class GetCommunityByIdQueryHandler
: IRequestHandler<GetCommunityByIdQuery, GetCommunityByIdResult>
{
    private readonly ICommunityRepository _communityRepository;
    private readonly IValidator<GetCommunityByIdQuery> _validator;
    private readonly IConfiguration _configuration;

    public GetCommunityByIdQueryHandler(
        ICommunityRepository communityRepository,
        IValidator<GetCommunityByIdQuery> validator,
        IConfiguration configuration)
    {
        _communityRepository = communityRepository;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<GetCommunityByIdResult> Handle(GetCommunityByIdQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@GetCommunityByIdQuery}",
            "Trying to retrieve community data",
            query);

        _validator.ValidateAndThrow(query);

        Community community = _communityRepository.GetCommunityById(query.CommunityId);

        GetCommunityByIdResult result = new(community);

        Log.Information(
            "{@GetCommunityByIdResult}",
            result);

        return result;
    }
}