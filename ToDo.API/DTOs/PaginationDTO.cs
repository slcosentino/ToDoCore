using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.DTOs 
{ 
    public class PaginationDTO
    {
        private int page;
        public int Page 
        { 
                get { return page; }
                set { page = (value > 0) ? value : 1; }
        }

        public int RecordsPerPage { get; set; }

        public PaginationDTO()
        {
            Page = 1;
            RecordsPerPage = 10;
        }
    }
}
