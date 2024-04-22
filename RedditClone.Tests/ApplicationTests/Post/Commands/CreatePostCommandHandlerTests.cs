namespace RedditClone.Tests.ApplicationTests.Post.Commands;

using Moq;
using ErrorOr;
using RedditClone.Domain.PostAggregate;
using RedditClone.Domain.PostAggregate.Entities;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.Post.Commands.CreatePost;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.Post.Results.CreatePostResult;
using RedditClone.Application.Common.Interfaces.Persistence;

public class CreatePostCommandHandlerTests
{

    [Fact]
    public async Task CreatePostCommand_ShouldReturnCreatePostResult_WhenPostIsValid()
    {
        var mockPostRepository = new Mock<IPostRepository>();

        var mockUserCommunitiesRepository = new Mock<IUserCommunitiesRepository>();

        var handler = new CreatePostCommandHandler(
            mockPostRepository.Object,
            mockUserCommunitiesRepository.Object);

        List<Votes> votes = new();

        var command = new CreatePostCommand(
            new CommunityId(Guid.NewGuid()),
            new UserId(Guid.NewGuid()),
            "TestTitle",
            "TestContent",
            votes);

        var result = await handler.Handle(command, default);

        Assert.IsType<ErrorOr<CreatePostResult>>(result);

        mockPostRepository.Verify(r => r.Add(It.IsAny<Post>()), Times.Once);
    }
}