using Behaviour.Interfaces;
using Behaviour.Repositories;
using Behaviour.Services;
using BLL.Interfaces;
using BLL.Services;

namespace Amovie.Configuration
{
    public static class ControllerConfig
    {
        public static IServiceCollection RegisterControllers(this IServiceCollection services)
        {
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IActorService, ActorService>();

            return services;
        }
    }
}