using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.DataTransferObjects.Dtos
{
    public class UpdateMovieDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal TicketPrice { get; set; }
        public string? Country { get; set; }
        public List<GenresDto>? Genres { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class GenresDto
    {
        public Guid GenreId { get; set; }
        public string? Name { get; set; }
    }
}
