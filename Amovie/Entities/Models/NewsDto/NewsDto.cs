using Entities.Entities;

namespace Entities.Models.NewsDto
{
    public class NewsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string AuthorName { get; set; }
    }
}
