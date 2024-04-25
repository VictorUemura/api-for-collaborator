using Api_test.Models;
using Api_test.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_test.Converters;
using FluentValidation;
using Api_test.Models.Response;
using Api_test.Models.Request;

namespace Api_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IValidator<DocumentoCadastroRequest> _validatorCadastro;
        private readonly IValidator<DocumentoPutRequest> _validatorPut;

        public DocumentoController(ApplicationContext context, IValidator<DocumentoCadastroRequest> validatorCadastro, IValidator<DocumentoPutRequest> validatorPut)
        {
            _context = context;
            _validatorCadastro = validatorCadastro;
            _validatorPut = validatorPut;
        }

        [HttpGet]
        public async Task<IActionResult> GetListaDocumento()
        {
            try
            {
                var documentos = await _context.Documentos.ToListAsync();
                var documentosInfoDTO = documentos.Select(c => new DocumentoConverter().ConvertModelParaInfoDTO(c));
                return Ok(new ServiceResponse<IEnumerable<DocumentoInfoResponse>> { Dados = documentosInfoDTO, Mensagem = "Informacoes de documentos recuperadas com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResponse<IEnumerable<DocumentoInfoResponse>> { Mensagem = $"Ocorreu um erro ao recuperar os documentos: {ex.Message}", Sucesso = false });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumento(long id)
        {
            try
            {
                var documentoModel = await _context.Documentos.FindAsync(id);

                if (documentoModel == null)
                {
                    return NotFound(new ServiceResponse<DocumentoCadastroRequest> { Mensagem = "Documento não encontrado.", Sucesso = false });
                }

                var documentoDTO = new DocumentoConverter().ConverterParaDTO(documentoModel);

                Response.ContentType = "application/octet-stream";

                string nomeArquivo = $"Documento_{id}.pdf";
                Response.Headers.Append("Content-Disposition", $"attachment; filename=\"{nomeArquivo}\"");

                return File(documentoModel.Arquivo, Response.ContentType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResponse<DocumentoCadastroRequest> { Mensagem = $"Ocorreu um erro ao recuperar o documento: {ex.Message}", Sucesso = false });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<DocumentoCadastroRequest>>> PostDocumento(DocumentoCadastroRequest documentoDTO)
        {
            try
            {
                var validationResult = _validatorCadastro.Validate(documentoDTO);
                if (!validationResult.IsValid)
                {
                    var erros = validationResult.Errors.Select(e => e.ErrorMessage);
                    return BadRequest(new ServiceResponse<DocumentoInfoResponse> { Mensagem = erros.First(), Sucesso = false });
                }

                var documentoModel = new DocumentoConverter().ConverterParaModel(documentoDTO);
                _context.Documentos.Add(documentoModel);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    var innerException = ex.InnerException;
                    while (innerException != null)
                    {
                        Console.WriteLine("Inner Exception Message: " + innerException.Message);
                        innerException = innerException.InnerException;
                    }
                    return StatusCode(500, new ServiceResponse<DocumentoInfoResponse> { Mensagem = "Ocorreu um erro ao criar o documento: " + ex.InnerException.Message, Sucesso = false });
                }

                return CreatedAtAction(nameof(GetDocumento), new { id = documentoModel.Id }, new ServiceResponse<DocumentoInfoResponse> { Dados = new DocumentoConverter().ConvertModelParaInfoDTO(documentoModel), Mensagem = "Documento criado com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResponse<DocumentoInfoResponse> { Mensagem = $"Ocorreu um erro ao criar o documento: {ex.Message}", Sucesso = false });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumento(long id, DocumentoPutRequest documentoDTO)
        {
            var validationResult = _validatorPut.Validate(documentoDTO);
            if (!validationResult.IsValid)
            {
                var erros = validationResult.Errors.Select(e => e.ErrorMessage);
                return BadRequest(new ServiceResponse<DocumentoInfoResponse> { Mensagem = erros.First(), Sucesso = false });
            }

            try
            {
                if (id != documentoDTO.Id)
                {
                    return BadRequest(new ServiceResponse<DocumentoModel> { Mensagem = "ID do documento na URL não corresponde ao ID do documento no corpo da solicitação.", Sucesso = false });
                }

                var documentoModel = new DocumentoConverter().ConvertPutParaModel(documentoDTO);
                _context.Entry(documentoModel).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return Ok(new ServiceResponse<DocumentoInfoResponse> { Dados = new DocumentoConverter().ConvertModelParaInfoDTO(documentoModel), Mensagem = "Documento atualizado com sucesso." });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentoModelExist(id))
                {
                    return NotFound(new ServiceResponse<DocumentoModel> { Mensagem = "Documento não encontrado.", Sucesso = false });
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResponse<DocumentoModel> { Mensagem = $"Ocorreu um erro ao atualizar o documento: {ex.Message}", Sucesso = false });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumento(long id)
        {
            try
            {
                var documentoModel = await _context.Documentos.FindAsync(id);
                if (documentoModel == null)
                {
                    return NotFound(new ServiceResponse<DocumentoInfoResponse> { Mensagem = "Documento não encontrado.", Sucesso = false });
                }

                _context.Documentos.Remove(documentoModel);
                await _context.SaveChangesAsync();
                return Ok(new ServiceResponse<DocumentoInfoResponse> { Dados = new DocumentoConverter().ConvertModelParaInfoDTO(documentoModel), Mensagem = "Documento excluído com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResponse<DocumentoInfoResponse> { Mensagem = $"Ocorreu um erro ao excluir o documento: {ex.Message}", Sucesso = false });
            }
        }

        private bool DocumentoModelExist(long id)
        {
            return _context.Documentos.Any(e => e.Id == id);
        }
    }
}
