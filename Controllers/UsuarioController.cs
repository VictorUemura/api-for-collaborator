using Api_test.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioContext _context;
        public UsuarioController(UsuarioContext context)
        {
            _context = context;
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUsuarioModel(int id)
        {
            var usuario = await _context.UsuarioItems.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.UsuarioItems.Remove(usuario);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioModel>> GetUsuarioModel(int id)
        {
            var usuarioModel = await _context.UsuarioItems.FindAsync(id);

            if (usuarioModel == null)
            {
                return NotFound();
            }

            return usuarioModel;
        }
        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> PostUsuarioModel(UsuarioModel UsuarioModel)
        {
            _context.UsuarioItems.Add(UsuarioModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuarioModel), new { id = UsuarioModel.Id }, UsuarioModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioModel(long id, UsuarioModel usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioModelExist(id))
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

        private bool UsuarioModelExist(long id)
        {
            return _context.UsuarioItems.Any(e => e.Id == id);
        }
    }

}