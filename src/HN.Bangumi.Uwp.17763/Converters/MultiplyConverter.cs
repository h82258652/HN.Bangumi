using System;
using Windows.UI.Xaml.Data;

namespace HN.Bangumi.Uwp.Converters
{
    public class MultiplyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var value1 = (double)System.Convert.ChangeType(value, TypeCode.Double);
            var value2 = (double)System.Convert.ChangeType(parameter, TypeCode.Double);
            return System.Convert.ChangeType(value1 * value2, targetType);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
