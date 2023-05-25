namespace HitecService.Data.Database.Models;

public class Privilege
{
    public int Id { get; set; }

    public int RolId { get; set; }

    public int MenuConfigurationId { get; set; }

    public bool CanCreate { get; set; }

    public bool CanEdit { get; set; }

    public bool CanRead { get; set; }

    public bool CanDelete { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual MenuConfiguration MenuConfiguration { get; set; } = null!;

    public virtual Role Rol { get; set; } = null!;
}