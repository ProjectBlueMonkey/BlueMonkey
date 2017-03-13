using System;
using System.Threading.Tasks;
using BlueMonkey.LoginService;
using Moq;
using Prism.Navigation;
using Prism.Services;
using Xunit;

namespace BlueMonkey.ViewModels.Tests
{
    public class LoginPageViewModelTest
    {
        [Fact]
        public void LoginSuccessCase()
        {
            var navigationService = new Mock<INavigationService>();
            var loginService = new Mock<ILoginService>();
            var pageDialogService = new Mock<IPageDialogService>();

            var actual = new LoginPageViewModel(navigationService.Object, loginService.Object, pageDialogService.Object);

            loginService.Setup(x => x.LoginAsync())
                .Returns(() => Task.FromResult(true));

            Assert.False(actual.IsBusy);
            Assert.PropertyChanged(actual, nameof(LoginPageViewModel.IsBusy), () => 
            {
                actual.LoginCommand.Execute();
            });
            Assert.False(actual.IsBusy);

            loginService.Verify(x => x.LoginAsync(), Times.Once());
            navigationService.Verify(x => x.NavigateAsync("/NavigationPage/MainPage", null, null, true), Times.Once());
        }

        [Fact]
        public void LoginFailCase()
        {
            var navigationService = new Mock<INavigationService>();
            var loginService = new Mock<ILoginService>();
            var pageDialogService = new Mock<IPageDialogService>();

            var actual = new LoginPageViewModel(navigationService.Object, loginService.Object, pageDialogService.Object);

            loginService.Setup(x => x.LoginAsync())
                .Returns(() => Task.FromResult(false));

            Assert.False(actual.IsBusy);
            Assert.PropertyChanged(actual, nameof(LoginPageViewModel.IsBusy), () =>
            {
                actual.LoginCommand.Execute();
            });
            Assert.False(actual.IsBusy);

            loginService.Verify(x => x.LoginAsync(), Times.Once());
            pageDialogService.Verify(x => x.DisplayAlertAsync("Information", "Login failed.", "OK"), Times.Once());
        }

        [Fact]
        public void LoginFailExceptionCase()
        {
            var navigationService = new Mock<INavigationService>();
            var loginService = new Mock<ILoginService>();
            var pageDialogService = new Mock<IPageDialogService>();

            var actual = new LoginPageViewModel(navigationService.Object, loginService.Object, pageDialogService.Object);

            loginService.Setup(x => x.LoginAsync())
                .Throws(new InvalidOperationException());

            Assert.False(actual.IsBusy);
            Assert.PropertyChanged(actual, nameof(LoginPageViewModel.IsBusy), () =>
            {
                actual.LoginCommand.Execute();
            });
            Assert.False(actual.IsBusy);

            loginService.Verify(x => x.LoginAsync(), Times.Once());
            pageDialogService.Verify(x => x.DisplayAlertAsync("Information", "Login failed.", "OK"), Times.Once());
        }
    }
}
