using MoviesManagement.Domain.Common.Model;
using MoviesManagement.Domain.DataTransferObjects.Dtos;
using MoviesManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Services.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseModel> CreateAsync(CreateUserDto model);
        Task<ResponseModel> Authenticate(AuthDto model);
        Task<User> GetUserAsync(Guid id);
    }
}
