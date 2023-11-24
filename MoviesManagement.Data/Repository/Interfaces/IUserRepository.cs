using MoviesManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(Guid id);
        Task<User> GetUserByEmail(string email);
    }
}
