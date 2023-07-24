using FluentValidation;
using YouYou.Business.Utils;

namespace YouYou.Business.Models.Validations
{
    public class PhysicalPersonValidation : AbstractValidator<PhysicalPerson>
    {
        public static string NomeNaoVazioMsg => "O campo Nome precisa ser fornecido";
        public static string NomeMaxLengthMsg => "O campo Nome só pode ter no máximo 256 caracteres";
        public static string CPFInvalidoMsg => "O CPF fornecido é inválido.";

        public PhysicalPersonValidation()
        {
            RuleFor(f => f.Name)
                .NotEmpty().WithMessage(NomeNaoVazioMsg)
                .MaximumLength(256)
                .WithMessage(NomeMaxLengthMsg);

            When(f => f.CPF != null && f.CPF != "", () =>
            {
                RuleFor(f => UsefulFunctions.ValidateCpf(f.CPF)).Equal(true)
                .WithMessage(CPFInvalidoMsg);
            });
        }
    }
}
