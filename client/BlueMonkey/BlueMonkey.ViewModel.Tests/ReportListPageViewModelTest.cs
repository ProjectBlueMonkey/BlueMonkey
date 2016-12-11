using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.Business;
using BlueMonkey.ExpenceServices;
using BlueMonkey.Model;
using BlueMonkey.ViewModels;
using Moq;
using Prism.Navigation;
using Xunit;

namespace BlueMonkey.ViewModel.Tests
{
    public class ReportListPageViewModelTest
    {
        [Fact]
        public void AddReportCommand()
        {
            var navigationService = new Mock<INavigationService>();
            var expenseService = new Mock<IExpenseService>();

            var actual = new ReportListPageViewModel(navigationService.Object, expenseService.Object);

            bool calledNavigationAsync = false;
            navigationService
                .Setup(m => m.NavigateAsync("ReportPage", It.IsAny<NavigationParameters>(), null, true))
                .Callback<string, NavigationParameters, bool?, bool>((name, parameter, useModalNavigation, animated) =>
                {
                    calledNavigationAsync = true;
                    Assert.NotNull(parameter);
                    Assert.Equal(1, parameter.Count);
                    Assert.True(parameter.ContainsKey(ReportPageViewModel.ReportIdKey));
                    Assert.Null(parameter[ReportPageViewModel.ReportIdKey]);
                });
            actual.AddReportCommand.Execute();

            Assert.True(calledNavigationAsync);
        }
    }
}
