using ERPLite.Application.Common.Exceptions;
using ERPLite.Application.Common.Extensions;
using ERPLite.Application.Common.Models;
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

    public async Task<PagedResult<Organization>> GetAllAsync(
        QueryParameters parameters,
        CancellationToken cancellationToken)
    {
        var query = _context.Organizations
            .AsNoTracking()
            .Where(x => !x.IsDeleted);

        query = query.ApplySearch(
            parameters.Search,
            x => x.Name,
            x => x.Code);

        query = query.ApplyFiltering(parameters);

        var totalRecords =
            await query.CountAsync(cancellationToken);

        query = query.ApplySorting(parameters);

        query = query.ApplyPagination(parameters);

        var organizations =
            await query.ToListAsync(cancellationToken);

        var totalPages =
            (int)Math.Ceiling(
                totalRecords /
                (double)parameters.PageSize);

        return new PagedResult<Organization>
        {
            Items = organizations,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages,
            HasNextPage = parameters.PageNumber < totalPages,
            HasPreviousPage = parameters.PageNumber > 1
        };
    }

    public async Task<Organization?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Organizations
            .Where(x => !x.IsDeleted)
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
            .Where(x => !x.IsDeleted)
            .AnyAsync(
                x => x.Code == code,
                cancellationToken);
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var organization =
            await GetByIdAsync(id, cancellationToken);

        if (organization is null)
        {
            throw new NotFoundException(
                "Organization",
                id);
        }

        organization.Delete();

        await _context.SaveChangesAsync(
            cancellationToken);
    }

    public async Task<bool> ExistsByCodeAsync(
        string code,
        Guid excludeId,
        CancellationToken cancellationToken)
    {
        return await _context.Organizations
            .Where(x => !x.IsDeleted)
            .AnyAsync(
                x => x.Code == code &&
                     x.Id != excludeId,
                cancellationToken);
    }

    public async Task UpdateAsync(
        Organization organization,
        CancellationToken cancellationToken)
    {
        _context.Organizations.Update(organization);

        await _context.SaveChangesAsync(cancellationToken);
    }
}