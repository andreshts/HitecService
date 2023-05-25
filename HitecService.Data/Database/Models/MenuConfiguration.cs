namespace HitecService.Data.Database.Models;

public class MenuConfiguration
{
    public int Id { get; set; }

    public string? MenuName { get; set; }

    public string PageName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Privilege> Privileges { get; } = new List<Privilege>();
}