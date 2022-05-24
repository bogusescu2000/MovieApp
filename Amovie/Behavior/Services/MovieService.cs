using AutoMapper;
using Behaviour.Interfaces;
using DataAccess.Data;
using Entities.Entities;
using Entities.Models.MovieDto;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Resources;

namespace Behaviour.Services
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _repository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public MovieService(IRepository<Movie> repository, IMapper mapper, DataContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }
        /// <summary>
        /// Get all movies
        /// </summary>
        /// <returns></returns>
        public async Task<List<MoviesDto>> GetAll()
        {
            var movies = await _repository.GetAll();

            var moviesDto = _mapper.Map<List<MoviesDto>>(movies);

            return moviesDto;
        }

        /// <summary>
        /// Get last 6 movies
        /// </summary>
        /// <returns></returns>
        public async Task<List<LastMovieDto>> GetLast()
        {
            var allMovies = await _repository.GetAll();
            var lastMovies = allMovies
            .Skip(Math
            .Max(0, allMovies
            .Count() - 6));

            var moviesDto = _mapper.Map<List<LastMovieDto>>(lastMovies);
            return moviesDto;
        }

        /// <summary>
        /// Get movie by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<SingleMovieDto> GetMovie(int id)
        {
            var movie = await _repository.GetByIdWithIncludes(id, x => x.Genres, x => x.Actors, x => x.Reviews);

            var movieDto = _mapper.Map<SingleMovieDto>(movie);

            if (movie == null)
            {
                throw new Exception(Resource.MovieNotFound);
            }
            else
            {
                return movieDto;
            }
        }
        /// <summary>
        /// Add a movie
        /// </summary>
        /// <param name="movieDto"></param>
        /// <returns></returns>
        public async Task AddMovie(AddMovieDto movieDto)
        {
            var genres = await _context.Genres.Where(x => movieDto.GenreId.Contains(x.Id)).ToListAsync();
            var actors = await _context.Actors.Where(x => movieDto.ActorId.Contains(x.Id)).ToListAsync();

            var movie = _mapper.Map<Movie>(movieDto);
            movie.Image = UploadImage(movieDto.Image);
            movie.Genres = genres;
            movie.Actors = actors;

            await _repository.Add(movie);
            await _repository.SaveChangesAsync();
        }

        /// <summary>
        /// Update a movie
        /// </summary>
        /// <param name="movieDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task UpdateMovie(AddMovieDto movieDto, int id)
        {
            var genres = await _context.Genres.Where(x => movieDto.GenreId.Contains(x.Id)).ToListAsync();
            var actors = await _context.Actors.Where(x => movieDto.ActorId.Contains(x.Id)).ToListAsync();

            var movie = await _repository.Get(id);

            movie = _mapper.Map<Movie>(movieDto);
            movie.Genres = genres;
            movie.Actors = actors;

            await _repository.Update(movie);
            await _repository.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a movie by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteMovie(int id)
        {
            var allMovies = await _repository.GetAll();

            var movie = allMovies.FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                throw new Exception(Resource.MovieNotFound);
            }
            else
            {
                await _repository.Delete(movie.Id);
                await _repository.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get paged movies with sorting and pagination options
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sort"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task<PagedMovieDto> GetPagedMovies(int page, int pageSize, string? sort, string? title)
        {
            var allMovies = await _repository.GetAll();

            if (title != null)
            {
                allMovies = allMovies.Where(x => x.Title.ToLower().Contains(title.ToLower())).ToList();
            }

            if (sort == "asc")
            {
                allMovies = allMovies.OrderBy(x => x.Rating).ToList();
            }
            else if (sort == "desc")
            {
                allMovies = allMovies.OrderByDescending(x => x.Rating).ToList();
            }

            if(sort == "ascDate")
            {
                allMovies = allMovies.OrderBy(x => x.Release).ToList();
            }
            else if(sort == "descDate")
            {
                allMovies = allMovies.OrderByDescending(x => x.Release).ToList();
            }

            var pageCount = Math.Ceiling(allMovies.Count() / (float)pageSize);

            var movies = allMovies
                .Skip((page - 1) * (int)pageSize)
                .Take((int)pageSize);

            var moviesDto = _mapper.Map<List<MoviesDto>>(movies);

            var pagedMovies = new PagedMovieDto
            {
                Movies = moviesDto,
                CurrentPage = page,
                Pages = (int)pageCount
            };
            return pagedMovies;
        }
        /// <summary>
        /// Upload an image to wwwroot
        /// </summary>
        /// <param name="fileImage"></param>
        /// <returns></returns>
        private string UploadImage(IFormFile fileImage)
        {
            if (fileImage != null)
            {
                var uniqueImageName = Guid.NewGuid().ToString() + "_" + fileImage.FileName;
                string uploadsFolder = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images")).Root;
                string filePath = Path.Combine(uploadsFolder, uniqueImageName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    fileImage.CopyTo(fileStream);
                }
                return uniqueImageName;
            }
            return null;
        }
    }
}
