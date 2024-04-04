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

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<DocumentoModel>>> GetDocumento(int id)
        {
            try
            {
                var documentoModel = await _context.Documentos.FindAsync(id);

                if (documentoModel == null)
                {
                    return NotFound(new ServiceResponse<DocumentoDTO> { Mensagem = "Documento não encontrado.", Sucesso = false });
                }

                var documentoDTO = new DocumentoService().ConverterParaDTO(documentoModel);

                return Ok(new ServiceResponse<DocumentoDTO> { Dados = documentoDTO, Mensagem = "Documento recuperado com sucesso." });
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
                    var errosMensagem = string.Join(", ", erros);
                    return BadRequest(new ServiceResponse<DocumentoInfoDTO> { Mensagem = validationResult.Errors.First().ErrorMessage, Sucesso = false });
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
            var validationResult = _validator.Validate(documentoDTO);
            if (!validationResult.IsValid)
            {
                var erros = validationResult.Errors.Select(e => e.ErrorMessage);
                var errosMensagem = string.Join(", ", erros);
                return BadRequest(new ServiceResponse<DocumentoInfoDTO> { Mensagem = validationResult.Errors.First().ErrorMessage, Sucesso = false });
            }
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
