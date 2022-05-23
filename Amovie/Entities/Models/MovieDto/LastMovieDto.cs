using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.MovieDto
{
    public class LastMovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        [Range(typeof(DateTime), "1/1/1900", "1/1/2022")]
        public DateTime Release { get; set; }
        [Range(1, 10)]
        public float Rating { get; set; }
    }
}
