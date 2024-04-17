namespace Api_test.Models.Request
{
    public class DocumentoPutRequest
    {
        public long Id { get; set; }
        public string? Tipo { get; set; }
        public long IdColaborador { get; set; }
        public IFormFile? Arquivo { get; set; }
    }
}