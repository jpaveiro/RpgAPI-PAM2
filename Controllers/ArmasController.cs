using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgApi.Data;
using RpgApi.Models;
using System.Threading.Tasks;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArmasController : ControllerBase
    {
        private readonly DataContext _context;

        public ArmasController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.TBL_ARMA.ToListAsync());
        }

        [HttpPost("Post")]
        public async Task<IActionResult> Adicionar(Arma arma)
        {
            if (arma == null)
                return BadRequest(new { Message = "Arma não foi inserida corretamente." });

            var armaExistente = await _context.TBL_ARMA
                .FirstOrDefaultAsync(a => a.PersonagemId == arma.PersonagemId);

            if (armaExistente != null)
                return BadRequest(new { Message = "Esse personagem já possui uma arma cadastrada." });

            arma.Id = 0;
            await _context.TBL_ARMA.AddAsync(arma);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Arma adicionada com sucesso." });
        }

        [HttpPut("Put")]
        public async Task<IActionResult> Atualizar(Arma arma)
        {
            if (arma == null)
                return BadRequest(new { Message = "Por favor, insira as informações da arma." });

            var armaEncontrada = await _context.TBL_ARMA.FindAsync(arma.Id);

            if (armaEncontrada == null)
                return BadRequest(new { Message = "Arma não existe." });

            armaEncontrada.Nome = arma.Nome;
            armaEncontrada.Dano = arma.Dano;
            armaEncontrada.Personagem = arma.Personagem;
            armaEncontrada.PersonagemId = arma.PersonagemId;

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Arma editada." });
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var arma = await _context.TBL_ARMA.SingleOrDefaultAsync(u => u.Id == id);

            if (arma == null)
                return BadRequest(new { Message = "Nenhuma arma encontrada." });

            return Ok(arma);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var numeroRowsDeletadas = await _context.TBL_ARMA
                .Where(u => u.Id == id)
                .ExecuteDeleteAsync();

            return Ok(new { Message = "Sucesso." });
        }
    }
}
