using MoviesManagement.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.Entities
{
    public class MovieGenre
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid MovieId { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        [ForeignKey("MovieId")]
        public Movie? Movie { get; set; }

    }
}
