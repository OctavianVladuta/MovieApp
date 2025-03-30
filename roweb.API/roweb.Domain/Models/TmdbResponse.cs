namespace roweb.Domain.Models
{
    public class TmdbResponse<T>
    {
        public int Page { get; set; }
        public List<T> Results { get; set; } = null!;
    }
}