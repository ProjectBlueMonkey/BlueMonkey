using BlueMonkey.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueMonkey.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        INavigationService _navigationService;

        public DelegateCommand<string> NavigateCommand => new DelegateCommand<string>(Navigate);

        private void Navigate(string uri)
        {
            _navigationService.NavigateAsync(uri);
        }

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
