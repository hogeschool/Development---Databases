using System;
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
              return BadRequest(new { Reason = "user exists" });
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

    [HttpPost("post")]
    public IActionResult Post([FromBody] PostData data)
    {
      var sessionToken = HttpContext.Request.Cookies["UserLogin"];
      return Authentication.AuthenticateAPIOperation<IActionResult>(
        sessionToken,
        () => Unauthorized(),
        (session, userId) =>
        {
          try
          {
            var result = CommunityService.CreateContent(userId, data);
            return Ok(result);
          }
          catch (Exception e)
          {
            return StatusCode(500, new { Exception = e.Message, Stack = e.StackTrace });
          }
        }
      );
    }

    [HttpGet("search")]
    public IActionResult Search([FromQuery] string term, [FromQuery] int pageSize, [FromQuery] int pageNumber)
    {
      try
      {
        return Ok(CommunityService.Search(term, pageSize, pageNumber));
      }
      catch (Exception e)
      {
        return StatusCode(500, new { Exception = e.Message, Stack = e.StackTrace });
      }
    }
  }
}