using FluentValidation;
using Api_test.Models;

public class ColaboradorValidatorModel : AbstractValidator<ColaboradorModel>
{
    public ColaboradorValidatorModel()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome do colaborador é obrigatório.");

        RuleFor(x => x.Genero)
            .NotEmpty().WithMessage("O gênero do colaborador é obrigatório.");

        RuleFor(x => x.Idade)
            .GreaterThanOrEqualTo(18).WithMessage("A idade do colaborador deve ser igual ou maior que 18 anos.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O email do colaborador é obrigatório.")
            .EmailAddress().WithMessage("O email do colaborador deve ser um endereço válido.");

        RuleFor(x => x.DataNasc)
            .NotEmpty().WithMessage("A data de nascimento do colaborador é obrigatória.");

        RuleFor(x => x.Telefone)
            .NotEmpty().WithMessage("O número de telefone do colaborador é obrigatório.");

        RuleFor(x => x.Cargo)
            .NotNull().WithMessage("O cargo do colaborador é obrigatório.");
    }
}