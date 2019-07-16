using System;
using System.Diagnostics;
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
            Debug.Assert(e.Parameter != null);
            var subjectId = (int)e.Parameter;

            _subjectId = subjectId;

            ViewModel.Load(subjectId);

            var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("SubjectForwardAnimation");
            if (animation != null)
            {
                var subjectImageUrl = animation.GetExtraData("SubjectImageUrl");
                if (subjectImageUrl != null)
                {
                    SubjectImage.Opacity = 0;

                    EventHandler handler = null;
                    handler = (sender, args) =>
                    {
                        SubjectImage.ImageOpened -= handler;

                        SubjectImage.Opacity = 1;
                        animation.TryStart(SubjectImage);
                    };
                    SubjectImage.ImageOpened += handler;

                    SubjectImage.Source = subjectImageUrl;
                }
                else
                {
                    animation.TryStart(SubjectImage);
                }
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back && _subjectId.HasValue)
            {
                var animation = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("SubjectBackAnimation", SubjectImage);
                animation.Configuration = new DirectConnectedAnimationConfiguration();
                animation.SetExtraData("SubjectId", _subjectId.Value);
            }
        }
    }
}
