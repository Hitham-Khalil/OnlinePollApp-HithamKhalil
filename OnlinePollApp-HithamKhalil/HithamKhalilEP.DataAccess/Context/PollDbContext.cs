using Microsoft.EntityFrameworkCore;
using YourNameEP.Domain.Models;

namespace YourNameEP.DataAccess.Context
{
    public class PollDbContext : DbContext
    {
        public PollDbContext(DbContextOptions<PollDbContext> options) : base(options) { }

        public DbSet<Poll> Polls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // You can add configuration here if needed later
        }
    }
}
