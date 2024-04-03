using Api_test.Repositories;
using Api_test.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace Api_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColaboradorController : ControllerBase
    {
        private readonly ColaboradorContext _context;
        private readonly IValidator<ColaboradorModel> _validator;

        public ColaboradorController(ColaboradorContext context, IValidator<ColaboradorModel> validator)
        {
            _context = context;
            _validator = validator;
        }

        private bool ColaboradorModelExist(long id)
        {
            return _context.ColaboradorItems.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ColaboradorModel>>> GetColaboradorModel()
        {
            try
            {
                var colaboradores = await _context.ColaboradorItems.ToListAsync();
                return Ok(new ServiceResponse<IEnumerable<ColaboradorModel>> { Dados = colaboradores, Mensagem = "Colaboradores recuperados com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResponse<IEnumerable<ColaboradorModel>> { Mensagem = $"Ocorreu um erro ao recuperar os colaboradores: {ex.Message}", Sucesso = false });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColaboradorModel(int id)
        {
            try
            {
                var colaboradorModel = await _context.ColaboradorItems.FindAsync(id);
                if (colaboradorModel == null)
                {
                    return NotFound(new ServiceResponse<object> { Mensagem = "Colaborador não encontrado.", Sucesso = false });
                }

                _context.ColaboradorItems.Remove(colaboradorModel);
                await _context.SaveChangesAsync();
                return Ok(new ServiceResponse<object> { Mensagem = "Colaborador excluído com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResponse<object> { Mensagem = $"Ocorreu um erro ao excluir o colaborador: {ex.Message}", Sucesso = false });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ColaboradorModel>> GetColaboradorModel(int id)
        {
            try
            {
                var colaboradorModel = await _context.ColaboradorItems.FindAsync(id);

                if (colaboradorModel == null)
                {
                    return NotFound(new ServiceResponse<ColaboradorModel> { Mensagem = "Colaborador não encontrado.", Sucesso = false });
                }

                return Ok(new ServiceResponse<ColaboradorModel> { Dados = colaboradorModel, Mensagem = "Colaborador recuperado com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResponse<ColaboradorModel> { Mensagem = $"Ocorreu um erro ao recuperar o colaborador: {ex.Message}", Sucesso = false });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ColaboradorModel>> PostColaboradorModel(ColaboradorModel colaboradorModel)
        {
            var validationResult = _validator.Validate(colaboradorModel);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ServiceResponse<ColaboradorModel> { Mensagem = "Erro de validação", Sucesso = false });
            }

            try
            {
                _context.ColaboradorItems.Add(colaboradorModel);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetColaboradorModel), new { id = colaboradorModel.Id }, new ServiceResponse<ColaboradorModel> { Dados = colaboradorModel, Mensagem = "Colaborador criado com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResponse<ColaboradorModel> { Mensagem = $"Ocorreu um erro ao criar o colaborador: {ex.Message}", Sucesso = false });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutColaboradorModel(long id, ColaboradorModel colaboradorModel)
        {
            try
            {
                if (id != colaboradorModel.Id)
                {
                    return BadRequest(new ServiceResponse<ColaboradorModel> { Mensagem = "ID do colaborador na URL não corresponde ao ID do colaborador no corpo da solicitação.", Sucesso = false });
                }

                _context.Entry(colaboradorModel).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColaboradorModelExist((int)id))
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchColaboradorModel(int id, JsonPatchDocument<ColaboradorModel> patchDocument)
        {
            try
            {
                if (patchDocument == null)
                {
                    return BadRequest(new ServiceResponse<ColaboradorModel> { Mensagem = "O documento de patch não pode ser nulo.", Sucesso = false });
                }

                var colaboradorModel = await _context.ColaboradorItems.FindAsync(id);

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
            return _context.ColaboradorItems.Any(e => e.Id == id);
        }
    }
}
