using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

namespace HN.Bangumi.Uwp.Controls
{
    [TemplatePart(Name = RootGridTemplateName, Type = typeof(Grid))]
    [TemplatePart(Name = WatchedButtonTemplateName, Type = typeof(Button))]
    [TemplatePart(Name = QueueButtonTemplateName, Type = typeof(Button))]
    [TemplatePart(Name = DropButtonTemplateName, Type = typeof(Button))]
    [TemplatePart(Name = RemoveButtonTemplateName, Type = typeof(Button))]
    [TemplateVisualState(GroupName = StatusStateGroupName, Name = WatchedStateName)]
    [TemplateVisualState(GroupName = StatusStateGroupName, Name = QueueStateName)]
    [TemplateVisualState(GroupName = StatusStateGroupName, Name = DropStateName)]
    [TemplateVisualState(GroupName = StatusStateGroupName, Name = AirStateName)]
    [TemplateVisualState(GroupName = StatusStateGroupName, Name = TodayStateName)]
    [TemplateVisualState(GroupName = StatusStateGroupName, Name = NAStateName)]
    public class EpItem2 : Control
    {
        public static readonly DependencyProperty AirDateProperty = DependencyProperty.Register(nameof(AirDate), typeof(string), typeof(EpItem2), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register(nameof(Duration), typeof(string), typeof(EpItem2), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty EpChineseNameProperty = DependencyProperty.Register(nameof(EpChineseName), typeof(string), typeof(EpItem2), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty EpNameProperty = DependencyProperty.Register(nameof(EpName), typeof(string), typeof(EpItem2), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty SortProperty = DependencyProperty.Register(nameof(Sort), typeof(float), typeof(EpItem2), new PropertyMetadata(default(float)));
        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register(nameof(Status), typeof(string), typeof(EpItem2), new PropertyMetadata(default(string), OnStatusChanged));
        public static readonly DependencyProperty UpdateStatusButtonVisibilityProperty = DependencyProperty.Register(nameof(UpdateStatusButtonVisibility), typeof(Visibility), typeof(EpItem2), new PropertyMetadata(default(Visibility)));

        private const string AirStateName = "Air";
        private const string DropButtonTemplateName = "PART_DropButton";
        private const string DropStateName = "Drop";
        private const string NAStateName = "NA";
        private const string QueueButtonTemplateName = "PART_QueueButton";
        private const string QueueStateName = "Queue";
        private const string RemoveButtonTemplateName = "PART_RemoveButton";
        private const string RootGridTemplateName = "PART_RootGrid";
        private const string StatusStateGroupName = "StatusStates";
        private const string TodayStateName = "Today";
        private const string WatchedButtonTemplateName = "PART_WatchedButton";
        private const string WatchedStateName = "Watched";
        private Grid _rootGrid;

        public EpItem2()
        {
            DefaultStyleKey = typeof(EpItem2);
        }

        public event EventHandler DropButtonClick;

        public event EventHandler QueueButtonClick;

        public event EventHandler RemoveButtonClick;

        public event EventHandler WatchedButtonClick;

        public string AirDate
        {
            get => (string)GetValue(AirDateProperty);
            set => SetValue(AirDateProperty, value);
        }

        public string Duration
        {
            get => (string)GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }

        public string EpChineseName
        {
            get => (string)GetValue(EpChineseNameProperty);
            set => SetValue(EpChineseNameProperty, value);
        }

        public string EpName
        {
            get => (string)GetValue(EpNameProperty);
            set => SetValue(EpNameProperty, value);
        }

        public float Sort
        {
            get => (float)GetValue(SortProperty);
            set => SetValue(SortProperty, value);
        }

        public string Status
        {
            get => (string)GetValue(StatusProperty);
            set => SetValue(StatusProperty, value);
        }

        public Visibility UpdateStatusButtonVisibility
        {
            get => (Visibility)GetValue(UpdateStatusButtonVisibilityProperty);
            set => SetValue(UpdateStatusButtonVisibilityProperty, value);
        }

        protected override void OnApplyTemplate()
        {
            _rootGrid = (Grid)GetTemplateChild(RootGridTemplateName);
            _rootGrid.Tapped += RootGrid_Tapped;
            var watchedButton = (Button)GetTemplateChild(WatchedButtonTemplateName);
            watchedButton.Click += WatchedButton_Click;
            var queueButton = (Button)GetTemplateChild(QueueButtonTemplateName);
            queueButton.Click += QueueButton_Click;
            var dropButton = (Button)GetTemplateChild(DropButtonTemplateName);
            dropButton.Click += DropButton_Click;
            var removeButton = (Button)GetTemplateChild(RemoveButtonTemplateName);
            removeButton.Click += RemoveButton_Click;

            UpdateVisual();
        }

        private static void OnStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (EpItem2)d;

            obj.UpdateVisual();
        }

        private void DropButton_Click(object sender, RoutedEventArgs e)
        {
            DropButtonClick?.Invoke(this, EventArgs.Empty);
        }

        private void QueueButton_Click(object sender, RoutedEventArgs e)
        {
            QueueButtonClick?.Invoke(this, EventArgs.Empty);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveButtonClick?.Invoke(this, EventArgs.Empty);
        }

        private void RootGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (FlyoutBase.GetAttachedFlyout(_rootGrid) != null)
            {
                FlyoutBase.ShowAttachedFlyout(_rootGrid);
            }
        }

        private void UpdateVisual()
        {
            var status = Status;
            if (string.Equals(status, "Watched", StringComparison.OrdinalIgnoreCase))
            {
                VisualStateManager.GoToState(this, WatchedStateName, true);
            }
            else if (string.Equals(status, "Queue", StringComparison.OrdinalIgnoreCase))
            {
                VisualStateManager.GoToState(this, QueueStateName, true);
            }
            else if (string.Equals(status, "Drop", StringComparison.OrdinalIgnoreCase))
            {
                VisualStateManager.GoToState(this, DropStateName, true);
            }
            else if (string.Equals(status, "Air", StringComparison.OrdinalIgnoreCase))
            {
                VisualStateManager.GoToState(this, AirStateName, true);
            }
            else if (string.Equals(status, "Today", StringComparison.OrdinalIgnoreCase))
            {
                VisualStateManager.GoToState(this, TodayStateName, true);
            }
            else if (string.Equals(status, "NA", StringComparison.OrdinalIgnoreCase))
            {
                VisualStateManager.GoToState(this, NAStateName, true);
            }
        }

        private void WatchedButton_Click(object sender, RoutedEventArgs e)
        {
            WatchedButtonClick?.Invoke(this, EventArgs.Empty);
        }
    }
}
