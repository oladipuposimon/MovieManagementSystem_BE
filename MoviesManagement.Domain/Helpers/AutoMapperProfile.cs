using AutoMapper;
using MoviesManagement.Domain.DataTransferObjects.Dtos;
using MoviesManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddMovieDto, Movie>();
            CreateMap<Movie, AddMovieDto>();

            CreateMap<MovieGenre, AddGenreDto>();
            CreateMap<AddGenreDto, MovieGenre>();

            CreateMap<Movie, MovieResponseDto>();

            CreateMap<User, CreateUserDto>();
            CreateMap<CreateUserDto, User>();

            CreateMap<MovieRating, RateMovieDto>();
            CreateMap<RateMovieDto, MovieRating>();
        }
    }
}
