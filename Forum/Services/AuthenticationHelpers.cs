using System;
using System.Linq;
using Forum.Models;
using Forum.Models.Authentication;
using Forum.Models.Users;

namespace Forum.Services.Authentication
{
  public class AuthenticationHelpers
  {
    public readonly ForumContext Context;

    public AuthenticationHelpers(ForumContext context)
    {
      Context = context;
    }

    public TResult AuthenticateAPIOperation<TResult>(
      string sessionToken,
      Func<TResult> onUnauthorized,
      Func<Session, int, TResult>onAuthorized
    )
    {
      var sessionDescriptor =
        (from session in Context.Session
        join user in Context.User on session.UserId equals user.Id
        where session.SessionToken == sessionToken
        select new { Session = session, SessionUserId = user.Id }).FirstOrDefault();
      
      if (sessionDescriptor == null)
        return onUnauthorized();
      
      return onAuthorized(sessionDescriptor.Session, sessionDescriptor.SessionUserId);
    }
  }
}