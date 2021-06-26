using ArticleService.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ArticleService.Core.Data
{
    public class ArticleServiceContext : DbContext
    {
        public ArticleServiceContext(DbContextOptions<ArticleServiceContext> options) : base(options)
        {
            // NoOp
        }

        public DbSet<ArticleData> Articles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Articles.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.Entity<ArticleData>().ToTable("Articles", "dbo");
            //modelBuilder.Entity<Article>(entity =>
            //{
            //    entity.HasKey(e => e.AssetId);
            //});
            base.OnModelCreating(modelBuilder);
        }
    }
}
