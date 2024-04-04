using System;
using System.ComponentModel.DataAnnotations;
using Api_test.Enums;

namespace Api_test.Models
{
    public class DocumentoInfoDTO
    {
        [Key]
        public int Id { get; set; }
        public string? Tipo { get; set; }
        public int IdColaborador { get; set; }
    
    }
}