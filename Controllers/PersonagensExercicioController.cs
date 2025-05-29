using RpgApi.Models.Enuns;
using RpgApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonagensExercicioController : ControllerBase
    {
        private static List<Personagem> personagens = new List<Personagem>()
        {
        new Personagem() { Id = 1, Nome = "Frodo", PontosVida=100, Forca=17, Defesa=23, Inteligencia=33, Classe=ClasseEnum.Cavaleiro },
        new Personagem() { Id = 2, Nome = "Sam", PontosVida=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro },
        new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=100, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Clerigo },
        new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=100, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago },
        new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro },
        new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=102, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Clerigo },
        new Personagem() { Id = 7, Nome = "Radagast", PontosVida=103, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago }
        };

        [HttpGet("GetByNome/{nome}")]
        public IActionResult GetByNome(string nome)
        {
            Personagem? p = personagens.Find(p => p.Nome == nome);
            if (p == null) return NotFound(new { Message = "[Erro] Personagem não encontrado! "});

            return Ok(p);
        }

        [HttpGet("GetClerigoMago")]
        public IActionResult GetClerigoMago()
        {
            return Ok(new {
                 ListaOrdenada = personagens.OrderByDescending(p => p.PontosVida)
            });
        }

        [HttpGet("GetEstatisticas")]
        public IActionResult GetEstatisticas()
        {
            return Ok(new {
                TotalPersonagens = personagens.Count,
                TotalInteligencia = personagens.Sum(p => p.Inteligencia)
            });
        }

        [HttpPost("PostValidacao")]
        public IActionResult PostValidacao(Personagem personagem)
        {
            if (personagem.Defesa < 10)
            {
                return BadRequest(new {
                    Message = "[Erro] Defesa deve ser maior ou igual a 10."
                });
            }
            else if (personagem.Inteligencia > 30)
            {
                return BadRequest(new {
                    Message = "[Erro] A Inteligência deve ser menor ou igual a 30."
                });
            }

            personagens.Add(personagem);

            return Ok(new {
                Message = "[Sucesso] Personagem criado."
            });
        }

        [HttpGet("GetByClasse/{classe}")]
        public IActionResult GetByClasse(ClasseEnum classe)
        {
            return Ok(personagens.FindAll(p => p.Classe == classe));
        }

        [HttpPost("PostValidacaoMago")]
        public IActionResult PostValidacaoMago(Personagem personagem)
        {
            if (personagem.Inteligencia < 35)
            {
                return BadRequest(new {
                    Message = "[Erro] A Inteligência deve ser maior ou igual a 35"
                });
            }

            personagem.Classe = ClasseEnum.Mago;
            personagens.Add(personagem);

            return Ok(new {
                Message = "[Sucesso] Mago criado."
            });
        }
    }
}
