using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.Common.Model
{
    public class ResponseModel
    {
        public bool isSuccessful { get; set; }
        public string? Message { get; set; }
        public int? StatusCode { get; set; }
        public object? Data { get; set; }
    }
}
