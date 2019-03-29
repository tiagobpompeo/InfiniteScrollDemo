using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace InfiniteScrollDemo
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public int pageIndex = 0;
        private bool _isBusy;
        readonly Service _service = new Service();
        public ICommand RefreshCommand { get; }
        public InfiniteScrollCollection<MoviesNewClass.Resultado> Movies { get; }

        public MainViewModel()
        {

            Movies = new InfiniteScrollCollection<MoviesNewClass.Resultado>
            {
                //todo scrool dispara esse evento
                OnLoadMore = async () =>
                {
                    IsBusy = true;
                 
                    pageIndex++;
                    var movies = await _service.GeAllMovies(pageIndex);
                   
                    IsBusy = false;
                    ClearOld();
                    return movies;
                }

            };


            RefreshCommand = new Command(() =>
            {
                // clear and start again
                Movies.Clear();
                Movies.LoadMoreAsync();
            });

            Movies.LoadMoreAsync();

        }

        private void ClearOld()
        {
            // clear and start again
            Movies.Clear();
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

       

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}