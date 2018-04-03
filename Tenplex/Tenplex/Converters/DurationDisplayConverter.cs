using System;
using Windows.UI.Xaml.Data;

namespace Tenplex.Converters
{
    public class DurationDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var time = (TimeSpan)value;

            if (time.Hours > 0)
                return time.ToString("hh':'mm':'ss");
            else
                return time.ToString("mm':'ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
