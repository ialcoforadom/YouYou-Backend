using YouYou.Business.Utils;
using System.ComponentModel.DataAnnotations;

namespace YouYou.Api.Extensions
{
    public class CEPValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public CEPValidationAttribute() { }

        /// <summary>
        /// Validação server
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return true;

            return UsefulFunctions.ValidateCep(value.ToString());
        }
    }
}
