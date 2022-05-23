using AutoMapper;
using Entities.Entities;
using Entities.Models.ActorDto;
using Entities.Models.AuthorDto;
using Entities.Models.GenreDto;
using Entities.Models.MovieDto;
using Entities.Models.NewsDto;
using Entities.Models.ReviewDto;

namespace Entities.Profiler
{
    public class MovieProfiler : Profile
    {
        public MovieProfiler()
        {
            //movie
            CreateMap<Movie, LastMovieDto>().ReverseMap();
            CreateMap<Movie, PagedMovieDto>().ReverseMap();
            CreateMap<Movie, MoviesDto>().ReverseMap();
            CreateMap<AddMovieDto, Movie>().ReverseMap();

            CreateMap<Movie, SingleMovieDto>()
                .ForMember(m => m.Actors,
                o => o.MapFrom(m => m.Actors!.Select(m => m.FirstName + " " + m.LastName)))
                .ForMember(m => m.Genres,
                o => o.MapFrom(m => m.Genres!.Select(m => m.Name)))
                .ReverseMap();

            //news
            CreateMap<News, NewsDto>()
                .ForMember(n => n.AuthorName,
               x => x.MapFrom(m => m.Author!.FirstName + " " + m.Author.LastName))
                .ReverseMap();
            CreateMap<News, AddNewsDto>().ReverseMap();
            CreateMap<PagedNewsDto, News>().ReverseMap();


            //Review
            CreateMap<Review, DisplayReviewDto>().ReverseMap();
            CreateMap<Review, AddReviewDto>()
                .ReverseMap();

            //Author
            CreateMap<Author, AuthorDto>().ReverseMap();

            //Genre
            CreateMap<Genre, GenreDto>().ReverseMap();

            //Actor
            CreateMap<Actor, ActorDto>().ReverseMap();
        }
    }
}
