using FluentValidation;
using System;

namespace RecruitTask.Models.Validators
{
    public class CompanyValidator: AbstractValidator<CompanyRequest>
    {
        public CompanyValidator()
        {
            RuleFor(c => c.EstablishmentYear).Must(x=>x>0 && x<=DateTime.Now.Year).WithMessage("eneterd year shuld be greater than 0 and not greater than current year");
            RuleFor(c => c.Name).NotEmpty().WithMessage("company name is required");
            RuleForEach(c => c.Employees).SetValidator(new EmployerValidator());
        }
    }
}
