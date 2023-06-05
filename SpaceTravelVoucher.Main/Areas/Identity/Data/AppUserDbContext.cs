using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpaceTravelVoucher.Main.Models;

namespace SpaceTravelVoucher.Main.Data;

public partial class AppUserDbContext : IdentityDbContext<ApplicationUser>
{
    public AppUserDbContext(DbContextOptions<AppUserDbContext> options)
        : base(options)
    {
    }
    public AppUserDbContext()
    {
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public virtual DbSet<VUsers> VUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<VUsers>(entity =>
        {

            entity.ToView("v_Users");

            entity.Property(e => e.Email).HasMaxLength(256);

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Id)
                .IsRequired()
                .HasMaxLength(450);

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Name).HasMaxLength(256);

            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        //OnModelCreatingPartial(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
