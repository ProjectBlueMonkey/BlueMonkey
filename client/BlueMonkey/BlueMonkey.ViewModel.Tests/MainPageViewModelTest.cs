using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.ViewModels;
using Moq;
using Prism.Navigation;
using Xunit;

namespace BlueMonkey.ViewModel.Tests
{
    public class MainPageViewModelTest
    {
        [Fact]
        public void NavigateCommand()
        {
            var navigationService = new Mock<INavigationService>();
            var actual = new MainPageViewModel(navigationService.Object);

            Assert.NotNull(actual.NavigateCommand);
            Assert.True(actual.NavigateCommand.CanExecute());

            actual.NavigateCommand.Execute("navigationPage");

            navigationService.Verify(m => m.NavigateAsync("navigationPage", null, null, true));
        }
    }
}
