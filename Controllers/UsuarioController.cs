using Api_test.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
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
}
