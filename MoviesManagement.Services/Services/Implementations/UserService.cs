using AutoMapper;
using Microsoft.Extensions.Logging;
using MoviesManagement.Data;
using MoviesManagement.Data.Repository.Interfaces;
using MoviesManagement.Domain.Common.Model;
using MoviesManagement.Domain.Common.Utilities;
using MoviesManagement.Domain.DataTransferObjects.Dtos;
using MoviesManagement.Domain.Entities;
using MoviesManagement.Domain.Helpers;
using MoviesManagement.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Services.Services.Implementations
{
    public class UserService: IUserService
    {
        public readonly IUserRepository _userRepo;
        private readonly ILogger<User> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IJwtUtils _jwtUtils;

        public UserService(IUserRepository userRepo, ILogger<User> logger, IMapper mapper, ApplicationDbContext context, IJwtUtils jwtUtils)
        {
            _userRepo = userRepo;
            _logger = logger;
            _mapper = mapper;
            _context = context;
            _jwtUtils = jwtUtils;
        }

        public async Task<ResponseModel> CreateAsync(CreateUserDto model)
        {
            var response = new ResponseModel();
            try
            {
                var userExist = await _userRepo.GetUserByEmail(model.EmailAddress);
                if(userExist != null)
                {
                    return response = new ResponseHandler().Failure("User already exist");
                }
                var user = _mapper.Map<User>(model);
                user.OnCreate($"{user.FirstName} {user.LastName}");
                await _context.Users.AddAsync(user);
                var saveChanges = await _context.SaveChangesAsync();
                response = new ResponseHandler().Success("User created successfully");
            }
            catch (Exception ex)
            {
                response = new ResponseHandler().InternalServerError();
                _logger.LogError(ex, $"An error occured: {ex.Message}"); 
            }
            return response;
        }

        public async Task<ResponseModel> Authenticate(AuthDto model)
        {
            var response = new ResponseModel();
            string jwtToken;
            try
            {
                var user = await _userRepo.GetUserByEmail(model.EmailAddress);
                if (user == null)
                {
                    return response = new ResponseHandler().Failure("User does not exist.");
                }
                jwtToken = _jwtUtils.GenerateJwtToken(user);
                response = new ResponseHandler().Success("Success", jwtToken);
            }
            catch(Exception ex)
            {
                response = new ResponseHandler().InternalServerError();
                _logger.LogError(ex, $"An error occured: {ex.Message}");
            }
           
            return response;
        }
        public async Task<User> GetUserAsync(Guid id)
        {
            return await _userRepo.GetUserAsync(id);
        }
    }
}
