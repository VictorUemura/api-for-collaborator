using FluentValidation;
using Api_test.Models;
using Api_test.Enums;

namespace Api_test.Validators
{
    public class ColaboradorValidatorModel : AbstractValidator<ColaboradorDTO>
    {
        public ColaboradorValidatorModel()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome do colaborador é obrigatório.");

            RuleFor(x => x.Genero)
                .NotEmpty().WithMessage("O gênero do colaborador é obrigatório.");

            RuleFor(x => x.Idade)
                .NotEmpty().WithMessage("A idade do colaborador é obrigatória.")
                .GreaterThanOrEqualTo(18).WithMessage("A idade do colaborador deve ser igual ou maior que 18 anos.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O email do colaborador é obrigatório.")
                .EmailAddress().WithMessage("O email do colaborador deve ser um endereço válido.");

            RuleFor(x => x.DataNasc)
                .NotEmpty().WithMessage("A data de nascimento do colaborador é obrigatória.");

            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage("O número de telefone do colaborador é obrigatório.");

            RuleFor(x => x.Cargo)
                .NotEmpty().WithMessage("O cargo do colaborador é obrigatório.")
                .NotNull().WithMessage("O cargo do colaborador é obrigatório.")
                .Must(x => Enum.GetNames(typeof(Cargo)).Contains(x)).WithMessage("Cargo inválido.");
        }
    }
}