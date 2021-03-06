﻿using System;
using System.Net;
using HN.Bangumi.API.Models;
using Windows.UI.Xaml.Data;

namespace HN.Bangumi.Uwp.Converters
{
    public class SubjectNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var subject = value as Subject;
            if (subject == null)
            {
                return null;
            }

            var result = string.IsNullOrEmpty(subject.NameCn) ? subject.Name : subject.NameCn;
            return WebUtility.HtmlDecode(result);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
