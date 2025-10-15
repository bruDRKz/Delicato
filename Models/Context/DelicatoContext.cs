using Microsoft.EntityFrameworkCore;

namespace DelicatoProject.Models.Context
{
    public class DelicatoContext : DbContext
    {
        public DelicatoContext(DbContextOptions<DelicatoContext> options) : base(options) {}

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Reservas> Reservas { get; set; }
        public DbSet<CardapioComidas> CardapioComidas { get; set; }
        public DbSet<CardapioBebidas> CardapioBebidas { get; set; }
        public DbSet<Avaliacao> Avaliacao { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
    }
}
