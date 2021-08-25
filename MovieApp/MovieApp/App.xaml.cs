using MovieApp.Views;
using Xamarin.Forms;

using MovieApp.ViewModels;
using System.Threading.Tasks;

using MovieApp.Services;
using System.IO;
using System;

namespace MovieApp
{
    public partial class App : Application
    {

        #region Creating out database that we will use in ViewModel inside the folder of the current app 

        private static LocalDataBaseService dbContext;

        public static LocalDataBaseService DbContext
        {
            get 
            {
                //if it is null (when the app start up) then we will obtain reference to the file and then send it to our serivce to create the database in this file  
                //else means not empty then => just return it 
                if(dbContext == null)
                {
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FaviourtShowsDatabase-v01.db3");
                    dbContext = new LocalDataBaseService(dbPath);
                }
                return dbContext;
            }
        }

    

        #endregion



        public App()
        {
            InitializeComponent();
            
            MainPage = new HomeView();
            //MainPage = new NavigationPage(new HomeView());
        }

        #region OnStart method
        protected  override void OnStart()
        {
            
        }
        #endregion

        #region OnSleep method
        protected override void OnSleep()
        {

        }

        #endregion

        #region OnResume method
        protected override void OnResume()
        {

        }
        #endregion

    }
}
