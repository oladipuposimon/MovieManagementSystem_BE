using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.Entities
{
    public class MovieRating
    {
        public Guid Id { get; set; }
        public Guid MovieId { get; set; }
        public Guid UserId { get; set; }
        public int Rating { get; set; }
        public DateTime? DateAdded { get; set; }


        [ForeignKey("MovieId")]
        public Movie? Movie { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
