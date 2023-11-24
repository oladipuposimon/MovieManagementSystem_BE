using MoviesManagement.Domain.Entities;
using MoviesManagement.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Services.Services
{
    public class BaseService
    {
        protected IUserAuthenticationService UserAuthenticationService;

        public BaseService(IUserAuthenticationService userAuthenticationService)
        {
            UserAuthenticationService = userAuthenticationService;
        }

        public User UserContext
        {
            get
            {
                return UserAuthenticationService.UserContext;
            }
        }
    }
}
