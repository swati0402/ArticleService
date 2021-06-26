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
        public async Task<string> ProcessessData(JObject data, string type)
        {
            var response="";
            try
            {
                if (type == "article.add")
                {
                    response=await AddArticleAsync(data);
                }
                else if (type == "article.withdraw")
                {
                    response=await RemoveArticle(data);
                }
                return response;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> AddArticleAsync(JObject data)
        {
            try
            {
                var contextOptions = new DbContextOptionsBuilder<ArticleServiceContext>()
                    .UseSqlite(@"Data Source=Articles.db")
                    .Options;
                var dbContext = new ArticleServiceContext(contextOptions);
                //Ensure database is created
                dbContext.Database.EnsureCreated();

                int article_id = 0;
                article_id = await dbContext.Articles.MaxAsync(x => (int?)x.Id) ?? 0;
                ArticleData article = new ArticleData();

                article.AssetId = data["AssetId"].Value<string>();
                article.Slug = data["Slug"].Value<string>();
                article.Thumbnail = data["Thumbnail"].Value<string>();
                article.Url = data["Url"].Value<string>();
                article.PublicationDateUtc = data["PublicationDateUtc"].Value<string>();
                article.Author = data["Author"].Value<string>();
                article.AssetType = data["AssetType"].Value<string>();
                article.Body = data["Body"].Value<string>();
                article.Geographies = data["Geographies"].ToString();
                article.Topics = data["Topics"].ToString();
                article.Version = data["Version"].Value<int>();

                if (article_id == 0)
                {
                    article.Id = 1;
                }
                else
                {
                    article.Id = article_id + 1;
                }
                await dbContext.Articles.AddAsync(article);
                await dbContext.SaveChangesAsync();

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> RemoveArticle(JObject data)
        {
            try
            {
                string assetid = data["AssetId"].Value<string>();
                int version = data["Version"].Value<int>();
                var contextOptions = new DbContextOptionsBuilder<ArticleServiceContext>()
                        .UseSqlite(@"Data Source=Articles.db")
                        .Options;
                var dbContext = new ArticleServiceContext(contextOptions);

                ArticleData oldArticle = await dbContext.Articles.FirstOrDefaultAsync(x => x.AssetId == assetid && x.Version == version);
                //if no Article found return 
                if (oldArticle == null)
                {
                    return "No Article with this AssetId  :" + assetid + " version:" + version.ToString();
                }
                dbContext.Articles.Remove(oldArticle);

                await dbContext.SaveChangesAsync();
                return "Article withdrawn";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
