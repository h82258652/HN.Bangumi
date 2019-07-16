using System;
using System.Text;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace HN.Bangumi.Uwp.Controls
{
    [TemplatePart(Name = OuterGridTemplateName, Type = typeof(Grid))]
    [TemplatePart(Name = BackProgressTextBlockTemplateName, Type = typeof(TextBlock))]
    [TemplatePart(Name = ProgressGridTemplateName, Type = typeof(Grid))]
    [TemplatePart(Name = FrontProgressTextBlockTemplateName, Type = typeof(TextBlock))]
    public class EpProgressControl : Control
    {
        public static readonly DependencyProperty BackTextBrushProperty = DependencyProperty.Register(nameof(BackTextBrush), typeof(Brush), typeof(EpProgressControl), new PropertyMetadata(default(Brush)));
        public static readonly DependencyProperty CurrentProperty = DependencyProperty.Register(nameof(Current), typeof(double), typeof(EpProgressControl), new PropertyMetadata(default(double), OnCurrentChanged));
        public static readonly DependencyProperty FrontTextBrushProperty = DependencyProperty.Register(nameof(FrontTextBrush), typeof(Brush), typeof(EpProgressControl), new PropertyMetadata(default(Brush)));
        public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register(nameof(TextAlignment), typeof(TextAlignment), typeof(EpProgressControl), new PropertyMetadata(default(TextAlignment)));
        public static readonly DependencyProperty TotalProperty = DependencyProperty.Register(nameof(Total), typeof(object), typeof(EpProgressControl), new PropertyMetadata(default(object), OnTotalChanged));

        private const string BackProgressTextBlockTemplateName = "PART_BackProgressTextBlock";
        private const string FrontProgressTextBlockTemplateName = "PART_FrontProgressTextBlock";
        private const string OuterGridTemplateName = "PART_OuterGrid";
        private const string ProgressGridTemplateName = "PART_ProgressGrid";

        private TextBlock _backProgressTextBlock;
        private TextBlock _frontProgressTextBlock;
        private Grid _outerGrid;
        private Grid _progressGrid;

        public EpProgressControl()
        {
            DefaultStyleKey = typeof(EpProgressControl);

            SizeChanged += EpProgressControl_SizeChanged;
        }

        public Brush BackTextBrush
        {
            get => (Brush)GetValue(BackTextBrushProperty);
            set => SetValue(BackTextBrushProperty, value);
        }

        public double Current
        {
            get => (double)GetValue(CurrentProperty);
            set => SetValue(CurrentProperty, value);
        }

        public Brush FrontTextBrush
        {
            get => (Brush)GetValue(FrontTextBrushProperty);
            set => SetValue(FrontTextBrushProperty, value);
        }

        public TextAlignment TextAlignment
        {
            get => (TextAlignment)GetValue(TextAlignmentProperty);
            set => SetValue(TextAlignmentProperty, value);
        }

        public double? Total
        {
            get
            {
                var value = GetValue(TotalProperty);
                if (value == null)
                {
                    return null;
                }

                return Convert.ToDouble(value);
            }
            set => SetValue(TotalProperty, value);
        }

        protected override void OnApplyTemplate()
        {
            _outerGrid = (Grid)GetTemplateChild(OuterGridTemplateName);
            _backProgressTextBlock = (TextBlock)GetTemplateChild(BackProgressTextBlockTemplateName);
            _progressGrid = (Grid)GetTemplateChild(ProgressGridTemplateName);
            _frontProgressTextBlock = (TextBlock)GetTemplateChild(FrontProgressTextBlockTemplateName);

            UpdateVisual();
        }

        private static void OnCurrentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (EpProgressControl)d;

            obj.UpdateVisual();
        }

        private static void OnTotalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (EpProgressControl)d;

            obj.UpdateVisual();
        }

        private void EpProgressControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            if (_outerGrid == null ||
                _backProgressTextBlock == null ||
                _progressGrid == null ||
                _frontProgressTextBlock == null)
            {
                return;
            }

            var current = Current;
            var total = Total;

            var textBuilder = new StringBuilder();
            textBuilder.Append(current);
            textBuilder.Append('/');
            if (total.HasValue)
            {
                textBuilder.Append(total.Value);
            }
            else
            {
                textBuilder.Append('?');
            }

            _backProgressTextBlock.Text = textBuilder.ToString();
            _frontProgressTextBlock.Text = textBuilder.ToString();

            if (total.HasValue)
            {
                var width = _outerGrid.ActualWidth * current / total.Value;
                _progressGrid.Clip = new RectangleGeometry
                {
                    Rect = new Rect(0, 0, width, _outerGrid.ActualHeight)
                };
            }
            else
            {
                _progressGrid.Clip = null;
            }
        }
    }
}
