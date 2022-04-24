using MoviesProject.MovieAccess;

namespace MoviesProject.BusinessLogic
{
    public class MovieManager : IMovieService
    {
        private IMovieAccess _movieAccess;
        public MovieManager(IMovieAccess movieAccess)
        {
            _movieAccess = movieAccess;
        }

        public async Task<string> Get(int id)
        {
            return await _movieAccess.Get(id);
        }

        public async Task<Dictionary<string, string>> GetByCategoryMovieList(string category)
        {
            return await _movieAccess.GetByCategoryMovieList(category);
        }

        public async Task<List<string>> GetCategoryList()
        {
            return await _movieAccess.GetCategoryList();
        }

        public async Task<Dictionary<string, string>> GetMovieList()
        {
            return await _movieAccess.GetMovieList();
        }
    }
}