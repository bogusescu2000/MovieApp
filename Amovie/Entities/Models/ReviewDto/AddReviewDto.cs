using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.ReviewDto
{
    public class AddReviewDto
    {
        public string User { get; set; }
        public string Content { get; set; }
        public int? MovieId { get; set; }
    }
}
