using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.DTOs
{
    public class ToDoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FolderDTO Folder { get; set; }        
    }
}
