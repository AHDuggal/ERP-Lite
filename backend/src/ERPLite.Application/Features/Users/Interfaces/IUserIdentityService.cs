using ERPLite.Application.Common.Models;
using ERPLite.Application.Features.Users.DTOs;
using ERPLite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Application.Features.Users.Interfaces
{
    public interface IUserIdentityService
    {
        Task<PagedResult<ApplicationUser>> GetUsersAsync(
    QueryParameters parameters,
    CancellationToken cancellationToken);

        Task<ApplicationUser?> GetUserByIdAsync(
            Guid id);

        Task<ApplicationUser> CreateUserAsync(
            CreateUserRequest request,
            CancellationToken cancellationToken);

        Task<ApplicationUser> UpdateUserAsync(
            UpdateUserRequest request,
            CancellationToken cancellationToken);

        Task DeleteUserAsync(
            Guid id,
            CancellationToken cancellationToken);
    }
}
