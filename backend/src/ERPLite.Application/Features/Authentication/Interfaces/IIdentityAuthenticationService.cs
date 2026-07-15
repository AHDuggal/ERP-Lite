using ERPLite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Application.Features.Authentication.Interfaces
{
    public interface IIdentityAuthenticationService
    {
        Task<ApplicationUser?> FindByEmailAsync(
    string email);

        Task<bool> CheckPasswordAsync(
            ApplicationUser user,
            string password);

        Task<IList<string>> GetRolesAsync(
            ApplicationUser user);

        Task SaveRefreshTokenAsync(
            RefreshToken refreshToken,
            ApplicationUser user,
            CancellationToken cancellationToken);

        Task<ApplicationUser?> FindByIdAsync(
            Guid userId);

        Task<RefreshToken?> GetRefreshTokenAsync(
            string token,
            CancellationToken cancellationToken);

        Task RevokeRefreshTokenAsync(
            RefreshToken refreshToken,
            string replacedByToken,
            string reason,
            CancellationToken cancellationToken);
    }
}
