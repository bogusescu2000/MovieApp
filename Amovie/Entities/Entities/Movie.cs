using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities.Entities
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        [Range(typeof(DateTime), "1/1/1900", "12/12/2022")]
        public DateTime Release { get; set; }
        [Range(1, 10)]
        public float Rating { get; set; }
        public int Duration { get; set; }
        public string Country { get; set; }
        public float Budget { get; set; }
        public List<Review>? Reviews { get; set; }
        public List<Genre>? Genres { get; set; }
        public List<Actor>? Actors { get; set; }
    }
}
