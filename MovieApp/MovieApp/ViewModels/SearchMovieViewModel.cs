using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;

using MovieApp.Models;
using MovieApp.Services;
using MovieApp.Views;
using MovieApp.ViewModels;
using Xamarin.Forms;
using System;

namespace MovieApp.ViewModels
{
    //1- List<> to obtain data from internet 
    //2- IEnumerable to fill it from List<>
    //3- Obseravable to Fill it from  IEnumerable => so it can be used in View or CollectionView
    public class SearchMovieViewModel : BaseViewModel
    {

        #region Members Fields 
        //fill it later with the returned list from GetMovie from Services that return list of Movies
        private List<Movie> MovieList;

        #endregion

        #region Proprties 

        //1- selected movie 

        private Movie selectedMovie;

        public Movie SelectedMovie
        {
            get { return selectedMovie; }
            set { SetProperty(ref selectedMovie, value); }
        }




        //readonly property it is useful to display a list of elements 
        //so it will be used for the ListView or CollectionView 
        public ObservableCollection<Movie> Movies { get; }


        #endregion

        #region ICommand

        public ICommand LoadMovieCommand { get; private set; }

        public ICommand SearchMovieCommand { get; private set; }

        public ICommand GoToShowDetailCommand { get; private set; }


        #endregion

        #region Methods 

        //from the user the title should be passed to this method

        private async Task LoadMovie()
        {

            //inform app that is busy at begining (indicator) loading
            IsBusy = true;
            //Choose shows to randomly show when page start 
            List<string> searchTitles = new List<string>()
            {
                "Friend",
                "Prison",
                "XXX",
                "World war",
                "Friends",
                "Elite",
                "Closer",
            };
            Random random = new Random();

            MovieList = await MovieService.GetMovie(searchTitles[random.Next(0, searchTitles.Count)]);
            Movies.Clear();
            //IEnumerable list that will get data from list we download data from internet into it 
            IEnumerable<Movie> movieSet = MovieList;
            foreach (Movie movie in movieSet)
            {
                Movies.Add(await MovieService.GetMovieByID(movie.imdbID));
            }
        }


        private async void SearchMovie(string SearchTitle)
        {
            //1- replace every space by %20 so the API protcol understand it as space 
            SearchTitle = SearchTitle.Replace(" ", "%20");

            //If search title is less than 3 chars don't allow it (api does not accept it !)
            if (SearchTitle.Length < 3)
            {
                await App.Current.MainPage.DisplayAlert("Missing Info!", "Please provide correct search info.", "Ok");
            }
            else
            {
                try
                {


                    //inform app that is busy at begining (indicator) loading
                    IsBusy = true;
                    MovieList = await MovieService.GetMovie(SearchTitle);
                    Movies.Clear();
                    //IEnumerable list that will get data from list we download data from internet into it 
                    IEnumerable<Movie> movieSet = MovieList;
                    foreach (Movie movie in movieSet)
                    {
                        //for each movie get full detial by it is id
                        //we will have a list of all movies that mach search title and each movie with full details       
                        Movies.Add(await MovieService.GetMovieByID(movie.imdbID));
                    }
                }
                catch (NullReferenceException)
                {
                    await App.Current.MainPage.DisplayAlert("Wrong Search Info!", "Please provide correct search info.", "Ok");
                }
            }
        }

        private async Task GoToShowDetail()
        {
            //Navigation to Detail Page 
            //1- Create instance of  MovieDetail ViewModel
            //MovieDetailViewModel vm = new MovieDetailViewModel(SelectedMovie);
            MovieDetailViewModel vm = new MovieDetailViewModel();
            //2- passing values from current view model to detail 
            vm.SelectedMovie = SelectedMovie;
            bool IsFoundInDB = await App.DbContext.IsExist(SelectedMovie.Title);
            if (IsFoundInDB)
            {
                vm.FavbuttonStatusLabel = "Remove From the Faviourt Menu";
            }
            else
            {
                vm.FavbuttonStatusLabel = "Add to Faviourt Menu";
            }
            //3- create instance of Movie Detail for make Binding Context equal to current
            MovieDetailsView view = new MovieDetailsView();
            view.BindingContext = vm;

            await App.Current.MainPage.Navigation.PushModalAsync(view);


        }



        #endregion

        #region Constructor
        public SearchMovieViewModel()
        {
            //initilizing
            MovieList = new List<Movie>();
            Movies = new ObservableCollection<Movie>();

            LoadMovieCommand = new Command(async () => await LoadMovie());
            SearchMovieCommand = new Command<string>(SearchMovie);
            GoToShowDetailCommand = new Command(async () => await GoToShowDetail());

        }


        #endregion


    }
}
