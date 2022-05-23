using System.Text.Json.Serialization;

namespace Entities.Entities
{
    public class News : BaseEntity
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
