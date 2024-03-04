using Microsoft.EntityFrameworkCore;
using URLShortenerAPI.Model;

namespace URLShortenerAPI.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<Url> Urls { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
