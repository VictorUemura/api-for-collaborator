using System;
using System.ComponentModel.DataAnnotations;

namespace Api_test.Models
{
    public class DocumentoModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public byte[] File { get; set; }
    }

}