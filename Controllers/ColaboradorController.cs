using Api_test.Models;
using Api_test.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColaboradorController : ControllerBase
    {
        private readonly ColaboradorContext _context;
        public ColaboradorController(ColaboradorContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ColaboradorModel>>> GetColaboradorModel()
        {
            return await _context.ColaboradorItems.ToListAsync();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteColaboradorModel(int id)
        {
            var colaboradorModel = await _context.ColaboradorItems.FindAsync(id);
            if (colaboradorModel == null)
            {
                return NotFound();
            }

            _context.ColaboradorItems.Remove(colaboradorModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ColaboradorModel>> GetColaboradorModel(int id)
        {
            var colaboradorModel = await _context.ColaboradorItems.FindAsync(id);

            if (colaboradorModel == null)
            {
                return NotFound();
            }

            return colaboradorModel;
        }
        [HttpPost]
        public async Task<ActionResult<ColaboradorModel>> PostColaboradorModel(ColaboradorModel ColaboradorModel)
        {
            _context.ColaboradorItems.Add(ColaboradorModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetColaboradorModel), new { id = ColaboradorModel.id }, ColaboradorModel);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColaboradorModel(long id, ColaboradorModel colaboradorModel)
        {
            if (id != colaboradorModel.id)
            {
                return BadRequest();
            }

            _context.Entry(colaboradorModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColaboradorModelExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        private bool ColaboradorModelExist(long id)
        {
            return _context.ColaboradorItems.Any(e => e.id == id);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchColaboradorModel(int id, JsonPatchDocument<ColaboradorModel> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("O documento de patch não pode ser nulo.");
            }

            var colaboradorModel = await _context.ColaboradorItems.FindAsync(id);

            if (colaboradorModel == null)
            {
                return NotFound();
            }

            patchDocument.ApplyTo(colaboradorModel, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(colaboradorModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColaboradorModelExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

    }

}