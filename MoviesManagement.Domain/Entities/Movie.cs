using MoviesManagement.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.Entities
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Genre { get; set; }
        public decimal TicketPrice { get; set; }
        public string? Country { get; set; }
        public string? Image { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        
        public ICollection<MovieGenre> MovieGenres { get; set; }

    }
}
