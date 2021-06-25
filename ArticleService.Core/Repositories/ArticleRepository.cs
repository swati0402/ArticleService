using ArticleService.Core.Interfaces;
using ArticleService.Core.Models;
using System.Threading.Tasks;
using ArticleService.Core.Helper;
using ArticleService.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace ArticleService.Core.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        public async Task<string> NewArticle(Article article, string content)
        {
            MQConfigs mq = new MQConfigs();
            mq.PublishToMessageQueue("article.add", content);

            return  "Article added";
        }

        public async Task<string> WithdrawArticle(string assetid, int version)
        {
            var contextOptions = new DbContextOptionsBuilder<ArticleServiceContext>()
                    .UseSqlite(@"Data Source=Articles.db")
                    .Options;
            var dbContext = new ArticleServiceContext(contextOptions);

            Article oldArticle = await dbContext.Articles.FirstOrDefaultAsync(x => x.AssetId == assetid && x.Version == version);
            //if no Article found return 
            if (oldArticle == null)
            {
                return "No Article with this AssetId  :" + assetid +" version:" + version.ToString();
            }
            dbContext.Articles.Remove(oldArticle);

            await dbContext.SaveChangesAsync();
            return "Article withdrawn";
        }
    }
}
