using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.Helpers
{
    public class AppSettings
    {
        public string? JwtSecret { get; set; }
        public TimeSpan? JwtTokenLifetime { get; set; }
        public string? JwtIssuer { get; set; }
    }
}
