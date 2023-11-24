using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesManagement.Api.Authorization;
using MoviesManagement.Domain.Common.Validator;
using MoviesManagement.Domain.DataTransferObjects.Dtos;
using MoviesManagement.Services.Services.Interfaces;

namespace MoviesManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost]
        [Route("UploadImage")]
        public async Task<IActionResult> SaveImage(IFormFile file)
        {
            var response = await _movieService.SaveImage(file);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();

        }

        [HttpPost]
        [Route("AddMovie")]
        public async Task<IActionResult> AddMovieAsync(AddMovieDto model)
        {
            var validation = new AddMovieValidator().Validate(model);

            if (!validation.IsValid)
            {
                var errorMessages = validation.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new { errors = errorMessages });
            }
            var response = await _movieService.AddMovieAsync(model);
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

       
        [HttpPut]
        [Route("UpdateMovie")]
        public async Task<IActionResult> UpdateMovieAsync(UpdateMovieDto model)
        {
            var response = await _movieService.UpdateMovieAsync(model);
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

        [HttpGet]
        [Route("GetMovieById")]
        public async Task<IActionResult> GetMovieByIdAsync([FromQuery]IdDto model)
        {
            var response = await _movieService.GetMovieByIdAsync(model);
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

        [HttpGet]
        [Route("GetMovies")]
        public async Task<IActionResult> GetMoviesAsync()
        {
            var response = await _movieService.GetMoviesAsync();
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

        [HttpDelete]
        [Route("DeleteMovie")]
        public async Task<IActionResult> DeleteMovieAsync(IdDto model)
        {
            var response = await _movieService.DeleteMovieAsync(model);
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
        [Route("RateMovie")]
        public async Task<IActionResult> RateMovie(RateMovieDto model)
        {
            var validation = new RateMovieValidator().Validate(model);

            if (!validation.IsValid)
            {
                var errorMessages = validation.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new { errors = errorMessages });
            }

            var response = await _movieService.RateMovie(model);
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

        [HttpGet]
        [Route("GetMovieRating")]
        public async Task<IActionResult> GetMovieRating([FromQuery]GetMovieRatingDto model)
        {
            var response = await _movieService.GetMovieRating(model);
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
