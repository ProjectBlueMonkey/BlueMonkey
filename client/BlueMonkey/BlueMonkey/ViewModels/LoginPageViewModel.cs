using BlueMonkey.LoginService;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Threading.Tasks;

namespace BlueMonkey.ViewModels
{
    public class LoginPageViewModel : BindableBase
    {
        private INavigationService _navigationService;
        private ILoginService _loginService;
        private IPageDialogService _pageDialogService;

        public DelegateCommand LoginCommand { get; }

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

            this.LoginCommand = new DelegateCommand(async () => await LoginAsync(), () => !IsBusy)
                .ObservesProperty(() => IsBusy);
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
