using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace HN.Bangumi.Uwp.Converters
{
    public class IsNullToVisibilityConverter : IValueConverter
    {
        public bool IsInversed { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (IsInversed)
            {
                return value == null ? Visibility.Collapsed : Visibility.Visible;
            }
            else
            {
                return value == null ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
