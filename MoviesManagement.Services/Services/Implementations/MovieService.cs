using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MoviesManagement.Data;
using MoviesManagement.Data.Repository.Interfaces;
using MoviesManagement.Domain.Common.Model;
using MoviesManagement.Domain.Common.Utilities;
using MoviesManagement.Domain.DataTransferObjects.Dtos;
using MoviesManagement.Domain.Entities;
using MoviesManagement.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Services.Services.Implementations
{
    public class MovieService : BaseService, IMovieService
    {
        private readonly IMovieRepository _movieRepo;
        private readonly ILogger<MovieService> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        readonly IHttpContextAccessor _contextAccessor;
        private readonly IHostEnvironment _hostEnvironment;
        public MovieService(IMovieRepository movieRepo, ILogger<MovieService> logger, ApplicationDbContext dbContext, IMapper mapper, 
            IUserAuthenticationService userAuthenticationService, IHttpContextAccessor contextAccessor, IHostEnvironment hostEnvironment) :base(userAuthenticationService)
        {
            _movieRepo = movieRepo;
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<ResponseModel> AddMovieAsync(AddMovieDto model)
        {
            var response = new ResponseModel();
            try
            {
                var movieRequest = new Movie
                {
                    Name = model.Name,
                    Description = model.Description,
                    TicketPrice = model.TicketPrice,
                    ReleaseDate = model.ReleaseDate,
                    Country = model.Country,
                    Image = model.ImageUrl,
                    Genre = model.Genres.Count(),
                    DateCreated = DateTime.Now
                };
                var movieId = await _movieRepo.AddMovie(movieRequest);
                foreach (var genre in model.Genres)
                {
                    var addGenre = new MovieGenre
                    {
                        MovieId = movieId,
                        Name = genre.Name,
                        DateCreated = DateTime.Now,
                    };
                    await _movieRepo.AddGenre(addGenre);
                }
                response = new ResponseHandler().Success("Movie added successfully", movieId);
            }
            catch (Exception ex)
            {
                response = new ResponseHandler().InternalServerError();
                _logger.LogError(ex, $"An error occured: {ex.Message}");
            }
            return response;
        }
        public async Task<ResponseModel> UpdateMovieAsync(UpdateMovieDto model)
        {
            var response = new ResponseModel();
            try
            {
                var movie = await _movieRepo.GetMovie(model.Id);
               
                movie.Name = model.Name;
                movie.Description = model.Description;
                movie.TicketPrice = model.TicketPrice;
                movie.Image = model.ImageUrl;
                movie.Country = model.Country;
                movie.Genre = model.Genres.Count();
                movie.ReleaseDate = model.ReleaseDate;
                movie.DateModified = DateTime.Now;

                foreach (var genre in model.Genres)
                {
                    var genreData = movie.MovieGenres.FirstOrDefault(x => x.Id == genre.GenreId);
                    if (genreData == null) continue;

                    genreData.Name = genre.Name;
                    genreData.DateModified = DateTime.Now;
                    await _movieRepo.UpdateMovieGenre(genreData);
                }

                _dbContext.Movies.Update(movie);
                await _dbContext.SaveChangesAsync();

                response = new ResponseHandler().Success("Movie updated successfully");
            }
            catch (Exception ex)
            {
                response = new ResponseHandler().InternalServerError();
                _logger.LogError(ex, $"An error occured: {ex.Message}");
            }
            return response;
        }

        public async Task<ResponseModel> GetMovieByIdAsync(IdDto model)
        {
            var response = new ResponseModel();
            try
            {
                var movie = await _movieRepo.GetMovie(model.Id);
                if (movie == null)
                {
                    return response = new ResponseHandler().Failure("No record found");
                }

                var genres = movie.MovieGenres.Select(x => new GenresDto { GenreId = x.Id, Name = x.Name }).ToList(); // Fetching genre names                                                                                                                      
                var result = new MovieResponseDto
                {
                    Id = movie.Id,
                    Name = movie.Name,
                    Description = movie.Description,
                    Genre = movie.Genre,
                    ReleaseDate = movie.ReleaseDate,
                    Country = movie.Country,
                    TicketPrice = movie.TicketPrice,
                    Image = movie.Image,
                    DateCreated = movie.DateCreated,
                    DateModified = movie.DateModified,
                    MovieGenres = genres
                };
                response = new ResponseHandler().Success("Movie feteched successfully", result);
            }
            catch (Exception ex)
            {
                response = new ResponseHandler().InternalServerError();
                _logger.LogError(ex, $"An error occured: {ex.Message}");
            }
            return response;
        }

        public async Task<ResponseModel> GetMoviesAsync()
        {
            var response = new ResponseModel();
            var responseList = new List<MovieResponseDto>();
            try
            {
                var movies = await _movieRepo.GetMovies();
                if (movies.Count == 0)
                {
                    return response = new ResponseHandler().Failure("No record found");
                }
                foreach (var movie in movies)
                {
                    var genres = movie.MovieGenres.Select(x => new GenresDto { GenreId = x.Id, Name = x.Name }).ToList(); // Fetching genre names
                    var result = new MovieResponseDto
                    {
                        Id = movie.Id,
                        Name = movie.Name,
                        Description = movie.Description,
                        Genre = movie.Genre,
                        ReleaseDate = movie.ReleaseDate,
                        Country = movie.Country,
                        TicketPrice = movie.TicketPrice,
                        Image = movie.Image,
                        DateCreated = movie.DateCreated,
                        DateModified = movie.DateModified,
                        MovieGenres = genres
                    };
                    responseList.Add(result);
                }
                response = new ResponseHandler().Success("Movies fetched successfully", responseList);
            }
            catch (Exception ex)
            {
                response = new ResponseHandler().InternalServerError();
                _logger.LogError(ex, $"An error occured: {ex.Message}");
            }
            return response;
        }

        public async Task<ResponseModel> DeleteMovieAsync(IdDto model)
        {
            var response = new ResponseModel();
            try
            {
                var movieToDelete = await _movieRepo.GetMovie(model.Id);
                if (movieToDelete == null)
                {
                    return response = new ResponseHandler().Failure("No record found");
                }

                _dbContext.Movies.Remove(movieToDelete);
                await _dbContext.SaveChangesAsync();

                response = new ResponseHandler().Success("Movie delete successfully");
            }
            catch (Exception ex)
            {
                response = new ResponseHandler().InternalServerError();
                _logger.LogError(ex, $"An error occured: {ex.Message}");
            }
            return response;
        }

        public async Task<ResponseModel> RateMovie(RateMovieDto model)
        {
            var response = new ResponseModel();
            var authuser = UserAuthenticationService.UserContext;
            try
            {
                var movieToRate = await _movieRepo.GetMovie(model.MovieId);
                if (movieToRate == null)
                {
                    return response = new ResponseHandler().Failure("No record found");
                }

                var request = _mapper.Map<MovieRating>(model);
                request.UserId = authuser.Id;
                request.DateAdded = DateTime.Now;
                await _dbContext.MovieRatings.AddAsync(request);
                await _dbContext.SaveChangesAsync();

                response = new ResponseHandler().Success("Rating added successfully");
            }
            catch (Exception ex)
            {
                response = new ResponseHandler().InternalServerError();
                _logger.LogError(ex, $"An error occured: {ex.Message}");
            }
            return response;
        }

        public async Task<ResponseModel> GetMovieRating(GetMovieRatingDto model)
        {
            var response = new ResponseModel();
            var authuser = UserAuthenticationService.UserContext;
            try
            {
                var movieRating = await _movieRepo.MovieRating(model.MovieId);
                if (movieRating.Count == 0)
                {
                    return response = new ResponseHandler().Failure("No record found");
                }
                response = new ResponseHandler().Success("Success", movieRating);
            }
            catch (Exception ex)
            {
                response = new ResponseHandler().InternalServerError();
                _logger.LogError(ex, $"An error occured: {ex.Message}");
            }
            return response;
        }

        public async Task<object> SaveImage(IFormFile imageFile)
        {

            var fileName = Path.GetFileName(imageFile.FileName);
            var fileExtension = Path.GetExtension(fileName);

            if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png")
            {
                throw new ArgumentException("Invalid image format");
            }

            var folder = "Resources/Image"; // Relative path within the project directory
            string uploadFolder = Path.Combine(_hostEnvironment.ContentRootPath, folder);

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            string uniqueFileName = $"{Guid.NewGuid().ToString()}{fileExtension}";
            string filePath = Path.Combine(uploadFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            Uri fileUri = new Uri(filePath);
            return new { ImageUrl = fileUri }; ; // Return the local file path as a string
        }

    }
}
