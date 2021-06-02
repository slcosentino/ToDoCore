using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Entities
{
    public class Pagination
    {
        public int Page { get; set; }

        public int RecordsPerPage { get; set; }

        public Pagination()
        {
            Page = 1;
            RecordsPerPage = 10;
        }
    }
}
