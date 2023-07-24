using YouYou.Business.Utils;
using System.ComponentModel.DataAnnotations;

namespace YouYou.Api.Extensions
{
    /// <summary>
    /// Validação customizada para CNPJ
    /// </summary>
    public class CNPJValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public CNPJValidationAttribute() { }

        /// <summary>
        /// Validação server
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return true;

            return UsefulFunctions.ValidateCNPJ(value.ToString());
        }
    }
}
