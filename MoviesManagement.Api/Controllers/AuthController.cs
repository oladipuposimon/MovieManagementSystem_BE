using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesManagement.Domain.Common.Validator;
using MoviesManagement.Domain.DataTransferObjects.Dtos;
using MoviesManagement.Services.Services.Interfaces;

namespace MoviesManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateAsync(CreateUserDto model)
        {
            var validation = new CreateUserValidator().Validate(model);

            if (!validation.IsValid)
            {
                var errorMessages = validation.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new { errors = errorMessages });
            }
            var response = await _userService.CreateAsync(model);
            if (response.isSuccessful)
            {
                return Ok(response);
            }
            else if (!response.isSuccessful && response.StatusCode == StatusCodes.Status400BadRequest)
            {
                return BadRequest(response);
            }
            return StatusCode(500, response);

        }

        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate(AuthDto model)
        {
            var validation = new AuthUserValidator().Validate(model);

            if (!validation.IsValid)
            {
                var errorMessages = validation.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new { errors = errorMessages });
            }
            var response = await _userService.Authenticate(model);
            if (response.isSuccessful)
            {
                return Ok(response);
            }
            else if (!response.isSuccessful && response.StatusCode == StatusCodes.Status400BadRequest)
            {
                return BadRequest(response);
            }
            return StatusCode(500, response);

        }

    }
}
