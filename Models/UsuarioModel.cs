using System.ComponentModel.DataAnnotations;
using Api_test.Enums;

namespace Api_test.Models
{
    public class UsuarioModel
    {
        [Key]
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Genero { get; set; }
        public int Idade { get; set; }
        public required string Endereco { get; set; }
        public CargoEnum Cargo { get; set; }
        public AreaEnum Area { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataDeCriacao { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime DataDeAlteracao { get; set; } = DateTime.Now.ToLocalTime();
    }
}


