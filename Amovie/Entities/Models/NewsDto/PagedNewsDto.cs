


namespace Entities.Models.NewsDto
{
    public class PagedNewsDto
    {
        public List<NewsDto> News { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
