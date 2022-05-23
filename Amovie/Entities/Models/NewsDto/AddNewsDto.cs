using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
namespace Entities.Models.NewsDto
{
    public class AddNewsDto
    {
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "title is too short")]
        public string Title { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 5, ErrorMessage = "content is too short")]
        public string Content { get; set; }
        [Required]
        [Range(typeof(DateTime), "1/1/1900", "12/12/2022")]
        public DateTime Date { get; set; }
        public int? AuthorId { get; set; }
    }
}
