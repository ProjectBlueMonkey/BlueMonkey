using System;
using Prism.Mvvm;
using Prism.Navigation;
using Reactive.Bindings;

namespace BlueMonkey.ViewModels
{
    /// <summary>
    /// ViewModel for MainPage.
    /// </summary>
    public class MainPageViewModel : BindableBase
    {
        /// <summary>
        /// NavigationService
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// Command of navigate next pages.
        /// </summary>
        public ReactiveCommand<string> NavigateCommand { get; }

        /// <summary>
        /// Initialize instance.
        /// </summary>
        /// <param name="navigationService"></param>
        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            NavigateCommand = new ReactiveCommand<string>();
            NavigateCommand.Subscribe(Navigate);
        }

        /// <summary>
        /// Navigate next page.
        /// </summary>
        /// <param name="uri"></param>
        private void Navigate(string uri)
        {
            _navigationService.NavigateAsync(uri);
        }
    }
}
