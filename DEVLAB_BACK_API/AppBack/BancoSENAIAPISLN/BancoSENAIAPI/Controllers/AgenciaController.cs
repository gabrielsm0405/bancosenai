using BancoSENAIAPI.Data; // Change
using BancoSENAIAPI.Models; // Change
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Change

namespace BancoSENAIAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AgenciaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AgenciaController(AppDbContext context) // Change
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodas()
        {
            var agencias = await _context.Agencias.ToListAsync(); // Change
            return Ok(agencias);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] Agencia novaAgencia)
        {
            if (await _context.Agencias.AnyAsync(a => a.NumeroAgencia == novaAgencia.NumeroAgencia)) // Change
                return BadRequest(new { message = "Este número de agência já existe." });

            _context.Agencias.Add(novaAgencia); // Change
            await _context.SaveChangesAsync(); // Change
            // Retorna Status 201 Created conforme boas práticas REST [6, 8]
            return Created("", novaAgencia);
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> ConsultarPorCodigo(int codigo)
        {
            var agencia = await _context.Agencias.FirstOrDefaultAsync(a => a.NumeroAgencia == codigo); // Change

            if (agencia == null)
                return NotFound(new { message = "Agência não encontrada." }); // Status 404 [6, 7]

            return Ok(agencia); // Status 200 OK [6, 7]
        }

        [HttpPut("{codigo}")]
        public async Task<IActionResult> Alterar(int codigo, [FromBody] Agencia agenciaAtualizada)
        {
            var agenciaExistente = await _context.Agencias.FirstOrDefaultAsync(a => a.NumeroAgencia == codigo); // Change

            if (agenciaExistente == null) return NotFound();

            agenciaExistente.Cidade = agenciaAtualizada.Cidade;
            agenciaExistente.SiglaEstado = agenciaAtualizada.SiglaEstado;

            await _context.SaveChangesAsync(); // Change
            // Retorna Status 204 No Content para atualizações bem-sucedidas [6, 9]
            return NoContent();
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> Excluir(int codigo)
        {
            var agencia = await _context.Agencias.FirstOrDefaultAsync(a => a.NumeroAgencia == codigo); // Change

            if (agencia == null) return NotFound();

            _context.Agencias.Remove(agencia); // Change
            await _context.SaveChangesAsync(); // Change
            return Ok(new { message = "Agência excluída com sucesso." }); // Status 200 [6]
        }
    }
}
