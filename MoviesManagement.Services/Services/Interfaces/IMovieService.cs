using Microsoft.AspNetCore.Http;
using MoviesManagement.Domain.Common.Model;
using MoviesManagement.Domain.DataTransferObjects.Dtos;
using MoviesManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Services.Services.Interfaces
{
    public interface IMovieService
    {
        Task<object> SaveImage(IFormFile imageFile);
        Task<ResponseModel> AddMovieAsync(AddMovieDto model);
        Task<ResponseModel> UpdateMovieAsync(UpdateMovieDto model);
        Task<ResponseModel> GetMovieByIdAsync(IdDto model);
        Task<ResponseModel> GetMoviesAsync();
        Task<ResponseModel> DeleteMovieAsync(IdDto model);
        Task<ResponseModel> RateMovie(RateMovieDto model);
        Task<ResponseModel> GetMovieRating(GetMovieRatingDto model);
    }
}
