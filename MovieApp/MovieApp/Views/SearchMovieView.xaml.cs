using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MovieApp.ViewModels;
using MovieApp.Models;

namespace MovieApp.Views
{

    

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchMovieView : ContentPage
    {
        SearchMovieViewModel vm; 
        public SearchMovieView()
        {
            InitializeComponent();
            //Bind the context from the view model to the view 
            vm = new SearchMovieViewModel();
            BindingContext = vm; 

        }

        protected async override void OnAppearing()
        {
            // ON appear load the data
            //if only the list is empty load the data else make the selected movie null for new selection value 


            if (vm.Movies.Count == 0)
                await Task.Run(() => vm.LoadMovieCommand.Execute(null));
           
            
            //else
             //vm.SelectedMovie = null;
        }



    }
}