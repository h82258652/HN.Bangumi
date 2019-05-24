using System;
using HN.Bangumi.Uwp.Extensions;
using HN.Bangumi.ViewModels;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace HN.Bangumi.Uwp.Views
{
    public sealed partial class SubjectView
    {
        private int? _subjectId;

        public SubjectView()
        {
            InitializeComponent();
        }

        public SubjectViewModel ViewModel => (SubjectViewModel)DataContext;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _subjectId = (int)e.Parameter;

            ViewModel.Load(_subjectId.Value);

            var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("SubjectForwardAnimation");
            if (animation != null)
            {
                SubjectImage.Opacity = 0;
                EventHandler handler = null;
                handler = delegate
                {
                    SubjectImage.ImageOpened -= handler;

                    SubjectImage.Opacity = 1;
                    animation.TryStart(SubjectImage);
                };
                SubjectImage.ImageOpened += handler;

                var subjectImageUrl = animation.GetExtraData("SubjectImageUrl");
                if (subjectImageUrl != null)
                {
                    SubjectImage.Source = subjectImageUrl;
                }
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back && _subjectId != null)
            {
                var animation = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("SubjectBackAnimation", SubjectImage);
                animation.Configuration = new DirectConnectedAnimationConfiguration();
                animation.SetExtraData("SubjectId", _subjectId.Value);
            }
        }
    }
}
