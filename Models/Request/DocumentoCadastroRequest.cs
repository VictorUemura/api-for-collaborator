namespace Api_test.Models.Request
{
    public class DocumentoCadastroRequest
    {
        public string? Tipo { get; set; }
        public long IdColaborador { get; set; }
        public IFormFile? Arquivo { get; set; }
    }
}