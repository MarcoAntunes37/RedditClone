using Microsoft.AspNetCore.Mvc;
using RedditClone.Contracts.Community;

namespace RedditClone.API.Controllers;

[Route("communities")]
public class CommunityController : ApiController
{
    [HttpPost("{userId}")]
    public IActionResult CreateCommunity(
        CreateCommunityRequest request, string userId)
    {
        return Ok(request);
    }
}