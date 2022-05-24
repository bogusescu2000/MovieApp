using Behaviour.Interfaces;
using AutoMapper;
using Entities.Entities;
using Entities.Models.NewsDto;
using Entities.Exceptions;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using Resources;

namespace Behaviour.Services
{
    public class NewsService : INewsService
    {
        private readonly IRepository<News> _repository;
        private readonly IMapper _mapper;

        public NewsService(IRepository<News> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all news
        /// </summary>
        /// <returns></returns>
        public async Task<List<NewsDto>> GetAll()
        {
            var newsWithInclude = _repository.GetAllWithIncludes(x => x.Author);
            var newsDto = _mapper.Map<List<NewsDto>>(newsWithInclude);

            return newsDto;
        }

        /// <summary>
        /// Get last 3 news
        /// </summary>
        /// <returns></returns>
        public async Task<List<NewsDto>> GetLast()
        {
            var allNews = await _repository.GetAll();
            var lastNews = _repository.GetAllWithIncludes(x => x.Author).AsQueryable()
            .Skip(Math
            .Max(0, allNews
            .Count() - 3));

            var newsDto = _mapper.Map<List<NewsDto>>(lastNews);
            return newsDto;
        }
        
        /// <summary>
        /// Get news by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<NewsDto> GetNews(int id)
        {
            var news = await _repository.GetByIdWithIncludes(id, x => x.Author);

            var newsDto = _mapper.Map<NewsDto>(news);

            if (news == null)
            {
                throw new Exception(Resource.NewsNotFound);
            }
            else
            {
                return newsDto;
            }
        }
        /// <summary>
        /// Get paginated news
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedNewsDto> GetPagedNews(int page, int pageSize)
        {
            var allNews = await _repository.GetAll();
            var pageCount = Math.Ceiling(allNews.Count() / (float)pageSize);

            var news = _repository.GetAllWithIncludes(x => x.Author).AsQueryable()
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var newsDto = _mapper.Map<List<NewsDto>>(news);

            var response = new PagedNewsDto
            {
                News = newsDto,
                CurrentPage = page,
                Pages = (int)pageCount
            };
            return response;
        }
        /// <summary>
        /// Add a news
        /// </summary>
        /// <param name="newsDto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task AddNews(AddNewsDto newsDto)
        {
            if (newsDto == null)
            {
                throw new ArgumentNullException(nameof(newsDto));
            }
            else
            {
                var news = _mapper.Map<News>(newsDto);
                news.Image = UploadImage(newsDto.Image);
                await _repository.Add(news);
                await _repository.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Update a news
        /// </summary>
        /// <param name="newsDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task UpdateNews(AddNewsDto newsDto, int id)
        {
            var news = await _repository.Get(id);

            if (news == null)
            {
                throw new NotFoundException(Resource.NewsNotFound);
            }
            else
            {
                _mapper.Map(newsDto, news);
                news.Image = UploadImage(newsDto.Image);
                await _repository.Update((News)news);
                await _repository.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Delete a news
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteNews(int id)
        {
            var allNews = await _repository.GetAll();
            var news = allNews.FirstOrDefault(m => m.Id == id);

            if (news == null)
            {
                throw new Exception(Resource.NewsNotFound);
            }
            else
            {
                await _repository.Delete(news.Id);
                await _repository.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Upload a image to wwwrooot
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
