using CrudSqlServerDapper.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudSqlServerDapper.Validators
{
    /// <summary>
    /// Validation class for Client entity
    /// </summary>
    public class ClientValidator : AbstractValidator<Client>
    {
        //Constructor Method
        public ClientValidator()
        {
            //Name validation
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(6).WithMessage("Name must be at least 6 characters");

            //Email validation
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            //BirthDate validation
            RuleFor(c => c.BirthDate)
                .NotEmpty().WithMessage("Birth date is required.")
                .LessThan(DateTime.Now).WithMessage("Birth date must be in the past.")
                .GreaterThan(DateTime.Now.AddYears(-120)).WithMessage("Birth date must be within the last 120 years.");
        }
    }
}
