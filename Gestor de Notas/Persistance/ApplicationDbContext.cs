using Gestor_de_Notas.Model;
using Gestor_de_Notas.Persistance.Config;
using Microsoft.EntityFrameworkCore;

namespace Gestor_de_Notas.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }
        
        /// db set ingles
        
        public DbSet<Campo>Campo { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Ciclo> Ciclo { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {

            base.OnModelCreating(builder);
            new CampoConfig(builder.Entity<Campo>());
            new CursoConfig(builder.Entity<Curso>());
            new CicloConfig(builder.Entity<Ciclo>());

        }
    }
}
