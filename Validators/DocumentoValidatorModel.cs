using FluentValidation;
using Api_test.Models.Request;
using System;
using System.IO;
using System.Linq;

namespace Api_test.Validators
{
    public class DocumentoCadastroRequestValidator : AbstractValidator<DocumentoCadastroRequest>
    {
        public DocumentoCadastroRequestValidator()
        {
            RuleFor(x => x.Tipo)
                .NotEmpty().WithMessage("O tipo do documento é obrigatório.");

            RuleFor(x => x.IdColaborador)
                .NotEmpty().WithMessage("O ID do colaborador é obrigatório.")
                .GreaterThan(0).WithMessage("O ID do colaborador deve ser maior que zero.");

            RuleFor(x => x.Arquivo)
                .NotNull().WithMessage("O arquivo do documento é obrigatório.")
                .Must(x => x is not null && x.ContentType.Equals("application/pdf")).WithMessage("O arquivo do documento deve ser no formato PDF.");
        }
    }
}
