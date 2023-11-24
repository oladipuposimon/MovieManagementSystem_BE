using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesManagement.Data.Repository.Interfaces;
using MoviesManagement.Data.Repository.Implementations;

namespace MoviesManagement.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMovieRepository, MovieRepository>();


            return services;
        }
    }
}
