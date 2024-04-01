using System;
using System.ComponentModel.DataAnnotations;
using Api_test.Enums;

namespace Api_test.Models
{
    public class DocumentoModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public TipoDocumento Tipo { get; set; }
    }

}