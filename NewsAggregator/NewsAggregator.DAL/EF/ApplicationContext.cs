using Microsoft.EntityFrameworkCore;
using NewsAggregator.DAL.Entities;

namespace NewsAggregator.DAL.EF
{
    public class ApplicationContext : DbContext
    {
        public DbSet<NewsSources> NewsSources { get; set; } = null!;
        public DbSet<News> News { get; set; } = null!;

        public ApplicationContext()    // TODO: use DbContextOptions instead OnConfiguring method
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=news.db");
        }
    }
}
