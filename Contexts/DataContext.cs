using fiap2022.Models;
using Microsoft.EntityFrameworkCore;

namespace fiap2022.Contexts
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }


        public DbSet<Time> Times{ get; set; }
        public DbSet<Jogador> Jogadores{ get; set; }
    }
}
