using HitecService.Data.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace HitecService.Data.Database;

public partial class HitecServiceDbContext : DbContext
{
    public HitecServiceDbContext() { }

    public HitecServiceDbContext(DbContextOptions<HitecServiceDbContext> options)
        : base(options) { }

    public virtual DbSet<MenuConfiguration> MenuConfigurations { get; set; }

    public virtual DbSet<Privilege> Privileges { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MenuConfiguration>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.MenuName).HasMaxLength(100);
            entity.Property(e => e.PageName).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Privilege>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.MenuConfiguration).WithMany(p => p.Privileges)
                .HasForeignKey(d => d.MenuConfigurationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Privileges_MenuConfigurations");

            entity.HasOne(d => d.Rol).WithMany(p => p.Privileges)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Privileges_Roles");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasIndex(e => e.Email, "idx_email");

            entity.HasIndex(e => e.UserName, "idx_user_name");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.LastActivity).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Observation).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.SecretAnswer).HasMaxLength(255);
            entity.Property(e => e.SecretQuestion).HasMaxLength(255);
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.Rol).WithMany(p => p.Users)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}