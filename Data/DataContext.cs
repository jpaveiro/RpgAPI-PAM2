using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RpgApi.Models;
using RpgApi.Models.Enuns;
using RpgApi.Utils;

namespace RpgApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Personagem> TBL_PERSONAGEM { get; set; }
        public DbSet<Arma> TBL_ARMA { get; set; }
        public DbSet<Usuario> TBL_USUARIO { get; set; }
        public DbSet<Habilidade> TB_HABILIDADES { get; set; }
        public DbSet<PersonagemHabilidade> TB_PERSONAGENS_HABILIDADES { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personagem>().ToTable("TBL_PERSONAGEM");
            modelBuilder.Entity<Usuario>().ToTable("TBL_USUARIO");
            modelBuilder.Entity<Arma>().ToTable("TBL_ARMA");
            modelBuilder.Entity<Habilidade>().ToTable("TBL_HABILIDADE");
            modelBuilder.Entity<PersonagemHabilidade>().ToTable("TB_PERSONAGEM_HABILIDADE");
            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Personagens)
                .WithOne(e => e.Usuario)
                .HasForeignKey(e => e.UsuarioId)
                .IsRequired(false);
            modelBuilder.Entity<Personagem>()
                .HasOne(e => e.Arma)
                .WithOne(e => e.Personagem)
                .HasForeignKey<Arma>(e => e.PersonagemId)
                .IsRequired();
            modelBuilder.Entity<PersonagemHabilidade>()
                .HasKey(ph => new { ph.PersonagemId, ph.HabilidadeId });

            // Personagens
            List<Personagem> personagens = new List<Personagem>()
            {
                new Personagem() { Id = 1, Nome = "Frodo", PontosVida=100, Forca=17, Defesa=23, Inteligencia=33, Classe=ClasseEnum.Cavaleiro, UsuarioId = 1 },
                new Personagem() { Id = 2, Nome = "Sam", PontosVida=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro, UsuarioId = 1 },
                new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=100, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Clerigo, UsuarioId = 1 },
                new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=100, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago, UsuarioId = 1 },
                new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro, UsuarioId = 1 },
                new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=102, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Clerigo, UsuarioId = 1 },
                new Personagem() { Id = 7, Nome = "Radagast", PontosVida=103, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago, UsuarioId = 1 }
            };
            modelBuilder.Entity<Personagem>().HasData(personagens);

            // Armas
            List<Arma> armas = new List<Arma>()
            {
                new Arma() { Id = 1, Nome = "AK-47", Dano = 17, PersonagemId = 1 },
                new Arma() { Id = 2, Nome = "Matadora de Dragões", Dano = 53, PersonagemId = 2 },
                new Arma() { Id = 3, Nome = "Martelo do Gigante", Dano = 32, PersonagemId = 3 },
                new Arma() { Id = 4, Nome = "Presa da Serpente", Dano = 25, PersonagemId = 4 },
                new Arma() { Id = 5, Nome = "Luvas com Espinhos", Dano = 9, PersonagemId = 5 },
                new Arma() { Id = 6, Nome = "Foice da Perdição", Dano = 40, PersonagemId = 6 },
                new Arma() { Id = 7, Nome = "Super Manopla", Dano = 29, PersonagemId = 7 }
            };
            modelBuilder.Entity<Arma>().HasData(armas);

            //Criptografia.CriarPasswordHash("123456", out byte[] hash, out byte[] salt);
            Usuario usuario = new Usuario();
            usuario.Id = 1;
            usuario.Username = "UsuarioAdmin";
            usuario.PasswordString = string.Empty;
            usuario.PasswordHash = "hash muito louco";
            usuario.PasswordSalt = "salt muito louco";
            usuario.Perfil = "Admin";
            usuario.Email = "seuEmail@gmail.com";
            usuario.Latitude = -23.5200241;
            usuario.Longitude = -46.596498;
            modelBuilder.Entity<Usuario>().HasData([usuario]);
            modelBuilder
                .Entity<Usuario>()
                .Property(u => u.Perfil)
                .HasDefaultValue("Jogador");

            modelBuilder.Entity<Habilidade>().HasData(
                new Habilidade() { Id = 1, Nome = "Adormecer", Dano = 39 },
                new Habilidade() { Id = 2, Nome = "Congelar", Dano = 41 },
                new Habilidade() { Id = 3, Nome = "Hipnotizar", Dano = 37 }
            );
            modelBuilder.Entity<PersonagemHabilidade>().HasData(
                new PersonagemHabilidade() { PersonagemId = 1, HabilidadeId = 1 },
                new PersonagemHabilidade() { PersonagemId = 1, HabilidadeId = 2 },
                new PersonagemHabilidade() { PersonagemId = 2, HabilidadeId = 2 },
                new PersonagemHabilidade() { PersonagemId = 3, HabilidadeId = 2 },
                new PersonagemHabilidade() { PersonagemId = 3, HabilidadeId = 3 },
                new PersonagemHabilidade() { PersonagemId = 4, HabilidadeId = 3 },
                new PersonagemHabilidade() { PersonagemId = 5, HabilidadeId = 1 },
                new PersonagemHabilidade() { PersonagemId = 6, HabilidadeId = 2 },
                new PersonagemHabilidade() { PersonagemId = 7, HabilidadeId = 3 }
            );
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveColumnType("varchar").HaveMaxLength(200);
        }
    }
}
