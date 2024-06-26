using Api_test.Enums;
using Api_test.Models;
using Api_test.Models.Request;
using Api_test.Models.Response;

namespace Api_test.Converters
{
    public class DocumentoConverter
    {
        public DocumentoModel ConverterParaModel(DocumentoCadastroRequest dto)
        {
            return new DocumentoModel
            {
                Tipo = Enum.Parse<TipoDocumento>(dto.Tipo),
                IdColaborador = dto.IdColaborador,
                Arquivo = ConverteIFileParaByte(dto.Arquivo)
            };
        }

        public DocumentoCadastroRequest ConverterParaDTO(DocumentoModel model)
        {
            return new DocumentoCadastroRequest
            {
                Tipo = model.Tipo.ToString(),
                IdColaborador = model.IdColaborador,
                Arquivo = ConverteByteParaIFile(model.Arquivo, model.Id.ToString() + "_" + model.Tipo.ToString() + ".pdf")
            };
        }

        public byte[] ConverteIFileParaByte(IFormFile file)
        {
            if (file == null)
            {
                return null;
            }
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);

                return memoryStream.ToArray();

            }
        }
        public IFormFile ConverteByteParaIFile(byte[] bytes, string fileName)
        {
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                return new FormFile(memoryStream, 0, bytes.Length, null, fileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/octet-stream" // Defina o tipo de conteúdo conforme necessário
                };
            }
        }

        public DocumentoInfoResponse ConvertModelParaInfoDTO(DocumentoModel model)
        {
            return new DocumentoInfoResponse
            {
                Id = model.Id,
                Tipo = model.Tipo.ToString(),
                IdColaborador = model.IdColaborador
            };
        }

        public DocumentoModel ConvertPutParaModel(DocumentoPutRequest dto)
        {
            return new DocumentoModel
            {
                Id = dto.Id,
                Tipo = Enum.Parse<TipoDocumento>(dto.Tipo),
                IdColaborador = dto.IdColaborador,
                Arquivo = ConverteIFileParaByte(dto.Arquivo)
            };
        }
    }
}
