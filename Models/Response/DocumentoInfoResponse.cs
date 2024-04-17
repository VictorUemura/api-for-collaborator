using System;
using System.ComponentModel.DataAnnotations;
using Api_test.Enums;

namespace Api_test.Models.Response
{
    public class DocumentoInfoResponse
    {
        [Key]
        public long Id { get; set; }
        public string? Tipo { get; set; }
        public int IdColaborador { get; set; }
    
    }
}