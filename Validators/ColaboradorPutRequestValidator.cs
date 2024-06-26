using FluentValidation;
using Api_test.Enums;
using Api_test.Models.Request;

namespace Api_test.Validators
{
    public class ColaboradorPutRequestValidator : AbstractValidator<ColaboradorPutRequest>
    {
        public ColaboradorPutRequestValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome do colaborador é obrigatório.");

            RuleFor(x => x.Genero)
                .NotEmpty().WithMessage("O gênero do colaborador é obrigatório.")
                .Must(x => Enum.GetNames(typeof(Genero)).Contains(x)).WithMessage("Genero inválido.");

            RuleFor(x => x.Idade)
                .NotEmpty().WithMessage("A idade do colaborador é obrigatória.")
                .GreaterThanOrEqualTo(18).WithMessage("A idade do colaborador deve ser igual ou maior que 18 anos.");

            RuleFor(x => x.DataNasc)
                .NotEmpty().WithMessage("A data de nascimento do colaborador é obrigatória.")
                .Must((colaborador, dataNasc) => TerDataNascimentoValida(dataNasc, colaborador)).WithMessage("A data de nascimento do colaborador é inválida ou inconsistente com a idade fornecida.");

            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage("O número de telefone do colaborador é obrigatório.");

            RuleFor(x => x.Cargo)
                .NotEmpty().WithMessage("O cargo do colaborador é obrigatório.")
                .NotNull().WithMessage("O cargo do colaborador é obrigatório.")
                .Must(x => Enum.GetNames(typeof(Cargo)).Contains(x)).WithMessage("Cargo inválido.");
        }

        private bool TerDataNascimentoValida(DateTime dataNasc, ColaboradorPutRequest colaborador)
        {
            int idade = DateTime.Today.Year - dataNasc.Year;
            if (dataNasc.Date > DateTime.Today.AddYears(-idade)) idade--;
            return idade == colaborador.Idade;
        }
    }
}
