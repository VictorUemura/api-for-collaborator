using System.ComponentModel.DataAnnotations;
using Api_test.Enums;

namespace Api_test.Models
{
    public class ColaboradorModel
    {
        [Key]
        public long Id { get; set; }
        public string? Nome { get; set; }
        public Genero Genero { get; set; }
        public int Idade { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataNasc { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public DateTime DataDeCriacao { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime DataDeAlteracao { get; set; } = DateTime.Now.ToLocalTime();
        public Cargo Cargo { get; set; }
    }
}


