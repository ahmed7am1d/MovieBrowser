using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using System.Timers;
using MovieApp.ViewModels;
using System.Threading.Tasks;

namespace MovieApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        private Timer timer;
        SearchMovieViewModel vm;
        public HomeView()
        {
            vm = new SearchMovieViewModel();
            BindingContext = vm;
            InitializeComponent();
        }


        #region OnAppear Method
        protected async override void OnAppearing()
        {
            //on the start of the app load the data just to show user some data
            if (vm.Movies.Count == 0)
                await Task.Run(() => vm.LoadMovieCommand.Execute(null));


            timer = new Timer(TimeSpan.FromSeconds(8).TotalMilliseconds) { AutoReset = true, Enabled = true };
            //what will happen with the timer is specified with the Timer Elapsed method (each 8 second)
            timer.Elapsed += Timer_Elapsed;
            base.OnAppearing();
        }
        #endregion


        #region OnDisappear Method
        protected override void OnDisappearing()
        {
            timer?.Dispose();
            base.OnDisappearing();
        }
        #endregion


        #region Method for Timer Event
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                //position of current view indicatior will increase and reset depend on last position or number of views
                //each 8 second this will happen to auto slide the caresoule

                if (cvWalkthrough.Position == 2)
                {
                    cvWalkthrough.Position = 0;
                    return;
                }

                cvWalkthrough.Position += 1;
            });
        }
    }
    #endregion

}