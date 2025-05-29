using System;

namespace RpgApi.Models;

public class PersonagemHabilidade
{
    public int PersonagemId { get; set; }
    public Personagem Personagem { get; set; } = null!;
    public int HabilidadeId { get; set; }
    public Habilidade? Habilidade { get; set; } = null!;
}
