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
            return CreatedAtAction(nameof(GetEmpresa), new { id = novaEmpresa.Id}, novaEmpresa);
        }


    }
}