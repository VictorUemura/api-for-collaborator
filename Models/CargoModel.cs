namespace Api_test.Models
{
    public class Cargo
    {
        public required int id { get; set; }
        public required string? nome { get; set; }
        public required string? departamento { get; set; }
        public required double salarioBase { get; set; }
        public string? descricao { get; set; }
    }
}