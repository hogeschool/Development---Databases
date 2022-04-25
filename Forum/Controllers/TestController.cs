using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Forum.Models;

namespace Forum.Controllers;

[Route("test")]
public class ForumController : Controller
{
  public ForumController()
  {

  }

  [HttpGet]
  public IActionResult Test()
  {
    return Ok("Hello World!");
  } 
}
