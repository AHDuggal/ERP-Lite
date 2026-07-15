using Asp.Versioning;
using ERPLite.Application.Common.Models;
using ERPLite.Application.Features.Organizations.DTOs;
using ERPLite.Application.Features.Organizations.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ERPLite.API.Controllers;

//[ApiController]
//[Route("api/[controller]")]

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[Authorize(Roles = "Admin")]
public class OrganizationsController : ControllerBase
{
    private readonly IOrganizationService _organizationService;

    public OrganizationsController(
        IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<OrganizationResponse>),
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
    CreateOrganizationRequest request,
    CancellationToken cancellationToken)
    {
        var result =
            await _organizationService.CreateAsync(
                request,
                cancellationToken);

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<OrganizationResponse>.Created(
                result,
                "Organization created successfully."));
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<OrganizationResponse>>),
    StatusCodes.Status200OK)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status401Unauthorized)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAll(
     CancellationToken cancellationToken)
    {
        var result =
            await _organizationService.GetAllAsync(
                cancellationToken);

        return Ok(
     ApiResponse<List<OrganizationResponse>>
         .Ok(
             result,
             "Organizations retrieved successfully."));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<OrganizationResponse>),
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
            await _organizationService.GetByIdAsync(
                id,
                cancellationToken);

        return Ok(
    ApiResponse<OrganizationResponse>
        .Ok(
            result,
            "Organization retrieved successfully."));
    }


    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<object>),
    StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
    Guid id,
    CancellationToken cancellationToken)
    {
        await _organizationService.DeleteAsync(
            id,
            cancellationToken);

        return Ok(
            ApiResponse<object>.Ok(
                default,
                "Organization deleted successfully."));
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<OrganizationResponse>),
    StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
    Guid id,
    UpdateOrganizationRequest request,
    CancellationToken cancellationToken)
    {
        request.Id = id;

        var result =
            await _organizationService.UpdateAsync(
                request,
                cancellationToken);

        return Ok(
            ApiResponse<OrganizationResponse>.Ok(
                result,
                "Organization updated successfully."));
    }
}
