using ArticleService.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ArticleService.Core.Data
{
    public class ArticleServiceContext : DbContext
    {
        public ArticleServiceContext(DbContextOptions<ArticleServiceContext> options) : base(options)
        {
            // NoOp
        }

        public DbSet<Article> Articles { get; set; }
    }
}
