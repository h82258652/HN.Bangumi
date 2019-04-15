using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace HN.Bangumi.Uwp.Converters
{
    public class IsNullOrEmptyStringConverter : IValueConverter
    {
        public bool IsInversed { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = string.IsNullOrEmpty((string)value);
            if (IsInversed)
            {
                return val ? Visibility.Collapsed : Visibility.Visible;
            }
            else
            {
                return val ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
