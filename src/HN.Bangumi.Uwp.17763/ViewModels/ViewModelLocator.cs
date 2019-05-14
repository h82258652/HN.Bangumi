using System;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using HN.Bangumi.API;
using HN.Bangumi.Configuration;
using HN.Bangumi.Services;
using HN.Bangumi.Uwp.Services;
using HN.Bangumi.Uwp.Views;
using HN.Bangumi.ViewModels;
using HN.Cache;
using HN.Services;

namespace HN.Bangumi.Uwp.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ImageExService.ConfigureImageSource(options =>
            {
                options.WithDefaultServices();
                options.WithDefaultPipes();
                options.UseHttpHandler<BangumiHttpHandler>();
            });

            var autofacServiceLocator = new AutofacServiceLocator(ConfigureAutofacContainer());
            ServiceLocator.SetLocatorProvider(() => autofacServiceLocator);
        }

        public CalendarViewModel Calendar => ServiceLocator.Current.GetInstance<CalendarViewModel>();

        public ProgressViewModel Progress => ServiceLocator.Current.GetInstance<ProgressViewModel>();

        public SearchViewModel Search => ServiceLocator.Current.GetInstance<SearchViewModel>();

        public SettingViewModel Setting => ServiceLocator.Current.GetInstance<SettingViewModel>();

        public ShellViewModel Shell => ServiceLocator.Current.GetInstance<ShellViewModel>();

        public SubjectViewModel Subject => ServiceLocator.Current.GetInstance<SubjectViewModel>();

        private static IContainer ConfigureAutofacContainer()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<AppConfiguration>().As<IAppConfiguration>();
            containerBuilder.RegisterType<AppSettings>().As<IAppSettings>();

            containerBuilder.Register(context =>
            {
                var appConfiguration = context.Resolve<IAppConfiguration>();
                return new BangumiClientBuilder()
                    .WithConfig(options =>
                    {
                        options.AppKey = appConfiguration.AppKey;
                        options.AppSecret = appConfiguration.AppSecret;
                        options.RedirectUri = appConfiguration.RedirectUri;
                        options.RetryCount = 3;
                        options.RetryDelay = TimeSpan.FromSeconds(1);
                    })
                    .UseDefaultAuthorizationProvider()
                    .UseDefaultAccessTokenStorage()
                    .Build();
            }).SingleInstance();

            containerBuilder.RegisterInstance(CreateNavigationService());
            containerBuilder.RegisterType<AppDialogService>().As<IAppDialogService>();
            containerBuilder.RegisterType<AppToastService>().As<IAppToastService>();
            containerBuilder.RegisterType<UserService>().As<IUserService>();
            containerBuilder.RegisterType<SubjectService>().As<ISubjectService>();
            containerBuilder.RegisterType<CalendarService>().As<ICalendarService>();
            containerBuilder.RegisterType<DiskCache>().As<IDiskCache>();

            containerBuilder.Register(context => Messenger.Default).SingleInstance();

            containerBuilder.RegisterType<ShellViewModel>().SingleInstance();
            containerBuilder.RegisterType<SearchViewModel>();
            containerBuilder.RegisterType<ProgressViewModel>().SingleInstance();
            containerBuilder.RegisterType<CalendarViewModel>().SingleInstance();
            containerBuilder.RegisterType<SettingViewModel>();
            containerBuilder.RegisterType<SubjectViewModel>();

            return containerBuilder.Build();
        }

        private static INavigationService CreateNavigationService()
        {
            var navigationService = new AppNavigationService();
            navigationService.Configure(ViewKeys.ShellViewKey, typeof(ShellView));
            navigationService.Configure(ViewKeys.SearchViewKey, typeof(SearchView));
            navigationService.Configure(ViewKeys.ProgressViewKey, typeof(ProgressView));
            navigationService.Configure(ViewKeys.CalendarViewKey, typeof(CalendarView));
            navigationService.Configure(ViewKeys.SettingViewKey, typeof(SettingView));
            navigationService.Configure(ViewKeys.SubjectViewKey, typeof(SubjectView));
            return navigationService;
        }
    }
}
