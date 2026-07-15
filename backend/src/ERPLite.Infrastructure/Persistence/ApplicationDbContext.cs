using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ERPLite.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ERPLite.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
        
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);


         modelBuilder.Entity<RefreshToken>()
        .HasOne(x => x.User)
        .WithMany(x => x.RefreshTokens)
        .HasForeignKey(x => x.UserId);

        modelBuilder.Entity<ApplicationUser>()
    .ToTable("Users");

        modelBuilder.Entity<ApplicationRole>()
            .ToTable("Roles");

        
    }
}
