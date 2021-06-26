using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using ArticleService.Core.Models;
using ArticleService.Core.Interfaces;
using System.Threading.Tasks;
using System.Text.Json;

namespace ArticleService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {

        private readonly ILogger<ArticleController> _logger;
        private readonly IArticleRepository _repository;

        public ArticleController(ILogger<ArticleController> logger, IArticleRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        //Withdrawn article by id
        [HttpDelete]
        public async Task<IActionResult> WithdrawArtcle([FromQuery] string assetid, [FromQuery] int version)
        {
            var response = "";
            try
            {
                if (_repository != null)
                {
                    _logger.LogInformation($"Withdraw article by assetid, version {assetid} {version}");
                    var request = new { AssetId = assetid, Version = version };
                    var item = JsonSerializer.Serialize(request, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, MaxDepth = 20 });
                    response = await _repository.WithdrawArticle(item);
                    return Ok(response);
                }
                return BadRequest("Service issue");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Publish new Article
        [HttpPost]
        public async Task<ActionResult<Article>> PublishArticle([FromBody] Article article)
        {
            try
            {
                if (_repository != null)
                {
                    var item = JsonSerializer.Serialize(article, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, MaxDepth = 20 });

                    _logger.LogInformation($"New article is published with AssetId: {article.AssetId}");

                    await _repository.NewArticle(article, item).ConfigureAwait(false);
                    return Ok(article);
                }

                return BadRequest("Service issue");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
