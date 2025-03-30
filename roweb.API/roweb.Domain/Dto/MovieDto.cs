namespace roweb.Domain.Dto
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public List<int> GenreIds { get; set; } = null!;

    }
}