using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace InfiniteScrollDemo
{
    public class Service
    {
        ObservableCollection<MoviesNewClass.Resultado> _movies = new ObservableCollection<MoviesNewClass.Resultado>();
        MoviesManager moviesManager = new MoviesManager();


        public async Task<List<MoviesNewClass.Resultado>> GeAllMovies(int pageIndex)
        {
            var movies = await moviesManager.GetAll(pageIndex);//comeca e 1
            var result = movies.Results;

            //sempre vem 20
            foreach (var a in result)
            {
                if (_movies.All(b => b.Id != a.Id))
                {
                    _movies.Add(new MoviesNewClass.Resultado
                    {
                        Title = a.Title
                    });
                }
            }

            return _movies.ToList();
        }

    }
}
