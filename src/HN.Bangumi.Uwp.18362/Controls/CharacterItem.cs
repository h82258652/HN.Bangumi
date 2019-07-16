using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace HN.Bangumi.Uwp.Controls
{
    [TemplateVisualState(GroupName = RoleStateGroupName, Name = LeadStateName)]
    [TemplateVisualState(GroupName = RoleStateGroupName, Name = MinorStateName)]
    [TemplateVisualState(GroupName = RoleStateGroupName, Name = AsideStateName)]
    public sealed class CharacterItem : Control
    {
        public static readonly DependencyProperty CharacterNameProperty = DependencyProperty.Register(nameof(CharacterName), typeof(string), typeof(CharacterItem), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(nameof(ImageSource), typeof(object), typeof(CharacterItem), new PropertyMetadata(default(object)));
        public static readonly DependencyProperty RoleNameFontSizeProperty = DependencyProperty.Register(nameof(RoleNameFontSize), typeof(double), typeof(CharacterItem), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty RoleNameForegroundProperty = DependencyProperty.Register(nameof(RoleNameForeground), typeof(Brush), typeof(CharacterItem), new PropertyMetadata(default(Brush)));
        public static readonly DependencyProperty RoleNameProperty = DependencyProperty.Register(nameof(RoleName), typeof(string), typeof(CharacterItem), new PropertyMetadata(default(string), OnRoleNameChanged));

        private const string AsideStateName = "Aside";
        private const string LeadStateName = "Lead";
        private const string MinorStateName = "Minor";
        private const string RoleStateGroupName = "RoleStates";

        public CharacterItem()
        {
            DefaultStyleKey = typeof(CharacterItem);
        }

        public string CharacterName
        {
            get => (string)GetValue(CharacterNameProperty);
            set => SetValue(CharacterNameProperty, value);
        }

        public object ImageSource
        {
            get => GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public string RoleName
        {
            get => (string)GetValue(RoleNameProperty);
            set => SetValue(RoleNameProperty, value);
        }

        public double RoleNameFontSize
        {
            get => (double)GetValue(RoleNameFontSizeProperty);
            set => SetValue(RoleNameFontSizeProperty, value);
        }

        public Brush RoleNameForeground
        {
            get => (Brush)GetValue(RoleNameForegroundProperty);
            set => SetValue(RoleNameForegroundProperty, value);
        }

        protected override void OnApplyTemplate()
        {
            UpdateVisual();
        }

        private static void OnRoleNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (CharacterItem)d;

            obj.UpdateVisual();
        }

        private void UpdateVisual()
        {
            switch (RoleName)
            {
                case "主角":
                    VisualStateManager.GoToState(this, LeadStateName, true);
                    break;

                case "配角":
                    VisualStateManager.GoToState(this, MinorStateName, true);
                    break;

                case "客串":
                    VisualStateManager.GoToState(this, AsideStateName, true);
                    break;
            }
        }
    }
}
