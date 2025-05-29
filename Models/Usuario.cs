using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RpgApi.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string PasswordSalt { get; set; } = string.Empty;
    public byte[]? Foto { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public DateTime? DataAcesso { get; set; }
    [NotMapped]
    public string PasswordString { get; set; } = string.Empty;
    public List<Personagem> Personagens { get; set; } = new List<Personagem>();
    public string? Perfil { get; set; }
    public string? Email { get; set; }
}
