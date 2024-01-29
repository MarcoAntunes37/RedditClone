using ErrorOr;
using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Domain.PostAggregate;

namespace RedditClone.Application.Post.Commands.CreatePost;

public class CreatePostCommandHandler :
    IRequestHandler<CreatePostCommand,
    ErrorOr<PostAggregate>>
{
    private readonly IPostRepository _postRepository;

    public CreatePostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<ErrorOr<PostAggregate>> Handle(CreatePostCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        //create post
        var post = PostAggregate.Create(
            command.Title,
            command.Content,
            command.UserId,
            command.CommunityId,
            command.CreatedAt,
            command.UpdatedAt,
            command.Upvotes,
            command.Downvotes
        );

        //persist post
        _postRepository.Add(post);

        //return post
        return post;
    }
}