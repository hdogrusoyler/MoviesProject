namespace MoviesProject.MovieAccess
{
    public class MovieAccessClass : IMovieAccess
    {
        public Dictionary<string, string> MovieList {
            get { 
                return new Dictionary<string, string> {
                    {"Film0","Category1" },
                    {"Film1","Category1" },
                    {"Film2","Category2" },
                    {"Film3","Category3" },
                    {"Film4","Category4" },
                    {"Film5","Category5" }
                }; 
            }
        
        }

        public async Task<string> Get(int id)
        {
            return MovieList.ElementAt(id).Key + " - " + MovieList.ElementAt(id).Value;
        }

        public async Task<List<string>> GetCategoryList()
        {
            return MovieList.Values.Distinct().ToList();
        }

        public async Task<Dictionary<string, string>> GetMovieList()
        {
            return MovieList;
        }

        public async Task<Dictionary<string, string>> GetByCategoryMovieList(string category)
        {
            return MovieList.Where(x => x.Value == category).ToDictionary(it => it.Key, it => it.Value);
        }

    }
}