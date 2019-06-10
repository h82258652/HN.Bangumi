using System;
using Windows.UI.Xaml.Data;

namespace HN.Bangumi.Uwp.Converters
{
    public class FileSizeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var b = (long)value;
            if (b < 1024)
            {
                return $"{b}B";
            }

            var kb = b / 1024.0;
            if (kb < 1024)
            {
                return $"{kb:F2}KB";
            }

            var mb = kb / 1024.0;
            if (mb < 1024)
            {
                return $"{mb:F2}MB";
            }

            var gb = mb / 1024.0;
            if (gb < 1024)
            {
                return $"{gb:F2}GB";
            }

            var tb = gb / 1024.0;
            return $"{tb:F2}TB";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
