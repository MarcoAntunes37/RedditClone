namespace RedditClone.Application.Post.Queries.GetPostVotesList;

using FluentValidation;
using RedditClone.Application.PostVotes.Queries.GetPostVotesList;

public partial class GetPostVotesListsQueryValidator : AbstractValidator<GetPostVotesListQuery>
{
    public GetPostVotesListsQueryValidator()
    {
        RuleFor(c => c.PostId)
            .NotNull()
                .WithMessage("Invalid Post");
    }
}