using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace roweb.Domain.Models
{
    public class CastMember
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Character { get; set; } = null!;
        public string Profile_Path { get; set; } = null!;
    }
}
