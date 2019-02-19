﻿using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Views;
using HN.Bangumi.Configuration;
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

        public SettingViewModel Setting => ServiceLocator.Current.GetInstance<SettingViewModel>();

        private static IContainer ConfigureAutofacContainer()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<AppConfiguration>().As<IAppConfiguration>();
            containerBuilder.RegisterType<AppSettings>().As<IAppSettings>();

            containerBuilder.RegisterInstance(CreateNavigationService());
            containerBuilder.RegisterType<SettingViewModel>();

            return containerBuilder.Build();
        }

        private static INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure(ViewKeys.SettingViewKey, typeof(SettingView));
            return navigationService;
        }
    }
}
