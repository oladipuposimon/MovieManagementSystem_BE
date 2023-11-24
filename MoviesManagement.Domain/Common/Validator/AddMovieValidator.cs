using FluentValidation;
using MoviesManagement.Domain.DataTransferObjects.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.Common.Validator
{
    public class AddMovieValidator : AbstractValidator<AddMovieDto>
    {
        public AddMovieValidator()
        {
            RuleFor(x => x.Name)
             .NotEmpty()
              .NotNull().WithMessage("Name is required");

            RuleFor(x => x.Description)
               .NotEmpty()
               .NotNull().WithMessage("Description is required");

            RuleFor(x => x.ReleaseDate)
               .NotEmpty()
               .NotNull().WithMessage("ReleaseDate is required");

            RuleFor(x => x.Genres)
               .NotEmpty()
               .NotNull().WithMessage("Genres is required");

            RuleFor(x => x.Country)
               .NotEmpty()
               .NotNull().WithMessage("Country is required");

            RuleFor(x => x.TicketPrice)
               .NotEmpty()
               .NotNull().WithMessage("TicketPrice is required");

            RuleFor(x => x.ImageUrl)
              .NotEmpty()
              .NotNull().WithMessage("ImageUrl is required");
        }
    }
}
