namespace RedditClone.Application.Comment.Queries.GetCommunityById;

using RedditClone.Application.Community.Results.GetCommunityByIdResult;
using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Application.Community.Queries.GetCommunitiesById;
using RedditClone.Domain.CommunityAggregate;
using FluentValidation;

public class GetCommunityByIdQueryHandler
: IRequestHandler<GetCommunityByIdQuery, GetCommunityByIdResult>
{
    private readonly ICommunityRepository _communityRepository;
    private readonly IValidator<GetCommunityByIdQuery> _validator;

    public GetCommunityByIdQueryHandler(
        ICommunityRepository communityRepository,
        IValidator<GetCommunityByIdQuery> validator)
    {
        _communityRepository = communityRepository;
        _validator = validator;
    }

    public async Task<GetCommunityByIdResult> Handle(GetCommunityByIdQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(query);

        Community community = _communityRepository.GetCommunityById(query.CommunityId);

        return new GetCommunityByIdResult(community);
    }
}