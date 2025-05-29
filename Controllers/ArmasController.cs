using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgApi.Data;
using RpgApi.Models;

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
                return Ok(
                await _context
                .TBL_ARMA
                .ToListAsync());
        }

        [HttpPut("Put")]
        public async Task<IActionResult> Atualizar(Arma arma)
        {
            if (arma == null)
            {
                return BadRequest(
                    new
                    {
                        Message = "Por favor, insira as informações da arma."
                    });
            }
            var armaEncontrada = _context
                                 .TBL_ARMA
                                 .
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _context
                .TBL_ARMA
                .SingleOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return BadRequest(new
                {
                    Message = "Nenhuma arma encontrada."
                });
            }

            return Ok(user);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var numeroRowsDeletadas = await _context
                .TBL_ARMA
                .Where(u => u.Id == id)
                .ExecuteDeleteAsync();

            return Ok(new
            {
                Message = "Sucesso."
            });
        }
    }
}
