using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoirtMoviesView : ContentPage
    {
        FavoirtMovieViewModel vm;
        public FavoirtMoviesView()
        {
            InitializeComponent();
            vm = new FavoirtMovieViewModel();
            BindingContext = vm;
        }



        protected async override void OnAppearing()
        {
            //load the data every time page appear  even if the list is filled
            await Task.Run(() => vm.LoadFavoritsMoviesCommand.Execute(null));
            await Task.Run(() => vm.LoadFavoritsTVSHOWSCommand.Execute(null));
        }

    }
}