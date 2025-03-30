using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace roweb.Domain.Dto
{
    public class CreateCommentDto
    {
        [Required]
        public int MovieId { get; set; }

        [Required]
        public string Text { get; set; } = null!;
    }
}
