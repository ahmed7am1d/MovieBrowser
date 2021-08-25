using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;


using MovieApp.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MovieApp.Services
{
    public static class MovieService
    {
        private readonly static string Key = "a6006918";

        #region Get movie by title  (Collection of movies matches the title ) Method
        public async static Task<List<Movie>> GetMovie(string SearchTitle)
        {
            List<Movie> movies = new List<Movie>();
            

            //1- Check Device has internet Connection 

            if(Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                //2- Make Connection to API 
                string URL = $"http://www.omdbapi.com/?s={SearchTitle}&apikey=a6006918";
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //3- we get data as a response message 
                HttpResponseMessage responseMessage = await client.GetAsync(URL);

                //4- If response message return Successed code then deserializ the json string to c# data
                if(responseMessage.IsSuccessStatusCode)
                {
                    string jsonString = await responseMessage.Content.ReadAsStringAsync();
                    //5- fill the data from deserialzation into our movie instance 
                    MovieInfo ListofMovies = JsonConvert.DeserializeObject<MovieInfo>(jsonString);
                    //this below will return a list of movies 
                    return ListofMovies.Search;
                }

            }

            return new List<Movie>();
        }

        #endregion

        //re use http client with base url

        #region Get movie by imdbID Method
        public static async Task<Movie> GetMovieByID(string imdbID)
        {
            Movie movie = new Movie();
            //1- Check Connection 
            if(Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                //2- Make Connection to API 
                string URL = $"http://www.omdbapi.com/?i={imdbID}&apikey=a6006918";

                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //3- we get data as a response message and we should check if return success code 
                HttpResponseMessage responseMessage = await httpClient.GetAsync(URL);
                if(responseMessage.IsSuccessStatusCode)
                {
                    //4- If response message return Successed code then deserializ the json string to c# data
                    string json = await responseMessage.Content.ReadAsStringAsync();
                    //5- fill the data from deserialzation into our movie instance  using json convert 
                    movie = JsonConvert.DeserializeObject<Movie>(json);

                    return movie;
                }


            }
            return new Movie();

        }


        #endregion


    }
}
