using System;
using HN.Bangumi.API.Models;
using Windows.UI.Xaml.Data;

namespace HN.Bangumi.Uwp.Converters
{
    public class StaffNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var staff = value as Staff;
            if (staff == null)
            {
                return null;
            }

            return string.IsNullOrEmpty(staff.NameCn) ? staff.Name : staff.NameCn;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
