using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoviesManagement.Domain.Helpers;
using MoviesManagement.Services.Services.Implementations;
using MoviesManagement.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IJwtUtils, JwtUtils>();
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<IUserAuthenticationService, UserAuthenticationService>();

            return services;
        }
    }
}
