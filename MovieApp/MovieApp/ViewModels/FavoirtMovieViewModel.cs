using MovieApp.Models;
using MovieApp.Views;
using MovieApp.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;

namespace MovieApp.ViewModels
{
    public class FavoirtMovieViewModel : BaseViewModel
    {
        //1- List<> to obtain data from internet 
        //2- IEnumerable to fill it from List<>
        //3- Obseravable to Fill it from  IEnumerable => so it can be used in View or CollectionView

        #region Memebers 

        List<Movie> MovieList;
        List<Movie> TVShowList;

        #endregion

        #region Proprties 

        private Movie selectedMovie;

        public Movie SelectedMovie
        {
            get { return selectedMovie; }
            set { SetProperty(ref selectedMovie, value); }
        }

        public ObservableCollection<Movie> Movies { get; }
        public ObservableCollection<Movie> TVShows { get; }



        #endregion

        #region ICommands 
        public ICommand LoadFavoritsMoviesCommand { get; private set; }
        public ICommand LoadFavoritsTVSHOWSCommand { get; private set; }

        public ICommand GoToDetailCommand { get; set; }
        #endregion

        #region Methods

        private async Task LoadFavoritMovies()
        {
            IsBusy = true;
            MovieList = await App.DbContext.GetMovies();
            Movies.Clear();
            IEnumerable<Movie> movieSet = MovieList;
            foreach(Movie movie in movieSet)
            {
                Movies.Add(movie);
            }
        }

        private async Task LoadFavoritShowes()
        {
            IsBusy = true;
            TVShowList = await App.DbContext.GetTVShows();
            TVShows.Clear();
            IEnumerable<Movie> TVShowSet = TVShowList;
            foreach (Movie TVShow in TVShowSet)
            {
                TVShows.Add(TVShow);
            }
        }


        private async Task GoToDetail()
        {
            MovieDetailViewModel vm = new MovieDetailViewModel();
            Movie movie = await Services.MovieService.GetMovieByID(SelectedMovie.imdbID);

            vm.SelectedMovie = movie;

            vm.FavbuttonStatusLabel = "Remove From the Faviourt Menu";
            MovieDetailsView view = new MovieDetailsView();
            view.BindingContext = vm;

            await App.Current.MainPage.Navigation.PushModalAsync(view);

        }

        #endregion

        #region Constructor 
        public FavoirtMovieViewModel()
        {
            MovieList = new List<Movie>();
            Movies = new ObservableCollection<Movie>();
            TVShows = new ObservableCollection<Movie>();
            LoadFavoritsMoviesCommand = new Command(async () => await LoadFavoritMovies());
            LoadFavoritsTVSHOWSCommand = new Command(async () => await LoadFavoritShowes());
            GoToDetailCommand = new Command(async () => await GoToDetail());
        }
        #endregion

    }
}
