using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using WinRTXamlToolkit.AwaitableUI;

namespace HN.Bangumi.Uwp.Controls
{
    [TemplatePart(Name = OuterBorderTemplateName, Type = typeof(Border))]
    [TemplatePart(Name = InnerBorderTemplateName, Type = typeof(Border))]
    public class ToastPrompt : Control
    {
        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register(nameof(Duration), typeof(TimeSpan), typeof(ToastPrompt), new PropertyMetadata(default(TimeSpan)));
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(IconElement), typeof(ToastPrompt), new PropertyMetadata(default(IconElement)));
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(nameof(Message), typeof(string), typeof(ToastPrompt), new PropertyMetadata(default(string)));

        private const string InnerBorderTemplateName = "PART_InnerBorder";
        private const string OuterBorderTemplateName = "PART_OuterBorder";
        private BindableMargin _innerBorderMargin;
        private Border _outerBorder;

        public ToastPrompt()
        {
            DefaultStyleKey = typeof(ToastPrompt);
        }

        public TimeSpan Duration
        {
            get => (TimeSpan)GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }

        public IconElement Icon
        {
            get => (IconElement)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public async void Show()
        {
            await ShowAsync();
        }

        public async Task ShowAsync()
        {
            await this.WaitForNonZeroSizeAsync();

            var width = ActualWidth;

            var storyboard = new Storyboard();
            {
                var animation = new DoubleAnimationUsingKeyFrames();
                animation.KeyFrames.Add(new LinearDoubleKeyFrame
                {
                    KeyTime = TimeSpan.FromSeconds(0),
                    Value = 0
                });
                animation.KeyFrames.Add(new LinearDoubleKeyFrame
                {
                    KeyTime = TimeSpan.FromSeconds(0.5),
                    Value = 1
                });
                animation.KeyFrames.Add(new LinearDoubleKeyFrame
                {
                    KeyTime = Duration + TimeSpan.FromSeconds(0.5),
                    Value = 1
                });
                animation.KeyFrames.Add(new LinearDoubleKeyFrame
                {
                    KeyTime = Duration + TimeSpan.FromSeconds(1),
                    Value = 0
                });
                Storyboard.SetTarget(animation, _outerBorder);
                Storyboard.SetTargetProperty(animation, nameof(Opacity));
                storyboard.Children.Add(animation);
            }
            {
                var animation = new DoubleAnimationUsingKeyFrames
                {
                    EnableDependentAnimation = true
                };
                animation.KeyFrames.Add(new DiscreteDoubleKeyFrame
                {
                    KeyTime = TimeSpan.FromSeconds(0),
                    Value = 0 - width
                });
                animation.KeyFrames.Add(new EasingDoubleKeyFrame
                {
                    KeyTime = TimeSpan.FromSeconds(0.5),
                    Value = 0,
                    EasingFunction = new BackEase
                    {
                        EasingMode = EasingMode.EaseOut
                    }
                });
                animation.KeyFrames.Add(new DiscreteDoubleKeyFrame
                {
                    KeyTime = Duration + TimeSpan.FromSeconds(0.5),
                    Value = 0
                });
                animation.KeyFrames.Add(new EasingDoubleKeyFrame
                {
                    KeyTime = Duration + TimeSpan.FromSeconds(1),
                    Value = 0 - width,
                    EasingFunction = new BackEase
                    {
                        EasingMode = EasingMode.EaseIn
                    }
                });
                Storyboard.SetTarget(animation, _innerBorderMargin);
                Storyboard.SetTargetProperty(animation, nameof(BindableMargin.Right));
                storyboard.Children.Add(animation);
            }
            await storyboard.BeginAsync();
            _innerBorderMargin.Right = 0;
        }

        protected override void OnApplyTemplate()
        {
            _outerBorder = (Border)GetTemplateChild(OuterBorderTemplateName);
            var innerBorder = (FrameworkElement)GetTemplateChild(InnerBorderTemplateName);
            _innerBorderMargin = new BindableMargin(innerBorder);
        }
    }
}
