using FluentValidation;
using MoviesManagement.Domain.DataTransferObjects.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.Common.Validator
{
    public class AuthUserValidator: AbstractValidator<AuthDto>
    {
        public AuthUserValidator()
        {
            RuleFor(x => x.EmailAddress)
             .EmailAddress()
             .NotNull().WithMessage("EmailAddress is required");
        }
    }
}
