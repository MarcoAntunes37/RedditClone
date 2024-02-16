namespace RedditClone.Application.Post.Commands.CreatePost;

using FluentValidation;
using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Application.Post.Results.CreatePostResult;
using RedditClone.Domain.PostAggregate;

public class CreatePostCommandHandler
    : IRequestHandler<CreatePostCommand, CreatePostResult>
{
    private readonly IPostRepository _postRepository;
    private readonly IValidator<CreatePostCommand> _validator;

    public CreatePostCommandHandler(
        IPostRepository postRepository,
        IValidator<CreatePostCommand> validator)
    {
        _postRepository = postRepository;
        _validator = validator;
    }

    public async Task<CreatePostResult> Handle(CreatePostCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        //create post
        var post = Post.Create(
            command.CommunityId,
            command.UserId,
            command.Title,
            command.Content,
            command.CreatedAt,
            command.UpdatedAt,
            command.Votes
        );

        //persist post
        _postRepository.Add(post);

        //return post
        return new CreatePostResult(
            post
        );
    }
}