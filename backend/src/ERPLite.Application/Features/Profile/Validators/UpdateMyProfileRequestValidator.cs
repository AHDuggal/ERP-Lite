using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ERPLite.Application.Features.Profile.DTOs;
using FluentValidation;

namespace ERPLite.Application.Features.Profile.Validators;

public sealed class UpdateMyProfileRequestValidator : AbstractValidator<UpdateMyProfileRequest>
{
    public UpdateMyProfileRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("First name is required.")
            .MaximumLength(100)
            .WithMessage("First name cannot exceed 100 characters.");

        RuleFor(x => x.LastName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .MaximumLength(100)
            .WithMessage("Last name cannot exceed 100 characters.");

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(20)
            .WithMessage("Phone number cannot exceed 20 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

        RuleFor(x => x.JobTitle)
            .MaximumLength(100)
            .WithMessage("Job title cannot exceed 100 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.JobTitle));

        RuleFor(x => x.Department)
            .MaximumLength(100)
            .WithMessage("Department cannot exceed 100 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Department));
    }
}
