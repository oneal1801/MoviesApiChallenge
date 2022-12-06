using AutoMapper;
using MoviesApiChallenge.Dtos;
using MoviesApiChallenge.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MoviesApiChallenge.Utilities.MapperProfilers
{
    public class GenericAutoMapperProfile : Profile
    {
        public GenericAutoMapperProfile()
        {


            CreateMap<Movie, MovieDto>().ReverseMap();
            CreateMap<MovieDto, Movie>()
                .ReverseMap();


            CreateMap<Reviews, ReviewDto>().ReverseMap();
            CreateMap<ReviewDto, Reviews>()
                .ReverseMap();
        }
    }
}
