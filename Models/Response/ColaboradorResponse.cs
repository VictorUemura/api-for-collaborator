using System.ComponentModel.DataAnnotations;

namespace Api_test.Models.Response
{
    public class ColaboradorResponse
    {
        [Key]
        public long Id { get; set; }
        public string? Nome { get; set; }
        public string? Genero { get; set; }
        public int Idade { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataNasc { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? Cargo { get; set; }
        public DateTime DataDeCriacao { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime DataDeAlteracao { get; set; } = DateTime.Now.ToLocalTime();
    }
}


