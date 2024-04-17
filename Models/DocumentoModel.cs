using System.ComponentModel.DataAnnotations;
using Api_test.Enums;

namespace Api_test.Models
{
    public class DocumentoModel
    {
        [Key]
        public long Id { get; set; }
        public TipoDocumento Tipo { get; set; }
        public long IdColaborador { get; set; }
        public byte[] Arquivo { get; set; }
    }

}