using FluentValidation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = FluentValidation.ValidationException;

namespace Core.CrossCuttingConcerns.Validation
{//aspect bunu çağırsın
    public static class ValidationTool
    {                               //doğrulamanın olduğu class --///doğrulnacak kılas
        public static void Validate(IValidator validator,object entity)//entity dto hepsini yazabilirim
        {
            var context = new ValidationContext<object>(entity);
           
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
