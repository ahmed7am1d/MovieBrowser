
using System.Threading;
using System.Collections.ObjectModel;
using MovieApp.Models;
using MovieApp.Services;
using System.Windows.Input;
using System.Threading.Tasks;
using MovieApp.Views;
using Xamarin.Forms;

namespace MovieApp.ViewModels
{
    public class HomeViewModel
    {
        #region Proprties 

        public ObservableCollection<WalkThrough> WalkThroughItmes { get => LoadData(); }

        #endregion

        #region ICommand

        public ICommand MoveToShellCommand { get; private set; }
        public object Movies { get; internal set; }

        #endregion

        #region Methods 

        private void MoveToShell()
        {
            //await App.Current.MainPage.DisplayAlert("Hello", "Hello my name is ahmed", "oK!");
            //await App.Current.MainPage.Navigation.PushAsync(new SearchMovieView());
            Application.Current.MainPage = new AppShell();
            //AppShell page = new AppShell();
            //await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(page));
            //await App.Current.MainPage.Navigation.PushAsync(new AppShell());
        }

        private ObservableCollection<WalkThrough> LoadData()
        {
            return CarouselViewServices.GetItemsForCarousel();
        }


        #endregion

        #region Constructor
        public HomeViewModel()
        {
            MoveToShellCommand = new Command(() => MoveToShell());
        }
        #endregion

    }
}
