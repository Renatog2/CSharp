using CSharpMVC.Models;
using Microsoft.EntityFrameworkCore;

// Esse Contexto é a ponte de conexão entre o C# e o Banco de Dados
namespace CSharpMVC.Data
{
    public class DBContext : DbContext
    {
        // Construtor
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<PessoasModel> Pessoas { get; set; }

        public DbSet<TicketsModel> Tickets { get; set; }
    }
}
