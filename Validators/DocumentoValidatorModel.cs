using FluentValidation;
using Api_test.Models;
using Api_test.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Api_test.Validators
{
    public class DocumentoValidatorModel : AbstractValidator<DocumentoDTO>
    {
        public DocumentoValidatorModel()
        {
            RuleFor(x => x.Tipo)
                .NotEmpty().WithMessage("O tipo do documento é obrigatório.")
                .Must(BeValidTipoDocumento).WithMessage("Tipo de documento inválido.");

            RuleFor(x => x.IdColaborador)
                .NotEmpty().WithMessage("O ID do colaborador é obrigatório.");

            RuleFor(x => x.Arquivo)
                .NotNull().WithMessage("O arquivo do documento é obrigatório.")
                .Must(BePdfFile).WithMessage("O arquivo deve ser do tipo PDF.");
        }

        private bool BeValidTipoDocumento(string tipo)
        {
            return Enum.TryParse<TipoDocumento>(tipo, out _);
        }

        private bool BePdfFile(IFormFile arquivo)
        {
            return arquivo != null && arquivo.ContentType == "application/pdf";
        }
    }
}
