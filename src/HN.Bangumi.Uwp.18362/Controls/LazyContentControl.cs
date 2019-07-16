using Windows.UI.Xaml.Controls;

namespace HN.Bangumi.Uwp.Controls
{
    public sealed class LazyContentControl : ContentControl
    {
        public LazyContentControl()
        {
            DefaultStyleKey = typeof(LazyContentControl);
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {

        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}
