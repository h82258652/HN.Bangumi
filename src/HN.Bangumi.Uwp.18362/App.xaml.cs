using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AppCenter;
using Windows.ApplicationModel;
using Microsoft.AppCenter.Analytics;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Background;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using HN.Bangumi.Uwp.Views;

namespace HN.Bangumi.Uwp
{
    /// <summary>
    /// 提供特定于应用程序的行为，以补充默认的应用程序类。
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// 初始化单一实例应用程序对象。这是执行的创作代码的第一行，
        /// 已执行，逻辑上等同于 main() 或 WinMain()。
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.UnhandledException += App_UnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            AppCenter.Start("e7c4e864-a03a-412a-8bec-e3e45cb179ef", typeof(Analytics));
        }

        /// <summary>
        /// 在应用程序由最终用户正常启动时进行调用。
        /// 将在启动应用程序以打开特定文件等情况下使用。
        /// </summary>
        /// <param name="e">有关启动请求和过程的详细信息。</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // 不要在窗口已包含内容时重复应用程序初始化，
            // 只需确保窗口处于活动状态
            if (rootFrame == null)
            {
                // 创建要充当导航上下文的框架，并导航到第一页
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: 从之前挂起的应用程序加载状态
                }

                // 将框架放在当前窗口中
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // 当导航堆栈尚未还原时，导航到第一页，
                    // 并通过将所需信息作为导航参数传入来配置
                    // 参数
                    rootFrame.Navigate(typeof(ShellView), e.Arguments);
                }

                // 注册后台任务
                await RegisterBackgroundTask();

                // 确保当前窗口处于活动状态
                Window.Current.Activate();
            }
        }

        private async void App_UnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            var exception = e.Exception;
            var content = e.Message +
                          Environment.NewLine +
                          exception.Message + Environment.NewLine + Environment.NewLine +
                          exception.StackTrace;
            try
            {
                await new MessageDialog(content, "oh shit, 炸了").ShowAsync();
            }
            catch
            {
                // ignored
            }
        }

        private async void CurrentDomain_UnhandledException(object sender, System.UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception exception)
            {
                var content = exception.Message + Environment.NewLine + Environment.NewLine + exception.StackTrace;
                try
                {
                    await new MessageDialog(content, "oh shit, 炸了").ShowAsync();
                }
                catch
                {
                    // ignored
                }
            }
        }
        /// <summary>
        /// 导航到特定页失败时调用
        /// </summary>
        ///<param name="sender">导航失败的框架</param>
        ///<param name="e">有关导航失败的详细信息</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// 在将要挂起应用程序执行时调用。  在不知道应用程序
        /// 无需知道应用程序会被终止还是会恢复，
        /// 并让内存内容保持不变。
        /// </summary>
        /// <param name="sender">挂起的请求的源。</param>
        /// <param name="e">有关挂起请求的详细信息。</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: 保存应用程序状态并停止任何后台活动
            deferral.Complete();
        }

        private async Task RegisterBackgroundTask()
        {
            const string refreshTokenTaskName = "RefreshTokenTask";

            var backgroundTaskRegistration = BackgroundTaskRegistration.AllTasks.Values.OfType<BackgroundTaskRegistration>()
                .FirstOrDefault(temp => temp.Name == refreshTokenTaskName);

            if (backgroundTaskRegistration != null)
            {
                // 已经注册后台任务。
                return;
            }

            var access = await BackgroundExecutionManager.RequestAccessAsync();
            if (access != BackgroundAccessStatus.AlwaysAllowed)
            {
                // 没有权限。
                return;
            }

            var builder = new BackgroundTaskBuilder();
            // 仅在网络可用下执行后台任务。
            builder.AddCondition(new SystemCondition(SystemConditionType.InternetAvailable));

            builder.Name = refreshTokenTaskName;
            builder.TaskEntryPoint = "HN.Bangumi.Uwp.BackgroundTasks.RefreshTokenTask";
            builder.SetTrigger(new TimeTrigger(60, false));
            backgroundTaskRegistration = builder.Register();
        }
    }
}
