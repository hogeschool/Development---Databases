using System;

namespace Forum.Models
{
  public abstract class EntityBase
  {
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime Modified { get; set; }

    public EntityBase()
    {
      CreatedDate = DateTime.UtcNow;
      Modified = DateTime.UtcNow;
    }
  }
}