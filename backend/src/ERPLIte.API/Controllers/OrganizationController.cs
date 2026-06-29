using Asp.Versioning;
using ERPLite.Application.Common.Models;
using ERPLite.Application.Features.Organizations.DTOs;
using ERPLite.Application.Features.Organizations.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ERPLite.API.Controllers;

//[ApiController]
//[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class OrganizationsController : ControllerBase
{
    private readonly IOrganizationService _organizationService;

    public OrganizationsController(
        IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    [HttpPost]
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
    public async Task<IActionResult> GetAll(
     CancellationToken cancellationToken)
    {
        var result =
            await _organizationService.GetAllAsync(
                cancellationToken);

        return Ok(
            ApiResponse<List<OrganizationResponse>>
                .Ok(result));
    }

    [HttpGet("{id:guid}")]
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
                .Ok(result));
    }
}
