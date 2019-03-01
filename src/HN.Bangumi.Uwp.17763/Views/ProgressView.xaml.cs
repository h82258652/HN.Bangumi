using System.Linq;
using HN.Bangumi.ViewModels;
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
            var container = itemsControl.ContainerFromItem(e.ClickedItem);
            var image = container.GetDescendantsOfType<Image>().FirstOrDefault(temp => temp.Name == "SubjectImage");
            if (image != null)
            {
                var animation = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("SubjectForwardAnimation", image);
                animation.Configuration = new BasicConnectedAnimationConfiguration();
            }
        }
    }
}
