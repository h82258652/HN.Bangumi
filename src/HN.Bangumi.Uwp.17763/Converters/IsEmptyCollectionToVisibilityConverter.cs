using System;
using System.Collections;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace HN.Bangumi.Uwp.Converters
{
    public class IsEmptyCollectionToVisibilityConverter : IValueConverter
    {
        public bool IsInversed { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var collection = (value as IEnumerable)?.Cast<object>();
            if (collection?.Any() == true)
            {
                return IsInversed ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                return IsInversed ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
