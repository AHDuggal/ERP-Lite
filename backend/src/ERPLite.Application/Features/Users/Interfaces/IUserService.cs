using ERPLite.Application.Features.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPLite.Application.Common.Models;

namespace ERPLite.Application.Features.Users.Interfaces;
public interface IUserService
{
    Task<PagedResult<UserDto>> GetAllAsync(
   QueryParameters parameters,
   CancellationToken cancellationToken);

    Task<UserDto> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<UserDto> CreateAsync(
        CreateUserRequest request,
        CancellationToken cancellationToken);

    Task<UserDto> UpdateAsync(
        UpdateUserRequest request,
        CancellationToken cancellationToken);

    Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken);
}
