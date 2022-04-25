using Forum.Models;
using Forum.Models.Users;
using Forum.Services.Data;
using System.Linq;

namespace Forum.Services.Community
{
  public class CommunityService
  {
    private readonly ForumContext Context;
    public CommunityService(ForumContext context)
    {
      Context = context;
    }

    public AddFriendResponse AddFriend(int userId, int friendId)
    {
      var friend = Context.User.FirstOrDefault(user => user.Id == friendId);
      var existingFriend =
        Context.User_Friend.FirstOrDefault(
          u_f => u_f.UserId == userId && u_f.FriendId == friendId
        );
      if (userId == friendId)
      {
        return AddFriendResponse.BadRequest;
      }
      if (existingFriend != null)
      {
        return AddFriendResponse.FriendExists;
      }
      if (friend == null)
      {
        return AddFriendResponse.NotFound;
      }

      var link =
        new User_Friend
        {
          UserId = userId,
          FriendId = friendId
        };

      Context.User_Friend.Add(link);
      Context.SaveChanges();
      return AddFriendResponse.Succeeded;
    } 
  }
}