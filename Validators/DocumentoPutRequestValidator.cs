using FluentValidation;
using Api_test.Models.Request;

namespace Api_test.Validators
{
    public class DocumentoPutRequestValidator : AbstractValidator<DocumentoPutRequest>
    {
        public DocumentoPutRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O ID do documento é obrigatório.")
                .GreaterThan(0).WithMessage("O ID do documento deve ser maior que zero.");

            RuleFor(x => x.Tipo)
                .NotEmpty().WithMessage("O tipo do documento é obrigatório.");

            RuleFor(x => x.IdColaborador)
                .NotEmpty().WithMessage("O ID do colaborador é obrigatório.")
                .GreaterThan(0).WithMessage("O ID do colaborador deve ser maior que zero.");

            RuleFor(x => x.Arquivo)
                .NotNull().WithMessage("O arquivo do documento é obrigatório.");
        }
    }
}
