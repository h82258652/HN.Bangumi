using HN.Bangumi.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace HN.Bangumi.Uwp.Views
{
    public sealed partial class SubjectView
    {


        public SubjectView()
        {
            InitializeComponent();
        }

        public SubjectViewModel ViewModel => (SubjectViewModel)DataContext;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var id = (int)e.Parameter;

            ViewModel.Load(id);

            var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("SubjectForwardAnimation");
            if (animation != null)
            {
                SubjectImage.Opacity = 0;
                RoutedEventHandler handler = null;
                handler = delegate
                {
                    SubjectImage.ImageOpened -= handler;

                    SubjectImage.Opacity = 1;
                    animation.TryStart(SubjectImage);
                };
                SubjectImage.ImageOpened += handler;
            }
        }

        private void SubjectImage_ImageOpened(object sender, RoutedEventArgs e)
        {
            var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("SubjectForwardAnimation");
            if (animation != null)
            {
                animation.TryStart(SubjectImage);
            }
        }
    }
}
