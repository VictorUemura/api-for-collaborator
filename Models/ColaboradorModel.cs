using System.ComponentModel.DataAnnotations;
using Api_test.Enums;

namespace Api_test.Models
{
    public class ColaboradorModel
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string? nome { get; set; }
        [Required]
        public string? genero { get; set; }
        public int idade { get; set; }
        public bool ativo { get; set; }
        public DateTime dataNasc { get; set; }
        public string? telefone { get; set; }
        public string? email { get; set; }
        public DateTime dataDeCriacao { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime dataDeAlteracao { get; set; } = DateTime.Now.ToLocalTime();
        [Required]
        public Cargo cargo { get; set; }
    }
}


