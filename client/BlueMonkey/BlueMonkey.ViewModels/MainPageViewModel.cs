#define DEBUG
using System;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
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
        /// The page dialog service.
        /// </summary>
        private readonly IPageDialogService _pageDialogService;

        /// <summary>
        /// Command of navigate next pages.
        /// </summary>
        public ReactiveCommand<string> NavigateCommand { get; }

        /// <summary>
        /// Initialize instance.
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="pageDialogService"></param>
        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;

            NavigateCommand = new ReactiveCommand<string>();
            NavigateCommand.Subscribe(Navigate);
        }

        /// <summary>
        /// Navigate next page.
        /// </summary>
        /// <param name="uri"></param>
        private async void Navigate(string uri)
        {
#if Azure
            if (Secrets.ServerUri.Contains("your"))
            {
                await _pageDialogService.DisplayAlertAsync(
                    "Invalid Server Uri", 
                    "You should write actual ServerUri and ConnectionString to Secrets.cs",
                    "Close");
                return;
            }
#endif

            await _navigationService.NavigateAsync(uri);
        }
    }
}
