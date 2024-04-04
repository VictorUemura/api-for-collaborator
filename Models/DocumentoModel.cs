using System;
using System.ComponentModel.DataAnnotations;
using Api_test.Enums;

namespace Api_test.Models
{
    public class DocumentoModel
    {
        [Key]
        public int Id { get; set; }
        public TipoDocumento Tipo { get; set; }
        public int IdColaborador { get; set; }
        public byte[] Arquivo { get; set; }
    }

}