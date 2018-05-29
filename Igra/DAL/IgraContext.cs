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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
