using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HN.Bangumi.Uwp.Controls
{
    [TemplatePart(Name = Item1TemplateName, Type = typeof(AppLoadingControlItem))]
    [TemplatePart(Name = Item2TemplateName, Type = typeof(AppLoadingControlItem))]
    [TemplatePart(Name = Item3TemplateName, Type = typeof(AppLoadingControlItem))]
    [TemplatePart(Name = Item4TemplateName, Type = typeof(AppLoadingControlItem))]
    [TemplateVisualState(GroupName = LoadingStateGroupName, Name = UnloadingStateName)]
    [TemplateVisualState(GroupName = LoadingStateGroupName, Name = LoadingStateName)]
    public sealed class AppLoadingControl : Control
    {
        public static readonly DependencyProperty IsLoadingProperty = DependencyProperty.Register(nameof(IsLoading), typeof(bool), typeof(AppLoadingControl), new PropertyMetadata(default(bool), OnIsLoadingChanged));

        private const string Item1TemplateName = "PART_Item1";
        private const string Item2TemplateName = "PART_Item2";
        private const string Item3TemplateName = "PART_Item3";
        private const string Item4TemplateName = "PART_Item4";
        private const string LoadingStateGroupName = "LoadingStates";
        private const string LoadingStateName = "Loading";
        private const string UnloadingStateName = "Unloading";

        private AppLoadingControlItem _item1;
        private AppLoadingControlItem _item2;
        private AppLoadingControlItem _item3;
        private AppLoadingControlItem _item4;
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
            _item1 = (AppLoadingControlItem)GetTemplateChild(Item1TemplateName);
            _item2 = (AppLoadingControlItem)GetTemplateChild(Item2TemplateName);
            _item3 = (AppLoadingControlItem)GetTemplateChild(Item3TemplateName);
            _item4 = (AppLoadingControlItem)GetTemplateChild(Item4TemplateName);

            UpdateVisual();
        }

        private static void OnIsLoadingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (AppLoadingControl)d;

            obj.UpdateVisual();
        }

        private void Timer_Tick(object sender, object e)
        {
            PlayAnimation();
        }

        private async void PlayAnimation()
        {
            _item1?.PlayAnimation();
            await Task.Delay(TimeSpan.FromSeconds(0.4));
            _item2?.PlayAnimation();
            await Task.Delay(TimeSpan.FromSeconds(0.4));
            _item3?.PlayAnimation();
            await Task.Delay(TimeSpan.FromSeconds(0.4));
            _item4?.PlayAnimation();
        }

        private void UpdateVisual()
        {
            if (_item1 == null ||
                _item2 == null ||
                _item3 == null ||
                _item4 == null)
            {
                return;
            }

            if (IsLoading)
            {
                _timer?.Stop();
                _timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(3)
                };
                _timer.Tick += Timer_Tick;
                _timer.Start();
                PlayAnimation();

                VisualStateManager.GoToState(this, LoadingStateName, true);
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
