using System.ComponentModel.DataAnnotations;

namespace Api_test.Models.Request
{
    public class DocumentoCadastroRequest
    {
        [Key]
        public int Id { get; set; }
        public string? Tipo { get; set; }
        public int IdColaborador { get; set; }
        public IFormFile? Arquivo { get; set; }
    }
}