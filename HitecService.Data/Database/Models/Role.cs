namespace HitecService.Data.Database.Models;

public class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Privilege> Privileges { get; } = new List<Privilege>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}