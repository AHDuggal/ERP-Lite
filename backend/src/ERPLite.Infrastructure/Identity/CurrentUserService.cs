using ERPLite.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace ERPLite.Infrastructure.Identity;

public sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor =
            httpContextAccessor;
    }

    public bool IsAuthenticated =>
        _httpContextAccessor
            .HttpContext?
            .User?
            .Identity?
            .IsAuthenticated ?? false;

    public Guid UserId
    {
        get
        {
            var value =
                _httpContextAccessor
                    .HttpContext?
                    .User?
                    .FindFirstValue(
                        ClaimTypes.NameIdentifier);

            return Guid.TryParse(
                value,
                out var id)
                ? id
                : Guid.Empty;
        }
    }

    public string? Email =>
        _httpContextAccessor
            .HttpContext?
            .User?
            .FindFirstValue(
                ClaimTypes.Email);

    public string? UserName =>
        _httpContextAccessor
            .HttpContext?
            .User?
            .Identity?
            .Name;

    public bool IsInRole(
        string role)
    {
        return _httpContextAccessor
            .HttpContext?
            .User?
            .IsInRole(role) ?? false;
    }
}
