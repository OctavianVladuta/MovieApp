namespace roweb.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string UserName { get; set; } = null!;
        public string Text { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}