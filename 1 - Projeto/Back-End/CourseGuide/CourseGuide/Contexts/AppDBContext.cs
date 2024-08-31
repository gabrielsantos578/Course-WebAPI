using Microsoft.EntityFrameworkCore;

namespace CourseGuide.Contexts
{
    public class AppDBContext : DbContext
    {
        // Mapeamento Relacional dos Objetos no Bando de Dados
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }


        // Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}