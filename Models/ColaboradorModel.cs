using System.ComponentModel.DataAnnotations;

namespace Api_test.Models
{
    public class ColaboradorModel
    {
        [Key]
        public int id { get; set; }
        public required string? nome { get; set; }
        public required string? genero { get; set; }
        public int idade { get; set; }
        public bool ativo { get; set; }
        public DateTime dataNasc { get; set; }
        public string? telefone { get; set; }
        public string? email { get; set; }
        public DateTime dataDeCriacao { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime dataDeAlteracao { get; set; } = DateTime.Now.ToLocalTime();
        public required int cargoId { get; set; }
    }
}


