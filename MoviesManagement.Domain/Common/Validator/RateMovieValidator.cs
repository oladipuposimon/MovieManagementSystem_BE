using FluentValidation;
using MoviesManagement.Domain.DataTransferObjects.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.Common.Validator
{
    public class RateMovieValidator: AbstractValidator<RateMovieDto>
    {
        public RateMovieValidator()
        {
                RuleFor(x => x.Rating)
               .GreaterThanOrEqualTo(1).WithMessage("Rating must not be less than 1")
               .LessThanOrEqualTo(5).WithMessage("Rating must not be greater than 5")
              .NotNull().WithMessage("LastName is required");

                RuleFor(x => x.MovieId)
                .NotEmpty()
              .NotNull().WithMessage("MovieId is required");
        }
    }
}
