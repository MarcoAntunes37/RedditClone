namespace RedditClone.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Authorize]
public class ApiController : ControllerBase
{

}