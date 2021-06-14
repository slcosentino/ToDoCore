using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Core;

namespace ToDo.Service
{
    public abstract class BaseService
    {
        public virtual bool validateEntity<T>(T entity, out List<ValidationResult> results)
        {           
            results = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(entity, null, null);
            return Validator.TryValidateObject(entity, context, results, validateAllProperties: true);
        }
        protected void validateFields<T>(T entity)
        {
            List<ValidationResult> validationResult;
            string entityName = typeof(T).Name;
            if (!validateEntity<T>(entity, out validationResult))
                throw new LogicException($"{entityName} validation.", validationResult);
        }

        protected void validateEntityExist<T>(T entity)
        {
            if (entity == null)
            {
                string entityName = typeof(T).Name;                
                var e = new ValidationResult("We don't found a valid Id.", new string[] { "Id" });
                List<ValidationResult> validationResult = new List<ValidationResult> { e };
                throw new LogicException($"{entityName} not found.", validationResult);
            }
        }
    }
}
