using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.DataTransferObjects.Dtos
{
    public class RateMovieDto
    {
        public Guid MovieId { get; set; }
        public int Rating { get; set; }
    }

    public class GetMovieRatingDto
    {
        public Guid MovieId { get; set; }
    }

    public class MovieRatingResponse
    {
        public UserRatingDto? userRating { get; set; }
        public MovieDto? Movie { get; set; }
    }

    public class UserRatingDto
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Rating { get; set; }
    }

    public class MovieDto
    {
        public Guid MovieId { get; set; }
        public string? MovieName { get; set; }
    }
}
