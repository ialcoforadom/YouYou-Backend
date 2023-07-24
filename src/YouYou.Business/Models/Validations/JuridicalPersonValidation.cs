using FluentValidation;
using YouYou.Business.Utils;

namespace YouYou.Business.Models.Validations
{
    public class JuridicalPersonValidation : AbstractValidator<JuridicalPerson>
    {
        public JuridicalPersonValidation()
        {
            RuleFor(f => f.CompanyName)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .MaximumLength(256)
                .WithMessage("O campo {PropertyName} só pode ter no máximo {MaxLength} caracteres");

            RuleFor(f => f.TradingName)
                .MaximumLength(256)
                .WithMessage("O campo {PropertyName} só pode ter no máximo {MaxLength} caracteres");

            When(f => f.CNPJ != null && f.CNPJ != "", () =>
            {
                RuleFor(f => f.CNPJ.Length).Equal(14)
                .WithMessage("O campo {PropertyName} precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
            RuleFor(f => UsefulFunctions.ValidateCNPJ(f.CNPJ)).Equal(true)
                .WithMessage("O {PropertyName} fornecido é inválido.");
            });
        }
    }
}
