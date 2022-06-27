using Forum.Controllers.Community;
using Forum.Models;
using Forum.Models.Content;
using Forum.Models.Users;
using Forum.Services.Data;
using System.Collections.Generic;
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

    public CreateContentResponse CreateContent(int authorId, PostData data)
    {
      switch (data.PostKind)
      {
        case PostKind.Topic:
          if (Context.Topic.Any(topic => topic.Title == data.Title))
            return CreateContentResponse.ContentExists;
          var newTopic = new Topic
          {
            AuthorId = authorId,
            Title = data.Title,
            Body = data.Body,
            Private = data.Private,
            SectionId = data.SectionId
          };
          Context.Topic.Add(newTopic);
          Context.SaveChanges();
          return CreateContentResponse.Succeeded;
        case PostKind.News:
          if (Context.News.Any(news => news.Title == data.Title))
            return CreateContentResponse.ContentExists;

          //TODO COMPLETE THE LOGIC
          return CreateContentResponse.Succeeded;
        default: return CreateContentResponse.Succeeded;
      }
    }
    public List<IGrouping<string, UserWithTopics>> Search(string term, int pageSize, int pageNumber)
    {
      var userWithTopics =
        (from user in Context.User
        join topic in Context.Topic on user.Id equals topic.AuthorId
        where user.Email.Contains(term) || user.UserName.Contains(term)
        group new UserWithTopics
        {
          UserName = user.UserName,
          Topic = topic
        } by user.UserName into userGroup
        select userGroup)
        .Skip(pageSize * pageNumber)
        .Take(pageNumber)
        .ToList();
      return userWithTopics;
    }
  }

}