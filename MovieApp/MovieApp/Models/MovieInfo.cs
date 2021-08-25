using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApp.Models
{
    public class MovieInfo
    {
        #region Proprties 

        //remember the name of proprties must match 100% the name of json proprties
        //no metter if Lower OR Upper Case 
        public List<Movie> Search { get; set; }

        #endregion

    }
}
