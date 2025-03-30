using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace roweb.Domain.Models
{
    public class Backdrop
    {
        [JsonPropertyName("file_path")]
        public string FilePath { get; set; } = null!;
    }
}
