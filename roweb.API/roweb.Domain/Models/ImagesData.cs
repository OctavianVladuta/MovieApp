using roweb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Roweb.DomainLib.Models
{
    public class ImagesData
    {
        [JsonPropertyName("backdrops")]
        public List<Backdrop> Backdrops { get; set; } = null!;

        [JsonPropertyName("posters")]
        public List<Poster> Posters { get; set; } = null!;
    }
}
