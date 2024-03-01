using BusinessObject.SqlObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessObject
{
    public class SqlDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public SqlDbContext()
        {
        }
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
        }
    }
}
