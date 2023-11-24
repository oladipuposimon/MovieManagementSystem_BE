using MoviesManagement.Domain.DataTransferObjects.Dtos;
using MoviesManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Data.Repository.Interfaces
{
    public interface IMovieRepository
    {
        Task<Guid> AddMovie(Movie model);
        Task<Guid> AddGenre(MovieGenre model);
        Task<Guid> UpdateMovieGenre(MovieGenre model);
        Task<Movie> GetMovie(Guid id);
        Task<List<Movie>> GetMovies();
        Task<List<MovieRatingResponse>> MovieRating(Guid movieId);
    }
}
