using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using roweb.Domain.Dto;
using roweb.Domain.Models;
using roweb.Services.Movies;
using static System.Net.WebRequestMethods;

namespace roweb.Services.Movies
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = null!;
        public MovieService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["TMDB:ApiKey"];
        }

        public async Task<IEnumerable<MovieDetailsDto>> GetLatestMoviesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"movie/now_playing?api_key={_apiKey}");
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<TmdbResponse<MovieDetailsDto>>();
                return result.Results;
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception("Eroare la comunicarea cu api-ul TMDB", httpEx);
            }
            catch (System.Text.Json.JsonException jsonEx)
            {
                throw new Exception("Eroare la procesarea datelor", jsonEx);
            }
            catch (Exception ex)
            {
                throw new Exception("A aparut o eroare neprevazuta", ex);
            }
        }

        public async Task<IEnumerable<MovieDetailsDto>> GetTopMoviesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"movie/top_rated?api_key={_apiKey}");
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<TmdbResponse<MovieDetailsDto>>();
                return result.Results;
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception("Eroare la comunicarea cu api-ul TMDB", httpEx);
            }
            catch (System.Text.Json.JsonException jsonEx)
            {
                throw new Exception("Eroare la procesarea datelor", jsonEx);
            }
            catch (Exception ex)
            {
                throw new Exception("A aparut o eroare neprevazuta", ex);
            }
        }

        public async Task<IEnumerable<MovieDetailsDto>> SearchMoviesAsync(string? name, int? genreId)
        {
            try
            {
                HttpResponseMessage response;
                if (!string.IsNullOrEmpty(name))
                {
                    var url = $"search/movie?api_key={_apiKey}&query={name}";
                    response = await _httpClient.GetAsync(url);
                }
                else if (genreId.HasValue)
                {
                    var url = $"discover/movie?api_key={_apiKey}&with_genres={genreId.Value}";
                    response = await _httpClient.GetAsync(url);
                }
                else
                {
                    return Enumerable.Empty<MovieDetailsDto>();
                }

                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<TmdbResponse<MovieDetailsDto>>();
                if (result is null)
                    return new List<MovieDetailsDto>();
                else
                    return result.Results;
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception("Eroare la comunicarea cu api-ul TMDB", httpEx);
            }
            catch (System.Text.Json.JsonException jsonEx)
            {
                throw new Exception("Eroare la procesarea datelor", jsonEx);
            }
            catch (Exception ex)
            {
                throw new Exception("A aparut o eroare neprevazuta", ex);
            }
        }

        public async Task<MovieDetailsDto> GetMovieDetailsAsync(int movieId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"movie/{movieId}?api_key={_apiKey}&append_to_response=images,credits");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<MovieDetailsDto>();
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception("Eroare la comunicarea cu api-ul TMDB", httpEx);
            }
            catch (System.Text.Json.JsonException jsonEx)
            {
                throw new Exception("Eroare la procesarea datelor", jsonEx);
            }
            catch (Exception ex)
            {
                throw new Exception("A aparut o eroare neprevazuta", ex);
            }
        }
    }
}
