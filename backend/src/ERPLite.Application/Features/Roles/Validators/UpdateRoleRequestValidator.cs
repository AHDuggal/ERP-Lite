using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ERPLite.Application.Features.Roles.DTOs;
using FluentValidation;

namespace ERPLite.Application.Features.Roles.Validators;

public sealed class UpdateRoleRequestValidator  : AbstractValidator<UpdateRoleRequest>
{
    public UpdateRoleRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Role id is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Role name is required.")
            .MaximumLength(100)
            .WithMessage("Role name cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Role description is required.")
            .MaximumLength(250)
            .WithMessage("Role description cannot exceed 250 characters.");
    }
}
