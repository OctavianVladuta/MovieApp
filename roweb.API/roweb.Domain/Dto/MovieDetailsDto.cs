using roweb.Domain.Models;
using Roweb.DomainLib.Models;
using System.Text.Json.Serialization;

namespace roweb.Domain.Dto
{
    public class MovieDetailsDto
    {
        [JsonPropertyName("images")]
        public ImagesData Images { get; set; } = null!;

        [JsonPropertyName("credits")]
        public CreditsData Credits { get; set; } = null!;

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; } = null!;

        [JsonPropertyName("adult")]
        public bool Adult { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; } = null!;

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; } = null!;

        [JsonPropertyName("genre_ids")]
        public List<int> GenreIds { get; set; } = null!;

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("original_title")]
        public string OriginalTitle { get; set; } = null!;

        [JsonPropertyName("original_language")]
        public string OriginalLanguage { get; set; } = null!;

        [JsonPropertyName("title")]
        public string Title { get; set; } = null!;

        [JsonPropertyName("backdrop_path")]
        public string BackdropPath { get; set; } = null!;

        [JsonPropertyName("popularity")]
        public float Popularity { get; set; }

        [JsonPropertyName("vote_count")]
        public int VoteCount { get; set; }

        [JsonPropertyName("video")]
        public bool Video { get; set; }

        [JsonPropertyName("vote_average")]
        public float VoteAverage { get; set; }

    }
}