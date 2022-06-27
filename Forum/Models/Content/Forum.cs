using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Forum.Models.Users;

namespace Forum.Models.Content
{
  public abstract class PostContent : EntityBase
  {
    [Column(TypeName = "varchar(255)")]
    public string Title { get; set; }
    public string Body { get; set; }
  }
  public class News : PostContent
  {

  }
  public class Topic : PostContent
  {
    public User Author { get; set; }
    public int AuthorId { get; set; }
    public Section Section { get; set; }
    public int SectionId { get; set; }
    public bool Private { get; set; }
  }

  public class Section
  {
    [Column(TypeName = "varchar(255)")]
    public string Title { get; set; }
    public string Description { get; set; }
  }
}
