using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.API.DTOs
{
    public class AppTokenDTO
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public DateTime Expiration { get; set; }
    }
}
