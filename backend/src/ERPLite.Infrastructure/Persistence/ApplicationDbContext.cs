using ERPLite.Application.Common.Interfaces;
using ERPLite.Domain.Common;
using ERPLite.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
{
    private readonly ICurrentUserService _currentUserService;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ICurrentUserService currentUserService)
        : base(options)
    {
        _currentUserService = currentUserService;
    }
    
        
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

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

    public override async Task<int> SaveChangesAsync(
    CancellationToken cancellationToken = default)
    {
        ApplyAuditInformation();

        return await base.SaveChangesAsync(
            cancellationToken);
    }

    private void ApplyAuditInformation()
    {
        var entries =
            ChangeTracker
                .Entries<AuditableEntity>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:

                    entry.Entity.CreatedOnUtc =
                        DateTime.UtcNow;

                    entry.Entity.CreatedBy =
                        _currentUserService.UserId;

                    break;

                case EntityState.Modified:

                    entry.Entity.UpdatedOnUtc =
                        DateTime.UtcNow;

                    entry.Entity.UpdatedBy =
                        _currentUserService.UserId;

                    if (entry.Entity.IsDeleted &&
                        entry.Entity.DeletedOnUtc is null)
                    {
                        entry.Entity.DeletedOnUtc =
                            DateTime.UtcNow;

                        entry.Entity.DeletedBy =
                            _currentUserService.UserId;
                    }

                    break;
               
            }
        }
    }
}
