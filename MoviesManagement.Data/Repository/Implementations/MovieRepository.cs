using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesManagement.Data.Repository.Interfaces;
using MoviesManagement.Domain.Common.Model;
using MoviesManagement.Domain.DataTransferObjects.Dtos;
using MoviesManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Data.Repository.Implementations
{
    public class MovieRepository: IMovieRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MovieRepository> _logger;

        public MovieRepository(ApplicationDbContext context, ILogger<MovieRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Guid> AddMovie(Movie model)
        {
            try
            {
                model.Id = new Guid();
                await _context.Movies.AddAsync(model);
                await _context.SaveChangesAsync();
                
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"An error occured: {ex.Message}");
                throw;
            }
            return model.Id;
        }

        public async Task<Guid> AddGenre(MovieGenre model)
        {
            try
            {
                await _context.MovieGenres.AddAsync(model);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occured: {ex.Message}");
                throw;
            }
            return model.Id;
        }

        public async Task<Guid> UpdateMovieGenre(MovieGenre model)
        {
            try
            {
                _context.MovieGenres.Update(model);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occured: {ex.Message}");
                throw;
            }
            return model.Id;
        }

        public async Task<Movie> GetMovie(Guid id)
        {
            var response = new Movie();
            try
            {
                response = await _context.Movies.AsNoTracking().Include(x => x.MovieGenres).FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occured: {ex.Message}");
                throw;
            }
            return response;
        }

        public async Task<List<Movie>> GetMovies()
        {
            var response = new List<Movie>();
            try
            {
                response = await _context.Movies.AsNoTracking().Include(x => x.MovieGenres).OrderByDescending(x => x.DateCreated).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occured: {ex.Message}");
                throw;
            }
            return response;
        }

        public async Task<List<MovieRatingResponse>> MovieRating(Guid movieId)
        {
            try
            {
                var movieRating = await _context.MovieRatings.Where(x => x.MovieId == movieId).OrderByDescending(x => x.DateAdded)
                  .Include(mr => mr.User)
                  .Include(mr => mr.Movie)
                  .Select(mr => new MovieRatingResponse
                  {
                      userRating = new UserRatingDto
                      {
                          UserId = mr.User.Id,
                          FirstName = mr.User.FirstName,
                          LastName = mr.User.LastName,
                          Rating = mr.Rating,
                      },
                      Movie = new MovieDto
                      {
                          MovieId = mr.Movie.Id,
                          MovieName = mr.Movie.Name,
                      },
                  })
                  .ToListAsync();


                return movieRating;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred: {ex.Message}");
                throw;
            }
        }

    }
}
