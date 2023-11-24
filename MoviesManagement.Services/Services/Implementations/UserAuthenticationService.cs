using Microsoft.AspNetCore.Http;
using MoviesManagement.Domain.Entities;
using MoviesManagement.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Services.Services.Implementations
{
    public class UserAuthenticationService : IUserAuthenticationService
    {

        readonly IHttpContextAccessor ContextAccessor;

        public UserAuthenticationService(IHttpContextAccessor contextAccessor)
        {
            ContextAccessor = contextAccessor;
        }


        public User UserContext
        {
            get
            {
                return (User)ContextAccessor.HttpContext.Items["User"];
            }
        }

    }
}
