using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using MovieApp.Models;
using System.Linq;

namespace MovieApp.Services
{
    public class LocalDataBaseService
    {
        //making the connection that will be re-used 
        private readonly SQLiteAsyncConnection databaseConnection;

        #region Method for building the Table from the calss 
        //don't forget to execute it in the Constructor 
        private async Task CreateTables()
        {
            await databaseConnection.CreateTableAsync<Movie>();
            await databaseConnection.CreateTableAsync<MovieRating>();
        }
        #endregion

        #region Methods (Get items, Add item , delete item)
        //here basically we need only method for removing from db and adding to db

        //A- method that's return a list of all tables info(Columns) from db(generic)(movies, games, ....etc)
        public async Task<List<T>> GetItems<T>() where T : new()
        {
            return await databaseConnection.Table<T>().ToListAsync();
        }

        //B- method for adding an element or item or object to the database(generic element)
        //bool we want to know if the operation is successful by the help of the added number of rows as long as we adding one item (== 1)
        public async Task<bool> AddNewItem<T>(T item) where T : new()
        {
            int rows = await databaseConnection.InsertAsync(item);
            return rows == 1;

            //int rows = await databaseConnection.DeleteAllAsync<Movie>();
            //return rows >1;

            // int rows = await databaseConnection.DropTableAsync<Movie>();
            //_ = await databaseConnection.DropTableAsync<MovieRating>();

            //return rows >1;

        }

        //C- method for deleting an element or item or object from the database
        //remeber it reconginze it by looking for the id of the item
        public async Task<bool> DeleteItem<T>(T item) where T : new()
        {
            int rows = await databaseConnection.DeleteAsync(item);
            return rows == 1;
        }



        #endregion

        #region Method to drop all tables of specific class
        public async Task<bool> DropTables<T>() where T:new()
        {
            int a = await databaseConnection.DropTableAsync<T>();
            return a > 1;
        }
        #endregion

        #region Method to delete all objects of specific class
        public async Task<bool> DeleteObjects<T>() where T : new()
        {
            int a = await databaseConnection.DeleteAllAsync<T>();
            return a > 1;
        }
        #endregion

        #region Method to obtain only Movies
        public async Task<List<Movie>> GetMovies()
        {
            var allShows = await databaseConnection.Table<Movie>().ToListAsync();
            List<Movie> movies = allShows.Where(x => x.Type == "movie").ToList();
            return movies;
        }


        #endregion

        #region Method to only obtain  TVShows(Series)
        public async Task<List<Movie>> GetTVShows()
        {
            var allShows = await databaseConnection.Table<Movie>().ToListAsync();
            List<Movie> TVShows = allShows.Where(x => x.Type == "series").ToList();
            return TVShows;
        }
        #endregion

        #region Method to check if a movie is found in a database or not 

        public async Task<bool> IsExist(string movieTitle)
        {
            bool IsExistOrNot = false;
            var ts = await databaseConnection.Table<Movie>().ToListAsync();
            foreach (Movie m in ts)
            {
                if (m.Title == movieTitle)
                {
                    IsExistOrNot = true;
                    if (IsExistOrNot == true)
                        break;
                }
            }
            return IsExistOrNot;

        }
        #endregion

        #region Constructor 
        //the path will be sended to this constructor from the App.xaml.cs :) 
        public LocalDataBaseService(string dbPath)
        {
            databaseConnection = new SQLiteAsyncConnection(dbPath);
            Task.Run(() => CreateTables()).Wait();
        }
        #endregion
    }
}
