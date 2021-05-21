using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Entities
{
    public class Folder
    {
        
        public int Id { get; set; }
        
        [Required]
        [StringLength(maximumLength:255,ErrorMessage ="El campo no puede ser superior a 255 caracteres.")]
        public string Name { get; set; }       
        public bool Enabled { get; set; }
        public List<ToDo> Todos { get; set; }

    }
}
