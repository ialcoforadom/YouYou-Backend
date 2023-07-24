using FluentValidation;

namespace YouYou.Business.Models.Validations
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(f => f.CEP)
                .MaximumLength(8)
                .WithMessage("O campo {PropertyName} só pode ter no máximo {MaxLength} caracteres");

            RuleFor(f => f.Street)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .MaximumLength(256)
                .WithMessage("O campo {PropertyName} só pode ter no máximo {MaxLength} caracteres");

            RuleFor(f => f.Number)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .MaximumLength(100)
                .WithMessage("O campo {PropertyName} só pode ter no máximo {MaxLength} caracteres");

            RuleFor(f => f.Neighborhood)
                .MaximumLength(256)
                .WithMessage("O campo {PropertyName} só pode ter no máximo {MaxLength} caracteres");

            RuleFor(f => f.Complement)
                .MaximumLength(256)
                .WithMessage("O campo {PropertyName} só pode ter no máximo {MaxLength} caracteres");
        }
    }
}
