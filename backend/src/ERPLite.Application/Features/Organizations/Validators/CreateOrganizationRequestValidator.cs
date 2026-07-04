using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;
using ERPLite.Application.Features.Organizations.DTOs;

namespace ERPLite.Application.Features.Organizations.Validators;

public sealed class CreateOrganizationRequestValidator
    : AbstractValidator<CreateOrganizationRequest>
{
    public CreateOrganizationRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Organization name is required.")
            .MaximumLength(200)
            .WithMessage("Organization name cannot exceed 200 characters.");

        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Organization code is required.")
            .MaximumLength(50)
            .WithMessage("Organization code cannot exceed 50 characters.");
    }
}
