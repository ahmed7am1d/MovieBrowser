using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using MovieApp.Models;

namespace MovieApp.Converters
{
    public class DuarationShowConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var CurrentShowType = value as String;
            var CurrentShow = new Movie();
            var CurrentDuration = String.Empty;


            if (CurrentShowType == "movie")
                CurrentDuration =  CurrentShow.Runtime;
            else if (CurrentShowType == "series")
                CurrentDuration =  CurrentShow.totalSeasons;

            return CurrentDuration;


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
