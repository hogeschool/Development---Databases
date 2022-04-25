using Forum.Models;
using Forum.Services.Authentication;
using Forum.Services.Community;
using Forum.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers.Community
{
  [Route("user")]
  public class CommunityController : Controller
  {
    private readonly AuthenticationHelpers Authentication;
    private readonly CommunityService CommunityService;

    public CommunityController(
      CommunityService communityService,
      AuthenticationHelpers authentication
    )
    {
      Authentication = authentication;
      CommunityService = communityService;
    }

    [HttpPost("add-friend")]
    public IActionResult AddFriend([FromQuery] int friendId)
    {
      var sessionToken = HttpContext.Request.Cookies["UserLogin"];
      return Authentication.AuthenticateAPIOperation<IActionResult>(
        sessionToken,
        () => Unauthorized(),
        (session, userId) =>
        {
          var addFriendResult = CommunityService.AddFriend(userId, friendId);
          switch (addFriendResult)
          {
            case AddFriendResponse.FriendExists:
              return BadRequest(new { Reson = "user exists" });
            case AddFriendResponse.NotFound:
              return BadRequest(new { Reason = "user not found" });
            case AddFriendResponse.BadRequest:
              return BadRequest();
            case AddFriendResponse.Succeeded:
              return Ok();
            default:
              return StatusCode(500);
          }
        }
      );
    }
  }
}