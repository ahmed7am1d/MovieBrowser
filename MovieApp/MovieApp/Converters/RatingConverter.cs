using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MovieApp.Converters
{
    public class RatingConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal ratingScore = decimal.Parse(value.ToString().Remove(3));
            string starImageUrl = string.Empty;
            if (ratingScore == 10)
                starImageUrl =  "fivestar.png";
            if (ratingScore < 10 && ratingScore >= 8)
                starImageUrl =  "fourstar.png";
            if (ratingScore < 8 && ratingScore >= 6)
                starImageUrl =  "threestar.png";
            if (ratingScore < 6 && ratingScore >= 4)
                starImageUrl =  "twostar.png";
            if (ratingScore < 4 && ratingScore >= 2)
                starImageUrl  =  "onestar.png";

            return starImageUrl;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
