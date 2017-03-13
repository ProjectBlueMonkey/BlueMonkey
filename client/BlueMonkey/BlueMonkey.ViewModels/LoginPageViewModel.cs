using BlueMonkey.LoginService;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace BlueMonkey.ViewModels
{
    public class LoginPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly ILoginService _loginService;
        private readonly IPageDialogService _pageDialogService;

        public AsyncReactiveCommand LoginCommand { get; }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public LoginPageViewModel(INavigationService navigationService, ILoginService loginService, IPageDialogService pageDialogService)
        {
            _navigationService = navigationService;
            _loginService = loginService;
            _pageDialogService = pageDialogService;

            LoginCommand = this.ObserveProperty(m => m.IsBusy).Select(x => !x).ToAsyncReactiveCommand();
            LoginCommand.Subscribe(async _ => await LoginAsync());
        }

        private async Task LoginAsync()
        {
            try
            {
                IsBusy = true;
                if (!(await _loginService.LoginAsync()))
                {
                    await _pageDialogService.DisplayAlertAsync(
                        "Information",
                        "Login failed.",
                        "OK");
                    return;
                }
                await _navigationService.NavigateAsync("/NavigationPage/MainPage");
            }
            catch (Exception)
            {
                await _pageDialogService.DisplayAlertAsync(
                    "Information",
                    "Login failed.",
                    "OK");
            }
            finally
            {
                IsBusy = false;
            }

        }
    }
}
