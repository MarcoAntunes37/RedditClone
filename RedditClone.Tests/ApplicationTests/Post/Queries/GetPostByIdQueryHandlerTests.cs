namespace RedditClone.Tests.ApplicationTests.Post.Queries;

using Moq;
using ErrorOr;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Application.Post.Queries.GetPostById;
using RedditClone.Application.Comment.Queries.GetPostById;
using RedditClone.Application.Post.Results.GetPostByIdResult;
using RedditClone.Application.Common.Interfaces.Persistence;

public class GetPostByIdQueryHandlerTests
{
    [Fact]
    public async Task GetPostByIdQuery_ShouldReturnGetPostByIdResult_WhenPostIsValid()
    {
        var mockPostRepository = new Mock<IPostRepository>();

        var handler = new GetPostByIdQueryHandler(mockPostRepository.Object);

        var query = new GetPostByIdQuery(new PostId(Guid.NewGuid()));

        var result = await handler.Handle(query, default);

        Assert.IsType<ErrorOr<GetPostByIdResult>>(result);

        mockPostRepository.Verify(r => r.GetPostById(It.IsAny<PostId>()), Times.Once);
    }
}