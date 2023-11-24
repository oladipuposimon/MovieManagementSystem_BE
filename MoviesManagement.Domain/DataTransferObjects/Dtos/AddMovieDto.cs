using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.DataTransferObjects.Dtos
{
    public class AddMovieDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal TicketPrice { get; set; }
        public string? Country { get; set; }
        public List<GenreDto> Genres { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class GenreDto
    {
        public string? Name { get; set; }
    }
}
