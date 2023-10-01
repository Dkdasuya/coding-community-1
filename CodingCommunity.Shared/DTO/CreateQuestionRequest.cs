using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingCommunity.Shared.DTO
{
    public class CreateQuestionRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public List<string> Tags { get; set; }
    }
}
