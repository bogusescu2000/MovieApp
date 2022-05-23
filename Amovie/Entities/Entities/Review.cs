namespace Entities.Entities
{
    public class Review : BaseEntity
    {
        public string User { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Content { get; set; }
        public Movie Movie { get; set; }
        public int MovieId { get; set; }
    }
}
