using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Entities
{
    public class AppToken
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public DateTime Expiration { get; set; }
    }
}
