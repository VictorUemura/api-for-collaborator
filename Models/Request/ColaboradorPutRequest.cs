namespace Api_test.Models.Request
{
    public class ColaboradorPutRequest
    {
        public long Id { get; set; }
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