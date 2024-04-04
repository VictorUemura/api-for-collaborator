using Api_test.Models;
using Api_test.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_test.Services;

namespace Api_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public DocumentoController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<DocumentoModel>>>> GetDocumentoModel()
        {
            try
            {
                var documentos = await _context.Documentos.ToListAsync();
                return Ok(new ServiceResponse<IEnumerable<DocumentoModel>> { Dados = documentos, Mensagem = "Documentos recuperados com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResponse<IEnumerable<DocumentoModel>> { Mensagem = $"Ocorreu um erro ao recuperar os documentos: {ex.Message}", Sucesso = false });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentoModel(int id)
        {
            try
            {
                var documentoModel = await _context.Documentos.FindAsync(id);
                if (documentoModel == null)
                {
                    return NotFound(new ServiceResponse<object> { Mensagem = "Documento não encontrado.", Sucesso = false });
                }

                _context.Documentos.Remove(documentoModel);
                await _context.SaveChangesAsync();
                return Ok(new ServiceResponse<object> { Mensagem = "Documento excluído com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResponse<object> { Mensagem = $"Ocorreu um erro ao excluir o documento: {ex.Message}", Sucesso = false });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<DocumentoModel>>> GetDocumentoModel(int id)
        {
            try
            {
                var documentoModel = await _context.Documentos.FindAsync(id);

                if (documentoModel == null)
                {
                    return NotFound(new ServiceResponse<DocumentoModel> { Mensagem = "Documento não encontrado.", Sucesso = false });
                }

                return Ok(new ServiceResponse<DocumentoModel> { Dados = documentoModel, Mensagem = "Documento recuperado com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResponse<DocumentoModel> { Mensagem = $"Ocorreu um erro ao recuperar o documento: {ex.Message}", Sucesso = false });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<DocumentoModel>>> PostDocumentoModel(DocumentoModel documentoModel)
        {
            try
            {
                _context.Documentos.Add(documentoModel);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetDocumentoModel), new { id = documentoModel.Id }, new ServiceResponse<DocumentoModel> { Dados = documentoModel, Mensagem = "Documento criado com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResponse<DocumentoModel> { Mensagem = $"Ocorreu um erro ao criar o documento: {ex.Message}", Sucesso = false });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumentoModel(long id, DocumentoModel documentoModel)
        {
            try
            {
                if (id != documentoModel.Id)
                {
                    return BadRequest(new ServiceResponse<DocumentoModel> { Mensagem = "ID do documento na URL não corresponde ao ID do documento no corpo da solicitação.", Sucesso = false });
                }

                _context.Entry(documentoModel).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent();
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchDocumentoModel(int id, JsonPatchDocument<DocumentoModel> patchDocument)
        {
            try
            {
                if (patchDocument == null)
                {
                    return BadRequest(new ServiceResponse<DocumentoModel> { Mensagem = "O documento de patch não pode ser nulo.", Sucesso = false });
                }

                var documentoModel = await _context.Documentos.FindAsync(id);

                if (documentoModel == null)
                {
                    return NotFound(new ServiceResponse<DocumentoModel> { Mensagem = "Documento não encontrado.", Sucesso = false });
                }

                patchDocument.ApplyTo(documentoModel, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(new ServiceResponse<DocumentoModel> { Mensagem = "As modificações aplicadas não são válidas.", Sucesso = false });
                }

                _context.Entry(documentoModel).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent();
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
