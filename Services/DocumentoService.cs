using Api_test.Enums;
using Api_test.Models;

namespace Api_test.Services
{
    public class DocumentoService
    {
        public DocumentoModel ConverterParaModel(DocumentoDTO dto)
        {
            return new DocumentoModel
            {
                Id = dto.Id,
                Tipo = ConverteTipoDocumento(dto.Tipo),
                IdColaborador = dto.IdColaborador,
                Arquivo = dto.Arquivo
            };
        }

        private TipoDocumento ConverteTipoDocumento(string tipo)
        {
            switch (tipo)
            {
                case "Estudante":
                    return TipoDocumento.Estudante;
                case "Corporativo":
                    return TipoDocumento.Pessoal;
                default:
                    throw new ArgumentException("Tipo de documento inválido.");
            }
        }
    }
}
