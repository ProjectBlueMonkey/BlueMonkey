using Moq;
using Prism.Navigation;
using Prism.Services;
using Xunit;

namespace BlueMonkey.ViewModels.Tests
{
    public class MainPageViewModelTest
    {
        [Fact]
        public void NavigateCommand()
        {
            var navigationService = new Mock<INavigationService>();
            var pageDialogService = new Mock<IPageDialogService>();
            var actual = new MainPageViewModel(navigationService.Object, pageDialogService.Object);

            Assert.NotNull(actual.NavigateCommand);
            Assert.True(actual.NavigateCommand.CanExecute());

            actual.NavigateCommand.Execute("navigationPage");

            navigationService.Verify(m => m.NavigateAsync("navigationPage", null, null, true));
        }
    }
}
