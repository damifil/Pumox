using FluentValidation;

namespace RecruitTask.Models.Validators
{
    public class EmployerValidator : AbstractValidator<EmployerRequest>
    {
        public EmployerValidator()
        {
            RuleFor(e => e.DateOfBirth).NotEmpty().WithMessage("Date of birth is required");
            RuleFor(e => e.FirstName).NotEmpty().WithMessage("Employees first name is required");
            RuleFor(e=>e.LastName).NotEmpty().WithMessage("Employees last name is required");
            RuleFor(e => e.JobTitle).NotNull().WithMessage("Job title is required");
        }
    }
}
