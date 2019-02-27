using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WinRTXamlToolkit.Controls.Extensions;

namespace HN.Bangumi.Uwp.Services
{
    public class AppNavigationService : INavigationService
    {
        public const string RootPageKey = "-- ROOT --";

        public const string UnknownPageKey = "-- UNKNOWN --";

        private readonly Dictionary<string, Type> _pagesByKey = new Dictionary<string, Type>();

        private Frame _currentFrame;

        public bool CanGoBack => CurrentFrame.CanGoBack;

        public bool CanGoForward => CurrentFrame.CanGoForward;

        public Frame CurrentFrame
        {
            get
            {
                return _currentFrame ?? (_currentFrame = Window.Current.Content.GetDescendantsOfType<Frame>().FirstOrDefault(temp => temp.Name == "ContentFrame"));
            }
            set
            {
                _currentFrame = value;
            }
        }

        public string CurrentPageKey
        {
            get
            {
                lock (_pagesByKey)
                {
                    if (CurrentFrame.BackStackDepth == 0)
                    {
                        return RootPageKey;
                    }

                    if (CurrentFrame.Content == null)
                    {
                        return UnknownPageKey;
                    }

                    var currentType = CurrentFrame.Content.GetType();

                    if (_pagesByKey.All(p => p.Value != currentType))
                    {
                        return UnknownPageKey;
                    }

                    var item = _pagesByKey.FirstOrDefault(i => i.Value == currentType);

                    return item.Key;
                }
            }
        }

        public void Configure(string key, Type pageType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(key))
                {
                    throw new ArgumentException("This key is already used: " + key);
                }

                if (_pagesByKey.Any(p => p.Value == pageType))
                {
                    throw new ArgumentException(
                        "This type is already configured with key " + _pagesByKey.First(p => p.Value == pageType).Key);
                }

                _pagesByKey.Add(
                    key,
                    pageType);
            }
        }

        public void GoBack()
        {
            if (CurrentFrame.CanGoBack)
            {
                CurrentFrame.GoBack();
            }
        }

        public void GoForward()
        {
            if (CurrentFrame.CanGoForward)
            {
                CurrentFrame.GoForward();
            }
        }

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        public virtual void NavigateTo(string pageKey, object parameter)
        {
            lock (_pagesByKey)
            {
                if (!_pagesByKey.ContainsKey(pageKey))
                {
                    throw new ArgumentException(
                        string.Format(
                            "No such page: {0}. Did you forget to call NavigationService.Configure?",
                            pageKey),
                        nameof(pageKey));
                }

                CurrentFrame.Navigate(_pagesByKey[pageKey], parameter);
            }
        }
    }
}
