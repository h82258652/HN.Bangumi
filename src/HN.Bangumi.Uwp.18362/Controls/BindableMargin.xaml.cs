using System;
using Windows.UI.Xaml;

namespace HN.Bangumi.Uwp.Controls
{
    public sealed partial class BindableMargin
    {
        public static readonly DependencyProperty BottomProperty = DependencyProperty.Register(nameof(Bottom), typeof(double), typeof(BindableMargin), new PropertyMetadata(default(double), OnBottomChanged));
        public static readonly DependencyProperty LeftProperty = DependencyProperty.Register(nameof(Left), typeof(double), typeof(BindableMargin), new PropertyMetadata(default(double), OnLeftChanged));
        public static readonly DependencyProperty RightProperty = DependencyProperty.Register(nameof(Right), typeof(double), typeof(BindableMargin), new PropertyMetadata(default(double), OnRightChanged));
        public static readonly DependencyProperty TopProperty = DependencyProperty.Register(nameof(Top), typeof(double), typeof(BindableMargin), new PropertyMetadata(default(double), OnTopChanged));

        private readonly FrameworkElement _owner;

        public BindableMargin(FrameworkElement owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException(nameof(owner));
            }

            _owner = owner;
            InitializeComponent();
        }

        public double Bottom
        {
            get
            {
                var ownerBottom = _owner.Margin.Bottom;
                var bottom = (double)GetValue(BottomProperty);
                if (!ownerBottom.Equals(bottom))
                {
                    Bottom = ownerBottom;
                }

                return ownerBottom;
            }
            set => SetValue(BottomProperty, value);
        }

        public double Left
        {
            get
            {
                var ownerLeft = _owner.Margin.Left;
                var left = (double)GetValue(LeftProperty);
                if (!ownerLeft.Equals(left))
                {
                    Left = ownerLeft;
                }

                return ownerLeft;
            }
            set => SetValue(LeftProperty, value);
        }

        public double Right
        {
            get
            {
                var ownerRight = _owner.Margin.Right;
                var right = (double)GetValue(RightProperty);
                if (!ownerRight.Equals(right))
                {
                    Right = ownerRight;
                }

                return ownerRight;
            }
            set => SetValue(RightProperty, value);
        }

        public double Top
        {
            get
            {
                var ownerTop = _owner.Margin.Top;
                var top = (double)GetValue(TopProperty);
                if (!ownerTop.Equals(top))
                {
                    Top = ownerTop;
                }

                return ownerTop;
            }
            set => SetValue(TopProperty, value);
        }

        private static void OnBottomChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (BindableMargin)d;
            var value = (double)e.NewValue;

            var owner = obj._owner;
            var margin = owner.Margin;
            margin.Bottom = value;
            owner.Margin = margin;
        }

        private static void OnLeftChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (BindableMargin)d;
            var value = (double)e.NewValue;

            var owner = obj._owner;
            var margin = owner.Margin;
            margin.Left = value;
            owner.Margin = margin;
        }

        private static void OnRightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (BindableMargin)d;
            var value = (double)e.NewValue;

            var owner = obj._owner;
            var margin = owner.Margin;
            margin.Right = value;
            owner.Margin = margin;
        }

        private static void OnTopChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (BindableMargin)d;
            var value = (double)e.NewValue;

            var owner = obj._owner;
            var margin = owner.Margin;
            margin.Top = value;
            owner.Margin = margin;
        }
    }
}
