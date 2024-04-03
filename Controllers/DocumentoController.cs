using Api_test.Models;
using Api_test.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<IEnumerable<DocumentoModel>>> GetDocumentoModel()
        {
            return await _context.Documentos.ToListAsync();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentoModel(int id)
        {
            var documentoModel = await _context.Documentos.FindAsync(id);
            if (documentoModel == null)
            {
                return NotFound();
            }

            _context.Documentos.Remove(documentoModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentoModel>> GetDocumentoModel(int id)
        {
            var documentoModel = await _context.Documentos.FindAsync(id);

            if (documentoModel == null)
            {
                return NotFound();
            }

            return documentoModel;
        }

        [HttpPost]
        public async Task<ActionResult<DocumentoModel>> PostDocumentoModel(DocumentoModel documentoModel)
        {
            _context.Documentos.Add(documentoModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDocumentoModel), new { id = documentoModel.Id }, documentoModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumentoModel(long id, DocumentoModel documentoModel)
        {
            if (id != documentoModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(documentoModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentoModelExist(id))
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

        private bool DocumentoModelExist(long id)
        {
            return _context.Documentos.Any(e => e.Id == id);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchDocumentoModel(int id, JsonPatchDocument<DocumentoModel> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("O documento de patch não pode ser nulo.");
            }

            var documentoModel = await _context.Documentos.FindAsync(id);

            if (documentoModel == null)
            {
                return NotFound();
            }

            patchDocument.ApplyTo(documentoModel, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(documentoModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentoModelExist(id))
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
