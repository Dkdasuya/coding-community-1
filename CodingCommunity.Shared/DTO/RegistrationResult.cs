using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingCommunity.Shared.DTO
{
    public class RegistrationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public RegistrationResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }

}
