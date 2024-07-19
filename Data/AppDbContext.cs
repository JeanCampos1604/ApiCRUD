using ApiCrud.Estudantes;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Estudante> Estudantes { get; set; }
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            if (String.IsNullOrEmpty(connectionString))
                optionsBuilder.UseOracle(connectionString);
            else
                throw new InvalidOperationException("A connection string 'DefaultConnection' não está definida nos segredos do usuário.");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Estudante>(entity =>
        {
            entity.ToTable("ESTUDANTES");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nome).HasColumnName("NOME");
            entity.Property(e => e.Ativo).HasColumnName("ATIVO");
        });
        }
    }
}