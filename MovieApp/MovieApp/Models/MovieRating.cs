
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MovieApp.Models
{
    [Table("MovieRating")]
    public class MovieRating
    {
        #region Proprties 
        [PrimaryKey,AutoIncrement]
        public int id { get; set; }

        [ForeignKey(typeof(Movie))]
        public int MovieId { get; set;}
        public string Source { get; set; }
        public string Value { get; set; }

        #endregion
    }
}
