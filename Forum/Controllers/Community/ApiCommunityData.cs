using Forum.Models.Content;

namespace Forum.Controllers.Community
{
  public enum PostKind { News, Topic }
  public class PostData : PostContent
  {
    public PostKind PostKind { get; set; }
    public bool Private { get; set; }
    public int SectionId { get; set; }
  }
}