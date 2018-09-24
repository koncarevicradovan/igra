using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Igra.DAL
{
    public class IgraContext : DbContext
    {

        public IgraContext() : base("IgraContext")
        {
        }

        public DbSet<GamingUser> Users { get; set; }
        public DbSet<ReadyForGame> ReadyForGameUsers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<FifthGame> FifthGames { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
