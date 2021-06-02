using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Core
{
    public class LogicException : ApplicationException
    {
        public List<ValidationResult> ValidationResult { get; set; }

        public LogicException(string message, List<ValidationResult> validationResult) : base(message)
        {
            ValidationResult = validationResult;
        }

        public LogicException(string message, Exception innerException, List<ValidationResult> validationResult)
            : base(message, innerException)
        {
            ValidationResult = validationResult;
        }
    }
}
