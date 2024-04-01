using System;
using System.ComponentModel.DataAnnotations;

namespace Api_test.Models
{
    public class DocumentoModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public byte[] Arquivo { get; set; }
    }

}