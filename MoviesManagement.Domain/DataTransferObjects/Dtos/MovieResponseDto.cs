using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.DataTransferObjects.Dtos
{
    public class MovieResponseDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Rating { get; set; }
        public int Genre { get; set; }
        public decimal TicketPrice { get; set; }
        public string? Country { get; set; }
        public string? Image { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public List<GenresDto>? MovieGenres { get; set; }
    }
}
