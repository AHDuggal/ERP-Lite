using Asp.Versioning;
using ERPLite.Application.Common.Models;
using ERPLite.Application.Features.Roles.DTOs;
using ERPLite.Application.Features.Roles.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERPLite.API.Controllers.V1;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(Roles = "Admin")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(
        IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<RoleDto>>),
    StatusCodes.Status200OK)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status401Unauthorized)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result =
            await _roleService.GetAllAsync(
                cancellationToken);

        return Ok(
       ApiResponse<IEnumerable<RoleDto>>.Ok(
           result,
           "Roles retrieved successfully."));
    }

    [HttpGet("{id:guid}")]
    
    [ProducesResponseType(typeof(ApiResponse<RoleDto>),
    StatusCodes.Status200OK)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status404NotFound)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status401Unauthorized)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result =
            await _roleService.GetByIdAsync(
                id,
                cancellationToken);

        return Ok(
        ApiResponse<RoleDto>.Ok(
            result,
            "Role retrieved successfully."));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<RoleDto>),
    StatusCodes.Status201Created)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status400BadRequest)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status409Conflict)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status401Unauthorized)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Create(
    CreateRoleRequest request,
    CancellationToken cancellationToken)
    {
        var result =
            await _roleService.CreateAsync(
                request,
                cancellationToken);

        return StatusCode(
    StatusCodes.Status201Created,
    ApiResponse<RoleDto>.Created(
        result,
        "Role created successfully."));
    }

    [HttpPut("{id:guid}")]

    [ProducesResponseType(typeof(ApiResponse<RoleDto>),
    StatusCodes.Status200OK)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status400BadRequest)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status404NotFound)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status409Conflict)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status401Unauthorized)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Update(
    Guid id,
    UpdateRoleRequest request,
    CancellationToken cancellationToken)
    {
        request.Id = id;

        var result =
            await _roleService.UpdateAsync(
                request,
                cancellationToken);

        return Ok(
            ApiResponse<RoleDto>.Ok(
                result,
                "Role updated successfully."));
    }

    [HttpDelete("{id:guid}")]

    [ProducesResponseType(typeof(ApiResponse<object>),
    StatusCodes.Status200OK)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status404NotFound)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status401Unauthorized)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Delete(
    Guid id,
    CancellationToken cancellationToken)
    {
        await _roleService.DeleteAsync(
            id,
            cancellationToken);

        return Ok(
            ApiResponse<object>.Ok(                
                default,
                "Role deleted successfully."));
    }

    [HttpPost("assign")]
    [ProducesResponseType(typeof(ApiResponse<object>),
    StatusCodes.Status200OK)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status400BadRequest)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status404NotFound)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status409Conflict)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status401Unauthorized)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AssignRole(
     AssignRoleRequest request,
     CancellationToken cancellationToken)
    {
        await _roleService.AssignRoleAsync(
            request,
            cancellationToken);

        return Ok(
            ApiResponse<object>.Ok(
                default,
                "Role assigned successfully."));
    }

    [HttpDelete("remove")]
    [ProducesResponseType(typeof(ApiResponse<object>),
    StatusCodes.Status200OK)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status400BadRequest)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status404NotFound)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status409Conflict)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status401Unauthorized)]

    [ProducesResponseType(
    typeof(ApiErrorResponse),
    StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> RemoveRole(
    AssignRoleRequest request,
    CancellationToken cancellationToken)
    {
        await _roleService.RemoveRoleAsync(
            request,
            cancellationToken);

        return Ok(
            ApiResponse<object>.Ok(
                default,
                "Role removed successfully."));
    }

    [HttpGet("user/{userId:guid}")]
    [ProducesResponseType(typeof(ApiResponse<IList<string>>),
    StatusCodes.Status200OK)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status404NotFound)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status401Unauthorized)]

    [ProducesResponseType( typeof(ApiErrorResponse),
    StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetUserRoles(
    Guid userId,
    CancellationToken cancellationToken)
    {
        var result =
            await _roleService.GetUserRolesAsync(
                userId,
                cancellationToken);

        return Ok(
            ApiResponse<IList<string>>.Ok(
                result,
                "User roles retrieved successfully."));
    }
}