using fiap2022.domain.Models;
using Microsoft.EntityFrameworkCore;

namespace fiap2022.persistence.Contexts
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
