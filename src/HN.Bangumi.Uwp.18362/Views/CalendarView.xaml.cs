using System;
using System.Linq;
using HN.Bangumi.API.Models;
using HN.Bangumi.Uwp.Extensions;
using HN.Bangumi.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace HN.Bangumi.Uwp.Views
{
    public sealed partial class CalendarView
    {
        public CalendarView()
        {
            InitializeComponent();
        }

        public CalendarViewModel ViewModel => (CalendarViewModel)DataContext;

        private void SubjectGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var itemsView = (ListViewBase)sender;
            var subject = (Subject)e.ClickedItem;
            var animation = itemsView.PrepareConnectedAnimation("SubjectForwardAnimation", subject, "SubjectImage");
            animation.Configuration = new BasicConnectedAnimationConfiguration();
            animation.SetExtraData("SubjectImageUrl", subject.Images?.Common);
        }

        private async void SubjectGridView_Loaded(object sender, RoutedEventArgs e)
        {
            var itemsView = (ListViewBase)sender;
            var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("SubjectBackAnimation");
            if (animation != null)
            {
                if (animation.GetExtraData("SubjectId") is int subjectId)
                {
                    var subject = ViewModel.Calendar?.SelectMany(temp => temp.Items).FirstOrDefault(temp => temp.Id == subjectId);
                    if (subject != null)
                    {
                        var calendar = (Calendar)itemsView.DataContext;
                        if (calendar?.Items?.Contains(subject) == true)
                        {
                            await itemsView.TryStartConnectedAnimationAsync(animation, subject, "SubjectImage");
                            return;
                        }
                    }
                }

                animation.Cancel();
            }
        }
    }
}
