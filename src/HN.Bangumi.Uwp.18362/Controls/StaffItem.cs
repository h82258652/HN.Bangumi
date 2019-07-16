using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HN.Bangumi.Uwp.Controls
{
    public sealed class StaffItem : Control
    {
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(nameof(ImageSource), typeof(object), typeof(StaffItem), new PropertyMetadata(default(object)));
        public static readonly DependencyProperty StaffNameProperty = DependencyProperty.Register(nameof(StaffName), typeof(string), typeof(StaffItem), new PropertyMetadata(default(string)));

        public StaffItem()
        {
            DefaultStyleKey = typeof(StaffItem);
        }

        public object ImageSource
        {
            get => GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public string StaffName
        {
            get => (string)GetValue(StaffNameProperty);
            set => SetValue(StaffNameProperty, value);
        }
    }
}
