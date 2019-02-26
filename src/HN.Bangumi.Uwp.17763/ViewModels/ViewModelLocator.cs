using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Views;
using HN.Bangumi.API;
using HN.Bangumi.Configuration;
using HN.Bangumi.Services;
using HN.Bangumi.Uwp.Services;
using HN.Bangumi.Uwp.Views;
using HN.Bangumi.ViewModels;

namespace HN.Bangumi.Uwp.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            var autofacServiceLocator = new AutofacServiceLocator(ConfigureAutofacContainer());
            ServiceLocator.SetLocatorProvider(() => autofacServiceLocator);
        }

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
                    .WithConfig(appConfiguration.AppKey, appConfiguration.AppSecret, appConfiguration.RedirectUri)
                    .UseDefaultAuthorizationProvider()
                    .UseDefaultAccessTokenStorage()
                    .Build();
            }).SingleInstance();

            containerBuilder.RegisterInstance(CreateNavigationService());
            containerBuilder.RegisterType<AppDialogService>().As<IAppDialogService>();
            containerBuilder.RegisterType<UserService>().As<IUserService>();
            containerBuilder.RegisterType<SubjectService>().As<ISubjectService>();
            containerBuilder.RegisterType<CalendarService>().As<ICalendarService>();

            containerBuilder.RegisterType<ShellViewModel>().SingleInstance();
            containerBuilder.RegisterType<SearchViewModel>();
            containerBuilder.RegisterType<ProgressViewModel>();
            containerBuilder.RegisterType<CalendarViewModel>();
            containerBuilder.RegisterType<SettingViewModel>();
            containerBuilder.RegisterType<SubjectViewModel>();

            return containerBuilder.Build();
        }

        public CalendarViewModel Calendar
        {
            get { return ServiceLocator.Current.GetInstance<CalendarViewModel>(); }
        }

        private static INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
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
