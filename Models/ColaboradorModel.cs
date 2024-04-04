using System.ComponentModel.DataAnnotations;
using Api_test.Enums;

namespace Api_test.Models
{
    public class ColaboradorModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Nome { get; set; }
        [Required]
        public string? Genero { get; set; }
        [Required]
        public int Idade { get; set; }
        [Required]
        public bool Ativo { get; set; }
        [Required]
        public DateTime DataNasc { get; set; }
        [Required]
        public string? Telefone { get; set; }
        [Required]
        public string? Email { get; set; }
        public DateTime DataDeCriacao { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime DataDeAlteracao { get; set; } = DateTime.Now.ToLocalTime();
        [Required]
        public Cargo Cargo { get; set; }
    }
}


