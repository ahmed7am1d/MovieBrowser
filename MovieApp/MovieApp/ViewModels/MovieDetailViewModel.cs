using System;
using System.Collections.Generic;
using System.Text;

using MovieApp.ViewModels;
using MovieApp.Models;
using MovieApp.Services;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace MovieApp.ViewModels
{
    public class MovieDetailViewModel : BaseViewModel
    {

        #region Proprties 

        private Movie selectedMovie;

        public Movie SelectedMovie
        {
            get { return selectedMovie; }
            set { SetProperty(ref selectedMovie, value); }
        }

        private string favbuttonStatusLabel;

        public string FavbuttonStatusLabel
        {
            get { return favbuttonStatusLabel; }
            set { SetProperty(ref favbuttonStatusLabel, value); }
        }



        #endregion

        #region ICommands 
        public ICommand AddToFavCommand { get; private set; }
        public ICommand ShareInfoSMSCommand { get; private set; }
        public ICommand ShareInfoEmailCommand { get; private set; }


        #endregion

        #region Methods 


        private async Task AddToFav()
        {
            //1- Check if the current item is exist in the data base if so delete it from the data base
            //because the button will be (Remove from the faviorte menu) 
            bool IsExistInDB = await App.DbContext.IsExist(SelectedMovie.Title);
            if (IsExistInDB)
            {
                var result = await Application.Current.MainPage.DisplayAlert("Alert!!!!", "Do you want to remove the Show from the Favioret menu ?", "Yes", "No");
                if (result == true)
                {
                    Movie movie = new Movie();
                    var DatabaseShow = await App.DbContext.GetItems<Movie>();
                    foreach (Movie movie1 in DatabaseShow)
                    {
                        if (movie1.Title == SelectedMovie.Title)
                        {
                            movie = movie1;
                            break;
                        }
                    }


                    bool IsDeleted = await App.DbContext.DeleteItem<Movie>(movie);
                    if (IsDeleted)
                        await Application.Current.MainPage.DisplayAlert("Successful", "The Show is removed from the faviourt menu", "Ok");

                    FavbuttonStatusLabel = "Add to Faviourt Menu";
                }
                else
                    return;
            }
            else
            {
                await App.DbContext.AddNewItem<Movie>(SelectedMovie);
                await App.Current.MainPage.DisplayAlert("Successful!", "The Show is added to the faviourt menu", "Ok");
                FavbuttonStatusLabel = "Remove From the Faviourt Menu";
            }
        }

        private async Task ShareInfoEmail()
        {
            var message = new EmailMessage
            {
                Subject = await App.Current.MainPage.DisplayPromptAsync("Email's Subject", "Enter the Subject of the Email","Send","Cancel"),
                Body = $"Hello, One of My Faviourts {SelectedMovie.Type} is {SelectedMovie.Title} which was released " +
                $"in {SelectedMovie.Year} and the Director was {SelectedMovie.Director}. ",
                To = { },
                //Cc = ccRecipients,
                //Bcc = bccRecipients
            };
            if (message.Subject == null)
            {

            }
            else
            {
                await Email.ComposeAsync(message);
            }
        }

        private async Task ShareInfoSMS()
        {
            var message = new SmsMessage();

            message.Body = $"Hello, One of My Faviourts {SelectedMovie.Type} is {SelectedMovie.Title} which was released " +
                $"in {SelectedMovie.Year} and the Director was {SelectedMovie.Director}. ";

            await Sms.ComposeAsync(message);

        }



        #endregion

        #region Constructor

        public MovieDetailViewModel()
        {
            //GetButtonFavStatusCommand = new Command(async () => await AssignButtonLabel());
            AddToFavCommand = new Command(async () => await AddToFav());
            ShareInfoEmailCommand = new Command(async () => await ShareInfoEmail());
            ShareInfoSMSCommand = new Command(async () => await ShareInfoSMS());
        }
        #endregion

    }
}
