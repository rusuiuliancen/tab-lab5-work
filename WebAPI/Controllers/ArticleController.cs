using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IRepository<Article> _articlesRepository;

        public ArticleController(IRepository<Article> articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }

        [HttpGet("getall")]
        public IEnumerable<ArticleDto> GetAll()
        {
            return _articlesRepository
                .GetAll()
                .Select(article => new ArticleDto(article.Title, article.Description, article.Url));
        }

        [HttpPost("add")]
        public void Post(ArticleDto article)
        {
            _articlesRepository.Add(new Article()
            {
                Title = article.Title,
                Description = article.Description,
                Url = article.Url
            });

            _articlesRepository.SaveChanges();
        }

        [HttpPut("update")]
        public ObjectResult Put(Guid articleId, ArticleDto article)
        {
            var articleFromDb = _articlesRepository.GetById(articleId);

            if (articleFromDb == null)
            {
                return NotFound("Articol not found");
            }

            articleFromDb.Title = article.Title;
            articleFromDb.Description = article.Description;
            articleFromDb.Url = article.Url;

            _articlesRepository.SaveChanges();
            
            return Ok("Articol updated succesfully");
        }

        [HttpDelete("delete")]
        public ObjectResult Delete(Guid articleId)
        {
            var articleFromDb = _articlesRepository.GetById(articleId);

            if (articleFromDb == null)
            {
                return NotFound("Articol not found");
            }

            _articlesRepository.Remove(articleFromDb);
            _articlesRepository.SaveChanges();

            return Ok("Removed succesfully");
        }
    }
}
