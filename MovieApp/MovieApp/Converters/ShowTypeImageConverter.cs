using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Xamarin.Forms;

namespace MovieApp.Converters
{
    //1- inherit fromm IValueConverter interface 
    public class ShowTypeImageConverter : IValueConverter
    {
        //2- adding the method 
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if the sended value is "series" then return series picture 
            //if the sended value is "movie" then return movie picture
            var ShowType = value.ToString();
            var PictureUrl = String.Empty;
            if (ShowType == "movie")
                PictureUrl = "popcorn.png";
            else if (ShowType == "series")
                PictureUrl =  "tv.png";
            return PictureUrl;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
