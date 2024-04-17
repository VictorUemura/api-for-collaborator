using FluentValidation;
using Api_test.Models;
using Api_test.Enums;
using Api_test.Models.Request;

namespace Api_test.Validators
{
    public class DocumentoValidatorModel : AbstractValidator<DocumentoCadastroRequest>
    {
        public DocumentoValidatorModel()
        {
            RuleFor(x => x.Tipo)
            .NotNull().WithMessage("O tipo do documento é obrigatório.")
                .NotEmpty().WithMessage("O tipo do documento é obrigatório.")
                .Must(x => Enum.GetNames(typeof(TipoDocumento)).Contains(x)).WithMessage("Tipo de documento inválido.");

            RuleFor(x => x.IdColaborador)
            .NotNull().WithMessage("O ID do colaborador é obrigatório.")
                .NotEmpty().WithMessage("O ID do colaborador é obrigatório.");

            RuleFor(x => x.Arquivo)
            .NotEmpty().WithMessage("O arquivo do documento é obrigatório.")
                .NotNull().WithMessage("O arquivo do documento é obrigatório.")
                .Must(x => x.FileName.EndsWith(".pdf")).WithMessage("O arquivo deve ser do tipo PDF.");
        }
    }
}
