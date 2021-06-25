using ArticleService.Core.Models;
using System.Threading.Tasks;

namespace ArticleService.Core.Interfaces
{
    public interface IArticleRepository
    {
        Task<string> NewArticle(Article article, string content);
        Task<string> WithdrawArticle(string assetid, int version);
    }
}
