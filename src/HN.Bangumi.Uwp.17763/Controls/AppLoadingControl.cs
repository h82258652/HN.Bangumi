using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HN.Bangumi.Uwp.Controls
{
    [TemplatePart(Name = Icon1TemplateName, Type = typeof(BitmapIcon))]
    [TemplatePart(Name = Icon2TemplateName, Type = typeof(BitmapIcon))]
    [TemplatePart(Name = Icon3TemplateName, Type = typeof(BitmapIcon))]
    [TemplatePart(Name = Icon4TemplateName, Type = typeof(BitmapIcon))]
    [TemplateVisualState(GroupName = LoadingStateGroupName, Name = UnloadingStateName)]
    [TemplateVisualState(GroupName = LoadingStateGroupName, Name = LoadingStateName)]
    public class AppLoadingControl : Control
    {
        public static readonly DependencyProperty IsLoadingProperty = DependencyProperty.Register(nameof(IsLoading), typeof(bool), typeof(AppLoadingControl), new PropertyMetadata(default(bool), OnIsLoadingChanged));

        private const string Icon1TemplateName = "PART_Icon1";
        private const string Icon2TemplateName = "PART_Icon2";
        private const string Icon3TemplateName = "PART_Icon3";
        private const string Icon4TemplateName = "PART_Icon4";
        private const string LoadingStateGroupName = "LoadingStates";
        private const string LoadingStateName = "Loading";
        private const string UnloadingStateName = "Unloading";
        private static readonly Random Rand = new Random();
        private BitmapIcon _icon1;
        private BitmapIcon _icon2;
        private BitmapIcon _icon3;
        private BitmapIcon _icon4;
        private DispatcherTimer _timer;

        public AppLoadingControl()
        {
            DefaultStyleKey = typeof(AppLoadingControl);
        }

        public bool IsLoading
        {
            get => (bool)GetValue(IsLoadingProperty);
            set => SetValue(IsLoadingProperty, value);
        }

        protected override void OnApplyTemplate()
        {
            _icon1 = (BitmapIcon)GetTemplateChild(Icon1TemplateName);
            _icon2 = (BitmapIcon)GetTemplateChild(Icon2TemplateName);
            _icon3 = (BitmapIcon)GetTemplateChild(Icon3TemplateName);
            _icon4 = (BitmapIcon)GetTemplateChild(Icon4TemplateName);

            UpdateVisual();
        }

        private static void OnIsLoadingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (AppLoadingControl)d;

            obj.UpdateVisual();
        }

        private static void RefreshIcon(BitmapIcon icon)
        {
            if (icon == null)
            {
                return;
            }

            var i = Rand.Next(1, 101);
            var uri = new Uri($"ms-appx:///Assets/Images/smiles/{i:00}.gif");
            icon.UriSource = uri;
        }

        private async Task RefreshIcon()
        {
            RefreshIcon(_icon1);
            await Task.Delay(TimeSpan.FromSeconds(0.4));
            RefreshIcon(_icon2);
            await Task.Delay(TimeSpan.FromSeconds(0.4));
            RefreshIcon(_icon3);
            await Task.Delay(TimeSpan.FromSeconds(0.4));
            RefreshIcon(_icon4);
        }

        private async void Timer_Tick(object sender, object e)
        {
            await RefreshIcon();
        }

        private async void UpdateVisual()
        {
            if (IsLoading)
            {
                _timer = new DispatcherTimer()
                {
                    Interval = TimeSpan.FromSeconds(3)
                };
                _timer.Tick += Timer_Tick;
                _timer.Start();
                VisualStateManager.GoToState(this, LoadingStateName, true);
                await RefreshIcon();
            }
            else
            {
                _timer?.Stop();
                _timer = null;
                VisualStateManager.GoToState(this, UnloadingStateName, true);
            }
        }
    }
}
