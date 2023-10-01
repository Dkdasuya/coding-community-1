using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingCommunity.Shared.DTO
{
        public class RegistrationRequest
        {
            [Required]
            public string Username { get; set; }

            [Required]
            [MinLength(8, ErrorMessage = "The password must be at least 8 characters.")]
            public string Password { get; set; }
        }
}
