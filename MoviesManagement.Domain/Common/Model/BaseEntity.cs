using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.Common.Model
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }

        public void OnCreate(string? createdBy)
        {
            this.CreatedBy = createdBy;
            this.DateCreated = DateTime.UtcNow;
        }

        public void OnModify(string? modifiedBy)
        {
            this.ModifiedBy = modifiedBy;
            this.DateModified = DateTime.UtcNow;
        }
    }
}
