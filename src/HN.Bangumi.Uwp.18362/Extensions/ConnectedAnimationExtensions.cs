using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace HN.Bangumi.Uwp.Extensions
{
    public class X : ThemeShadow
    {
        public X():base()
        {
        }
    }

    public static class ConnectedAnimationExtensions
    {
        private static readonly ConditionalWeakTable<ConnectedAnimation, Dictionary<string, object>> WeakTable = new ConditionalWeakTable<ConnectedAnimation, Dictionary<string, object>>();

        public static object GetExtraData(this ConnectedAnimation animation, string key)
        {
            if (animation == null)
            {
                throw new ArgumentNullException(nameof(animation));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (WeakTable.TryGetValue(animation, out var dictionary))
            {
                if (dictionary.TryGetValue(key, out var value))
                {
                    return value;
                }
            }

            return null;
        }

        public static void SetExtraData(this ConnectedAnimation animation, string key, object value)
        {
            if (animation == null)
            {
                throw new ArgumentNullException(nameof(animation));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var dictionary = WeakTable.GetOrCreateValue(animation);
            dictionary[key] = value;
        }
    }
}
