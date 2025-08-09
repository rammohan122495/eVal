using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eValDTO.DTOs
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public string UserName { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public bool IsSuceed { get; set; }
    }
}
