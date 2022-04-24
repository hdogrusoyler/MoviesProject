using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesProject.BusinessLogic;
using MoviesProject.CrossCutting;
using System.Security.Claims;
using static MoviesProject.CrossCutting.CustomCacheAttribute;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesProject.WebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IMovieService movieService;
        public MoviesController(IMovieService _movieService)
        {
            movieService = _movieService;
        }

        // GET api/<MoviesController>/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            return await movieService.Get(id);
        }

        // GET: api/<MoviesController>
        [HttpGet]
        public async Task<Dictionary<string, string>> Get()
        {
            return await movieService.GetMovieList();
        }

        [HttpGet("GetByCategory/{category}")]
        public async Task<Dictionary<string, string>> GetByCategory(string category)
        {
            return await movieService.GetByCategoryMovieList(category);
        }

        [CustomAuthorizeAttribute(ClaimTypes.Role, "User")]
        [CustomCacheAttribute(Duration = 120, Location = ResponseCacheLocation.Client, NoStore = false)]
        [HttpGet("Categories")]
        public async Task<List<string>> GetCategories()
        {
            if (false)
            {
                throw new NotImplementedException();
            }

            return await movieService.GetCategoryList();
        }

        // POST api/<MoviesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MoviesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MoviesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
