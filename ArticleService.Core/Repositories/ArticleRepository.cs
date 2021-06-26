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

        public async Task<string> WithdrawArticle(string content)
        {
            MQConfigs mq = new MQConfigs();
            mq.PublishToMessageQueue("article.withdraw", content);
            return "Article withdrawn";
        }
    }
}
