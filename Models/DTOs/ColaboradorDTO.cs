using System.ComponentModel.DataAnnotations;
using Api_test.Enums;

namespace Api_test.Models
{
    public class ColaboradorDTO
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Genero { get; set; }
        public int Idade { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataNasc { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? Cargo { get; set; }
    }
}


