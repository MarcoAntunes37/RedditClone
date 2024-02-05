using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RedditClone.API.Controllers;

[ApiController]
[Authorize]
public class ApiController : ControllerBase
{

}