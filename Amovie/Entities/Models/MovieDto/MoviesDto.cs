using System.ComponentModel.DataAnnotations;


namespace Entities.Models.MovieDto
{
    public class MoviesDto
    {
        public int Id { get; set; }
        [MaxLength(30)]
        [Required]
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        [Range(typeof(DateTime), "1/1/1900", "1/1/2022")]
        public DateTime Release { get; set; }
        [Range(1, 10)]
        public float Rating { get; set; }
        public int Duration { get; set; }
    }
}
