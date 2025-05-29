using Microsoft.AspNetCore.Mvc;
using TesteHelena.Models;
using TesteHelena.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TesteHelena.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Rota base
    public class EmpresasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmpresasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet] // GET: api/empresas
        public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresas()
        {
            return Ok(await _context.Empresas.ToListAsync());
        }

        [HttpGet("{id}")] // GET: api/empresas/{id}
        public async Task<ActionResult<Empresa>> GetEmpresa(int id)
        {
            var empresa = await _context.Empresas.FirstOrDefaultAsync(e => e.Id == id);
            if (empresa == null)
            {
                return NotFound($"Nao existe uma empresa com esse ID {id}");
            }

            return Ok(empresa);
        }

        [HttpPost] // POST: api/empresas
        public async Task<ActionResult<Empresa>> PostEmpresa([FromBody] Empresa novaEmpresa)
        {
            if (await _context.Empresas.AnyAsync(e => e.Id == novaEmpresa.Id))
            {
                return BadRequest($"Ja existe uma empresa com o ID {novaEmpresa.Id}.");
            }
            _context.Empresas.Add(novaEmpresa);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmpresa), new { id = novaEmpresa.Id }, novaEmpresa);
        }

        [HttpPut("{id}")] // PUT: api/empresas/{id}
        public async Task<IActionResult> PutEmpresa(int id, [FromBody] Empresa empresaAtualizada)
        {
            if (id != empresaAtualizada.Id)
            {
                return BadRequest("O ID da rota nao corresponde ao ID da empresa.");
            }

            var empresaExistente = await _context.Empresas.FirstOrDefaultAsync(e => e.Id == id);
            if (empresaExistente == null)
            {
                return NotFound($"Empresa com ID {id} nao encontrada para atualizacao.");
            }

            _context.Entry(empresaExistente).CurrentValues.SetValues(empresaAtualizada);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")] // DELETE: api/empresas/{id}
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            var empresaParaRemover = await _context.Empresas.FirstOrDefaultAsync(e => e.Id == id);
            if (empresaParaRemover == null)
            {
                return NotFound($"Empresa com ID {id} nao foi encontrada para remocao.");
            }

            _context.Empresas.Remove(empresaParaRemover);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}