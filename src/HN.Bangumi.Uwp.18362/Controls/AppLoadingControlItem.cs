using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace HN.Bangumi.Uwp.Controls
{
    [TemplatePart(Name = IconTemplateName, Type = typeof(BitmapIcon))]
    public sealed class AppLoadingControlItem : Control
    {
        private const string IconTemplateName = "PART_Icon";
        private static readonly Random Rand = new Random();
        private readonly TaskCompletionSource<object> _iconReadyTcs = new TaskCompletionSource<object>();
        private BitmapIcon _icon;

        public AppLoadingControlItem()
        {
            DefaultStyleKey = typeof(AppLoadingControlItem);
        }

        public async void PlayAnimation()
        {
            if (_icon == null)
            {
                await _iconReadyTcs.Task;
            }

            var i = Rand.Next(1, 101);
            var uri = new Uri($"ms-appx:///Assets/Images/smiles/{i:00}.gif");
            Debug.Assert(_icon != null);
            _icon.UriSource = uri;

            var storyboard = new Storyboard();
            {
                var animation = new DoubleAnimation
                {
                    From = 270,
                    To = 360,
                    Duration = TimeSpan.FromSeconds(1)
                };
                Storyboard.SetTarget(animation, _icon);
                Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(TransformGroup.Children)[0].(RotateTransform.Angle)");
                storyboard.Children.Add(animation);
            }
            {
                var animation = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(1)
                };
                Storyboard.SetTarget(animation, _icon);
                Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(TransformGroup.Children)[1].(ScaleTransform.ScaleX)");
                storyboard.Children.Add(animation);
            }
            {
                var animation = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(1)
                };
                Storyboard.SetTarget(animation, _icon);
                Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(TransformGroup.Children)[1].(ScaleTransform.ScaleY)");
                storyboard.Children.Add(animation);
            }
            storyboard.Begin();
        }

        protected override void OnApplyTemplate()
        {
            _icon = (BitmapIcon)GetTemplateChild(IconTemplateName);
            _iconReadyTcs.SetResult(null);
        }
    }
}
