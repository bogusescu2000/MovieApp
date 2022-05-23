using Entities.Models.NewsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Behaviour.Interfaces
{
    public interface INewsService
    {
        Task<List<NewsDto>> GetAll();
        Task<List<NewsDto>> GetLast();
        Task<NewsDto> GetNews(int id);
        Task AddNews(AddNewsDto movie);
        Task UpdateNews(AddNewsDto movie, int id);
        Task DeleteNews(int id);
        Task<PagedNewsDto> GetPagedNews(int page, int pageSize);
    }
}
