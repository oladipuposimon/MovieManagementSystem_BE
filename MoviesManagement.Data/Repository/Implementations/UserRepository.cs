using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MoviesManagement.Data.Repository.Interfaces;
using MoviesManagement.Domain.Entities;
using MoviesManagement.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Data.Repository.Implementations
{
    public class UserRepository: IUserRepository
    {
        public readonly ApplicationDbContext _context;
        private readonly ILogger<UserRepository> _logger;
        private readonly IMapper _mapper;
        private readonly IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;
        public UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger, IMapper mapper, IOptions<AppSettings> appSettings, IJwtUtils jwtUtils)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _jwtUtils = jwtUtils;
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            var user = new User();
            try
            {
                user = await _context.Users.FindAsync(id);
                if (user == null) { throw new KeyNotFoundException("User not found"); }               
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"An error occured: {ex.Message}");
                throw;
            }
            return user;

        }

        public async Task<User> GetUserByEmail(string emailAddress)
        {
            var user = new User();

            try
            {
                user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.EmailAddress.Equals(emailAddress));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"An error occured: {ex.Message}");
                throw;
            }
            return user;
        }
    }
}
