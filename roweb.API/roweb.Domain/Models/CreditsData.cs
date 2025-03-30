using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace roweb.Domain.Models
{
    public class CreditsData
    {
        [JsonPropertyName("cast")]
        public List<CastMember> Cast { get; set; } = null!;

        [JsonPropertyName("crew")]
        public List<CrewMember> Crew { get; set; } = null!;
    }
}
