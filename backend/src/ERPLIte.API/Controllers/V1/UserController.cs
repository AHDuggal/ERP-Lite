using Asp.Versioning;
using ERPLite.Application.Features.Users.DTOs;
using ERPLite.Application.Features.Users.Interfaces;
using ERPLite.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERPLite.API.Controllers.V1;
[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(
        IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] QueryParameters parameters, CancellationToken cancellationToken)
    {
        var users =
            await _userService.GetAllAsync(parameters,
                cancellationToken);

        return Ok(
    ApiResponse<PagedResult<UserDto>>.Ok(
        users,
        "Users retrieved successfully."));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(
    typeof(UserDto),
    StatusCodes.Status200OK)]
    [ProducesResponseType(
    StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
    Guid id,
    CancellationToken cancellationToken)
    {
        var user =
            await _userService.GetByIdAsync(
                id,
                cancellationToken);

        return Ok(
    ApiResponse<UserDto>.Ok(
        user,
        "User retrieved successfully."));
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserDto),
    StatusCodes.Status201Created)]
    [ProducesResponseType( StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
    CreateUserRequest request,
    CancellationToken cancellationToken)
    {
        var user =
            await _userService.CreateAsync(
                request,
                cancellationToken);

        return Ok(
     ApiResponse<UserDto>.Created(
         user,
         "User created successfully."));
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType( typeof(UserDto),
    StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
    Guid id,
    UpdateUserRequest request,
    CancellationToken cancellationToken)
    {
        request.Id = id;

        var user =
            await _userService.UpdateAsync(
                request,
                cancellationToken);

        return Ok(
    ApiResponse<UserDto>.Ok(
        user,
        "User updated successfully."));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
    Guid id,
    CancellationToken cancellationToken)
    {
        await _userService.DeleteAsync(
            id,
            cancellationToken);

        return Ok(
    ApiResponse<object>.Ok(default,
        message: "User deleted successfully."));
    }





}