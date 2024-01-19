namespace RedditClone.API.Controllers;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
public class MainFeedController : ApiController {
    [HttpGet]
    public IActionResult MainFeed(){
        return Ok(Array.Empty<string>());
    }
}