using Adopcat.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Adopcat.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=AdopcatDb")
        {
        }

        public virtual DbSet<ApplicationParameter> ApplicationParameter { get; set; }
        public virtual DbSet<SystemLog> SystemLog { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Token> Token { get; set; }
        public virtual DbSet<Poster> Poster { get; set; }
        public virtual DbSet<PetPicture> PetPicture { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
    public interface IDbContextFactory
    {
        DatabaseContext GetDbContext();
    }

    public class DatabaseContextFactory : IDbContextFactory
    {
        private readonly DatabaseContext _context;

        public DatabaseContextFactory()
        {
            _context = new DatabaseContext();
        }

        public DatabaseContext GetDbContext()
        {
            return _context;
        }
    }
}