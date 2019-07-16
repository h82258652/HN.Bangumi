using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace HN.Bangumi.Uwp.Converters
{
    public class IsEqualsToVisibilityConverter : IValueConverter
    {
        public bool IsInversed { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (Equals(value, parameter))
            {
                return IsInversed ? Visibility.Collapsed : Visibility.Visible;
            }
            else
            {
                return IsInversed ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
