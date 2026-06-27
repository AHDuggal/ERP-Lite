using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ERPLite.Application.Features.Organizations.Interfaces;
using ERPLite.Domain.Entities;
using ERPLite.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ERPLite.Infrastructure.Repositories;

public sealed class OrganizationRepository
    : IOrganizationRepository
{
    private readonly ApplicationDbContext _context;

    public OrganizationRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Organization> AddAsync(
        Organization organization,
        CancellationToken cancellationToken)
    {

        await _context.Organizations.AddAsync(
            organization,
            cancellationToken);

        return organization;
    }

    public async Task<List<Organization>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Organizations
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Organization?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Organizations
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(
            cancellationToken);
    }

    public async Task<bool> ExistsByCodeAsync(
    string code,
    CancellationToken cancellationToken)
    {
        return await _context.Organizations
            .AnyAsync(
                x => x.Code == code,
                cancellationToken);
    }
}
