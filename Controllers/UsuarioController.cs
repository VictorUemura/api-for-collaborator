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
    DeleteUsuario
}
