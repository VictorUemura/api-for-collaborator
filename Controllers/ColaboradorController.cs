using Api_test.Repositories;
using Api_test.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Api_test.Converters;
using Api_test.Models.Response;
using Api_test.Models.Request;

namespace Api_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColaboradorController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IValidator<ColaboradorCadastroRequest> _validator;
        private readonly IValidator<ColaboradorPutRequest> _validatorPut;

        public ColaboradorController(ApplicationContext context, IValidator<ColaboradorCadastroRequest> validator, IValidator<ColaboradorPutRequest> validatorPut)
        {
            _context = context;
            _validator = validator;
            _validatorPut = validatorPut;
        }

        private bool ColaboradorModelExist(long id)
        {
            return _context.Colaboradores.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetColaborador()
        {
            try
            {
                var colaboradores = await _context.Colaboradores.ToListAsync();
                var colaboradoresDTO = colaboradores.Select(c => new ColaboradorConverter().ConverterParaDTO(c));
                return Ok(new ServiceResponse<IEnumerable<ColaboradorResponse>> { Dados = colaboradoresDTO, Mensagem = "Colaboradores recuperados com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new ServiceResponse<IEnumerable<ColaboradorResponse>> { Dados = null, Mensagem = $"Ocorreu um erro ao recuperar os colaboradores: {ex.Message}", Sucesso = false });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColaborador(int id)
        {
            try
            {
                var colaboradorModel = await _context.Colaboradores.FindAsync(id);
                if (colaboradorModel == null)
                {
                    return NotFound(new ServiceResponse<object> { Mensagem = "Colaborador não encontrado.", Sucesso = false });
                }

                _context.Colaboradores.Remove(colaboradorModel);
                await _context.SaveChangesAsync();
                return Ok(new ServiceResponse<object> { Mensagem = "Colaborador excluído com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new ServiceResponse<object> { Mensagem = $"Ocorreu um erro ao excluir o colaborador: {ex.Message}", Sucesso = false });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetColaborador(int id)
        {
            try
            {
                var colaboradorModel = await _context.Colaboradores.FindAsync(id);

                if (colaboradorModel == null)
                {
                    return NotFound(new ServiceResponse<ColaboradorResponse> { Mensagem = "Colaborador não encontrado.", Sucesso = false });
                }

                var colaboradorDTO = new ColaboradorConverter().ConverterParaDTO(colaboradorModel);
                return Ok(new ServiceResponse<ColaboradorResponse> { Dados = colaboradorDTO, Mensagem = "Colaborador recuperado com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new ServiceResponse<ColaboradorModel> { Mensagem = $"Ocorreu um erro ao recuperar o colaborador: {ex.Message}", Sucesso = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostColaborador(ServiceRequest<ColaboradorCadastroRequest> req)
        {

            try
            {
                var colab = req.Dados;
                var validationResult = _validator.Validate(colab);
                if (!validationResult.IsValid)
                {
                    var erros = validationResult.Errors.Select(e => e.ErrorMessage);
                    return BadRequest(new ServiceResponse<ColaboradorResponse> { Mensagem = "Erro de validação: " + erros.First(), Sucesso = false });
                }
                var colaboradorModel = new ColaboradorConverter().ConverterCadastroParaModel(colab);
                _context.Colaboradores.Add(colaboradorModel);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetColaborador), new { id = colaboradorModel.Id }, new ServiceResponse<ColaboradorModel> { Dados = colaboradorModel, Mensagem = "Colaborador criado com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResponse<ColaboradorResponse> { Mensagem = $"Ocorreu um erro ao criar o colaborador: {ex.Message}", Sucesso = false });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutColaborador(long id, ServiceRequest<ColaboradorPutRequest> req)
        {
            var colab = req.Dados;
            var validationResult = _validatorPut.Validate(colab);
            try
            {
                if (id != colab.Id)
                {
                    return BadRequest(new ServiceResponse<ColaboradorResponse> { Mensagem = "ID do colaborador na URL não corresponde ao ID do colaborador no corpo da solicitação.", Sucesso = false });
                }
                if (!validationResult.IsValid)
                {
                    var erros = validationResult.Errors.Select(e => e.ErrorMessage);
                    return BadRequest(new ServiceResponse<ColaboradorResponse> { Mensagem = "Erro de validação: " + erros.First(), Sucesso = false });
                }

                var colaboradorAtual = await _context.Colaboradores.FindAsync(id);
                var colaboradorModel = new ColaboradorConverter().ConverterPutParaModel(colab);

                colaboradorModel.DataDeAlteracao = colaboradorAtual.DataDeAlteracao;
                _context.Entry(colaboradorModel).State = EntityState.Modified;

                var colaboradorResponse = new ColaboradorConverter().ConverterParaDTO(colaboradorModel);
                await _context.SaveChangesAsync();
                return Ok(new ServiceResponse<ColaboradorResponse> { Dados = colaboradorResponse, Mensagem = "Colaborador atualizado com sucesso." });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColaboradorModelExist((int)id))
                {
                    return NotFound(new ServiceResponse<ColaboradorResponse> { Mensagem = "Colaborador não encontrado.", Sucesso = false });
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResponse<ColaboradorResponse> { Mensagem = $"Ocorreu um erro ao atualizar o colaborador: {ex.Message}", Sucesso = false });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchColaborador(int id, JsonPatchDocument<ColaboradorModel> patchDocument)
        {
            try
            {
                if (patchDocument == null)
                {
                    return BadRequest(new ServiceResponse<ColaboradorModel> { Mensagem = "O documento de patch não pode ser nulo.", Sucesso = false });
                }

                var colaboradorModel = await _context.Colaboradores.FindAsync(id);

                if (colaboradorModel == null)
                {
                    return NotFound(new ServiceResponse<ColaboradorModel> { Mensagem = "Colaborador não encontrado.", Sucesso = false });
                }

                patchDocument.ApplyTo(colaboradorModel, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(new ServiceResponse<ColaboradorModel> { Mensagem = "As modificações aplicadas não são válidas.", Sucesso = false });
                }

                _context.Entry(colaboradorModel).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColaboradorModelExist(id))
                {
                    return NotFound(new ServiceResponse<ColaboradorModel> { Mensagem = "Colaborador não encontrado.", Sucesso = false });
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResponse<ColaboradorModel> { Mensagem = $"Ocorreu um erro ao atualizar o colaborador: {ex.Message}", Sucesso = false });
            }
        }

        private bool ColaboradorModelExist(int id)
        {
            return _context.Colaboradores.Any(e => e.Id == id);
        }
    }
}
