﻿using System.Diagnostics;
using System.Windows;
using HN.Bangumi.API;
using HN.Bangumi.Configuration;

namespace Tester
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            IAppConfiguration appConfiguration = new AppConfiguration();
            var client = new BangumiClientBuilder()
                .WithConfig(appConfiguration.AppKey, appConfiguration.AppSecret, appConfiguration.RedirectUri)
                .UseDefaultAuthorizationProvider()
                .UseDefaultAccessTokenStorage()
                .Build();

            await client.SignInAsync();

            var isSignIn = client.IsSignIn;

            var userId = client.UserId;

            Debugger.Break();
        }
    }
}