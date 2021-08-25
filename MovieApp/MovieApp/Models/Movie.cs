using System;
using System.Collections.Generic;
using System.Text;
using MovieApp.Models;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MovieApp.Models
{
    [Table("Movie")]
    public class Movie
    {
        #region Proprties 

        //primary key the increase automatically 
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Awards { get; set; }
        public string Poster { get; set; }
        public string Metascore { get; set; }
        public string imdbRating { get; set; }
        public string imdbVotes { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public List<MovieRating> Ratings { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string totalSeasons { get; set; }
        public string Response { get; set; }

        public string ShowDuration => Type == "movie" ? Runtime : Type =="game"?Runtime : (int.Parse(totalSeasons) > 1 ? totalSeasons + " Seasons" : totalSeasons + " Season" );

        #endregion



    }
}
