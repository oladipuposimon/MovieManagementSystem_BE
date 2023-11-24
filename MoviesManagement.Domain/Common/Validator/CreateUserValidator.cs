using FluentValidation;
using MoviesManagement.Domain.DataTransferObjects.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.Common.Validator
{
    public class CreateUserValidator: AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FirstName)
             .NotEmpty()
             .NotNull().WithMessage("FirstName is required");

            RuleFor(x => x.LastName)
           .NotEmpty()
           .NotNull().WithMessage("LastName is required");

            RuleFor(x => x.EmailAddress)
          .EmailAddress()
          .NotNull().WithMessage("LastName is required");
        }
    }
}
