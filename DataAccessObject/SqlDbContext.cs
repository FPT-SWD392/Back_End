using BusinessObject.SqlObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessObject
{
    public class SqlDbContext : DbContext
    {
        public DbSet<ArtInfo> ArtInfo { get; set; }
        public DbSet<ArtRating> ArtRating { get; set; }
        public DbSet<Commission> Commission { get; set; }
        public DbSet<CreatorInfo> CreatorInfo { get; set; }
        public DbSet<Follow> Follow { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<PostLike> PostLike { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<TransactionHistory> TransactionHistorie { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<ArtTag> ArtTag { get; set; }
        public SqlDbContext()
        {
        }
        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            var strConn = config["ConnectionStrings:SqlConnectionString"];
            return strConn;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(GetConnectionString());
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
