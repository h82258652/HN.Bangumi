using System;
using HN.Bangumi.API.Models;
using Windows.UI.Xaml.Data;

namespace HN.Bangumi.Uwp.Converters
{
    public class CharacterNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var character = value as Character;
            if (character == null)
            {
                return null;
            }

            return string.IsNullOrEmpty(character.NameCn) ? character.Name : character.NameCn;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
