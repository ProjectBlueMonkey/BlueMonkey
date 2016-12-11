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
        public void Constructor()
        {
            var navigationService = new Mock<INavigationService>();
            var referReport = new Mock<IReferReport>();
            referReport
                .Setup(m => m.Reports)
                .Returns(new ReadOnlyObservableCollection<Report>(new ObservableCollection<Report>()));

            var actual = new ReportListPageViewModel(navigationService.Object, referReport.Object);

            Assert.NotNull(actual.Reports);
            Assert.Equal(0, actual.Reports.Count);

            Assert.NotNull(actual.AddReportCommand);
            Assert.True(actual.AddReportCommand.CanExecute());
        }

        [Fact]
        public void AddReportCommand()
        {
            var navigationService = new Mock<INavigationService>();
            var referReport = new Mock<IReferReport>();
            referReport
                .Setup(m => m.Reports)
                .Returns(new ReadOnlyObservableCollection<Report>(new ObservableCollection<Report>()));

            var actual = new ReportListPageViewModel(navigationService.Object, referReport.Object);

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
