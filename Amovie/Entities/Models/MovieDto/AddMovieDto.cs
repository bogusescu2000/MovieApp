
using System.Text.Json.Serialization;

namespace Entities.Models.MovieDto
{
    public class AddMovieDto
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public DateTime Release { get; set; }
        public float Rating { get; set; }
        public int Duration { get; set; }
        public string Country { get; set; }
        public float Budget { get; set; }
        public List<int> GenreId { get; set; }
        public List<int> ActorId { get; set; }
    }
}
