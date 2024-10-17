using API_Redis.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Redis.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Configurações das tabelas no banco de dados.
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Marca> Marcas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            // Mapeamento da chave primária para Carro
            modelBuilder.Entity<Carro>()
                .ToTable("Carro") // Define o nome da tabela
                .HasKey(c => c.Id); // Define 'Id' como chave primária

            // Mapeamento da chave primária para Marca
            modelBuilder.Entity<Marca>()
                .ToTable("Marca") // Define o nome da tabela
                .HasKey(m => m.Id); // Define 'Id' como chave primária

            // Configuração de relacionamento entre Carro e Marca.
            modelBuilder.Entity<Carro>()
                .HasOne(c => c.Marca)
                .WithMany(m => m.Carros)
                .HasForeignKey(c => c.MarcaId);
        }
    }
}
