using Microsoft.EntityFrameworkCore;
using GameLibrary.Models; // Certifique-se de incluir o namespace correto para suas classes

namespace GameLibrary.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {

        }

        // Classes que se tornarão tabelas no banco de dados
        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<Jogador> Jogadores { get; set; }
        public DbSet<JogosJogados> RegistrosJogosJogados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<JogosJogados>()
        .HasOne(j => j.Jogador)
        .WithMany()
        .HasForeignKey(j => j.JogadorId);

    modelBuilder.Entity<JogosJogados>()
        .HasOne(j => j.Jogo)
        .WithMany()
        .HasForeignKey(j => j.JogoId);
}

    }
}
