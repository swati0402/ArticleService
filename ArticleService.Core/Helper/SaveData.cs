using ArticleService.Core.Data;
using ArticleService.Core.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace ArticleService.Core.Helper
{
    public class SaveData
    {
        public async Task<string> AddArticleAsync(JObject data,string type)
        {
            try
            {
                Article article = new Article()
                {
                    AssetId = data["AssetId"].Value<string>(),
                    Slug = data["Slug"].Value<string>(),
                    Thumbnail = data["Thumbnail"].Value<string>(),
                    Url = data["Url"].Value<string>(),
                    PublicationDateUtc = data["PublicationDateUtc"].Value<string>(),
                    Author = data["Author"].Value<string>(),
                    AssetType = data["AssetType"].Value<string>(),
                    Body = data["Body"].Value<string>(),
                    Geographies = data["Geographies"].Value<string[]>(),
                    Toipcs = data["Toipcs"].Value<string[]>(),
                    Version = data["Version"].Value<int>()
                };

                var contextOptions = new DbContextOptionsBuilder<ArticleServiceContext>()
                    .UseSqlite(@"Data Source=Articles.db")
                    .Options;
                var dbContext = new ArticleServiceContext(contextOptions);

                int article_id = (int)await dbContext.Articles.MaxAsync(x => x.Id);
                if(article_id == 0)
                {
                    article.Id = 1;
                }
                else
                {
                    article.Id = article_id + 1;
                }
                if (type == "article.add")
                {
                   await dbContext.Articles.AddAsync(article);
                   await dbContext.SaveChangesAsync();
                }
                return "";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
