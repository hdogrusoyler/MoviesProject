using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesProject.BusinessLogic
{
    public interface IMovieService
    {
        Task<string> Get(int id);
        Task<List<string>> GetCategoryList();
        Task<Dictionary<string,string>> GetMovieList();
        Task<Dictionary<string, string>> GetByCategoryMovieList(string category);
    }
}
