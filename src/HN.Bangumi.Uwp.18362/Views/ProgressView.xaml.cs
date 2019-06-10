using System.Linq;
using HN.Bangumi.API.Models;
using HN.Bangumi.Uwp.Extensions;
using HN.Bangumi.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using WinRTXamlToolkit.Controls.Extensions;

namespace HN.Bangumi.Uwp.Views
{
    public sealed partial class ProgressView
    {
        public ProgressView()
        {
            InitializeComponent();
        }

        public ProgressViewModel ViewModel => (ProgressViewModel)DataContext;

        private void SubjectGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var itemsControl = (ItemsControl)sender;
            var collectionSubject = (CollectionSubject)e.ClickedItem;
            var container = itemsControl.ContainerFromItem(collectionSubject);
            var image = container.GetDescendantsOfType<FrameworkElement>().FirstOrDefault(temp => temp.Name == "SubjectImage");
            if (image != null)
            {
                var animation = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("SubjectForwardAnimation", image);
                animation.Configuration = new BasicConnectedAnimationConfiguration();
                animation.SetExtraData("SubjectImageUrl", collectionSubject.Subject.Images?.Common);
            }
        }

        private void SubjectGridView_Loaded(object sender, RoutedEventArgs e)
        {
            var itemsView = (ListViewBase)sender;
            var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("SubjectBackAnimation");
            if (animation != null)
            {
                if (animation.GetExtraData("SubjectId") is int subjectId)
                {
                    var collectionSubject = ViewModel.Collection?.FirstOrDefault(temp => temp.SubjectId == subjectId);
                    if (collectionSubject != null)
                    {
                        var container = itemsView.ContainerFromItem(collectionSubject);
                        var image = container.GetDescendantsOfType<FrameworkElement>().FirstOrDefault(temp => temp.Name == "SubjectImage");
                        if (image != null)
                        {
                            animation.TryStart(image);
                            return;
                        }
                    }
                }

                animation.Cancel();
            }
        }
    }
}
