using Api_test.Models;
using Api_test.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_test.Services;
using Api_test.Validators;
using FluentValidation;

namespace Api_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IValidator<DocumentoDTO> _validator;

        public DocumentoController(ApplicationContext context, IValidator<DocumentoDTO> validator)
        {
            _context = context;
            _validator = validator;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumento(int id)
        {
            try
            {
                var documentoModel = await _context.Documentos.FindAsync(id);
                if (documentoModel == null)
                {
                    return NotFound(new ServiceResponse<DocumentoInfoDTO> { Mensagem = "Documento não encontrado.", Sucesso = false });
                }

                _context.Documentos.Remove(documentoModel);
                await _context.SaveChangesAsync();
                return Ok(new ServiceResponse<DocumentoInfoDTO> { Dados = new DocumentoService().ConvertModelParaInfoDTO(documentoModel), Mensagem = "Documento excluído com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResponse<DocumentoInfoDTO> { Mensagem = $"Ocorreu um erro ao excluir o documento: {ex.Message}", Sucesso = false });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetListaDocumento()
        {
            try
            {
                var documentos = await _context.Documentos.ToListAsync();
                var documentosInfoDTO = documentos.Select(c => new DocumentoService().ConvertModelParaInfoDTO(c));
                return Ok(new ServiceResponse<IEnumerable<DocumentoInfoDTO>> { Dados = documentosInfoDTO, Mensagem = "Informacoes de documentos recuperadas com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResponse<IEnumerable<DocumentoInfoDTO>> { Mensagem = $"Ocorreu um erro ao recuperar os documentos: {ex.Message}", Sucesso = false });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumento(int id)
        {
            try
            {
                var documentoModel = await _context.Documentos.FindAsync(id);

                if (documentoModel == null)
                {
                    return NotFound(new ServiceResponse<DocumentoDTO> { Mensagem = "Documento não encontrado.", Sucesso = false });
                }

                var documentoDTO = new DocumentoService().ConverterParaDTO(documentoModel);

                Response.ContentType = "application/octet-stream";

                string nomeArquivo = $"Documento_{id}.pdf";
                Response.Headers.Append("Content-Disposition", $"attachment; filename=\"{nomeArquivo}\"");

                return File(documentoModel.Arquivo, Response.ContentType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResponse<DocumentoDTO> { Mensagem = $"Ocorreu um erro ao recuperar o documento: {ex.Message}", Sucesso = false });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<DocumentoDTO>>> PostDocumento(DocumentoDTO documentoDTO)
        {
            try
            {
                /*
                var validationResult = _validator.Validate(documentoDTO);
                if (!validationResult.IsValid)
                {
                    var erros = validationResult.Errors.Select(e => e.ErrorMessage);
                    return BadRequest(new ServiceResponse<DocumentoInfoDTO> { Mensagem = erros.First(), Sucesso = false });
                }
                */

                var documentoModel = new DocumentoService().ConverterParaModel(documentoDTO);
                _context.Documentos.Add(documentoModel);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetDocumento), new { id = documentoModel.Id }, new ServiceResponse<DocumentoInfoDTO> { Dados = new DocumentoService().ConvertModelParaInfoDTO(documentoModel), Mensagem = "Documento criado com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResponse<DocumentoInfoDTO> { Mensagem = $"Ocorreu um erro ao criar o documento: {ex.Message}", Sucesso = false });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumento(long id, DocumentoDTO documentoDTO)
        {
            /*
            var validationResult = _validator.Validate(documentoDTO);
            if (!validationResult.IsValid)
            {
                var erros = validationResult.Errors.Select(e => e.ErrorMessage);
                return BadRequest(new ServiceResponse<DocumentoInfoDTO> { Mensagem = erros.First(), Sucesso = false });
            }
            */
            try
            {
                if (id != documentoDTO.Id)
                {
                    return BadRequest(new ServiceResponse<DocumentoModel> { Mensagem = "ID do documento na URL não corresponde ao ID do documento no corpo da solicitação.", Sucesso = false });
                }
                var documentoModel = new DocumentoService().ConverterParaModel(documentoDTO);
                _context.Entry(documentoModel).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return Ok(new ServiceResponse<DocumentoInfoDTO> { Dados = new DocumentoService().ConvertModelParaInfoDTO(documentoModel), Mensagem = "Documento atualizado com sucesso." });
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

        private bool DocumentoModelExist(long id)
        {
            return _context.Documentos.Any(e => e.Id == id);
        }
    }
}
