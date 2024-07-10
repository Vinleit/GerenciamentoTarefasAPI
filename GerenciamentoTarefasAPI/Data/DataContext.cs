using GerenciamentoTarefasAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoTarefasAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=TarefasDB;Trusted_Connection=True;TrustServerCertificate=true");
            }
        }

        public DbSet<Tarefa> Tarefas { get; set; }
    }
}
