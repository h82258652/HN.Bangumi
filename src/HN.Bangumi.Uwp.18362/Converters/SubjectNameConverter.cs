using System;
using System.Net;
using HN.Bangumi.API.Models;
using Windows.UI.Xaml.Data;

namespace HN.Bangumi.Uwp.Converters
{
    public class SubjectNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Subject subject)
            {
                var result = string.IsNullOrEmpty(subject.NameCn) ? subject.Name : subject.NameCn;
                return WebUtility.HtmlDecode(result);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
