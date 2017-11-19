using System.Collections.ObjectModel;
using System.Reactive.Concurrency;
using BlueMonkey.Usecases;
using Moq;
using Prism.Navigation;
using Reactive.Bindings;
using Xunit;

namespace BlueMonkey.ViewModels.Tests
{
    public class ReportListPageViewModelTest
    {
        [Fact]
        public void Constructor()
        {
            var navigationService = new Mock<INavigationService>();
            var referReport = new Mock<IReferReport>();
            referReport
                .Setup(m => m.ReportSummaries)
                .Returns(new ReadOnlyObservableCollection<ReportSummary>(new ObservableCollection<ReportSummary>()));

            var actual = new ReportListPageViewModel(navigationService.Object, referReport.Object);

            Assert.NotNull(actual.ReportSummaries);
            Assert.Empty(actual.ReportSummaries);

            Assert.NotNull(actual.AddReportCommand);
            Assert.True(actual.AddReportCommand.CanExecute());
        }

        [Fact]
        public void ReportProperty()
        {
            ReactivePropertyScheduler.SetDefault(CurrentThreadScheduler.Instance);
            var navigationService = new Mock<INavigationService>();
            var referReport = new Mock<IReferReport>();
            var reportSummaries = new ObservableCollection<ReportSummary>();
            var readOnlyReportSummaries = new ReadOnlyObservableCollection<ReportSummary>(reportSummaries);
            referReport
                .Setup(m => m.ReportSummaries)
                .Returns(readOnlyReportSummaries);

            var actual = new ReportListPageViewModel(navigationService.Object, referReport.Object);

            Assert.Empty(actual.ReportSummaries);

            var reportSummary = new ReportSummary();
            reportSummaries.Add(reportSummary);

            Assert.Single(actual.ReportSummaries);
            Assert.Equal(reportSummary, actual.ReportSummaries[0]);
        }

        [Fact]
        public void AddReportCommand()
        {
            var navigationService = new Mock<INavigationService>();
            var referReport = new Mock<IReferReport>();
            referReport
                .Setup(m => m.ReportSummaries)
                .Returns(new ReadOnlyObservableCollection<ReportSummary>(new ObservableCollection<ReportSummary>()));

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

        [Fact]
        public void UpdateReportCommand()
        {
            var navigationService = new Mock<INavigationService>();
            var referReport = new Mock<IReferReport>();
            referReport
                .Setup(m => m.ReportSummaries)
                .Returns(new ReadOnlyObservableCollection<ReportSummary>(new ObservableCollection<ReportSummary>()));

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
                    Assert.Equal("reportId", parameter[ReportPageViewModel.ReportIdKey]);
                });
            actual.UpdateReportCommand.Execute(new Report { Id = "reportId" });

            Assert.True(calledNavigationAsync);

        }

        [Fact]
        public void OnNavigatedFrom()
        {
            var navigationService = new Mock<INavigationService>();
            var referReport = new Mock<IReferReport>();
            referReport
                .Setup(m => m.ReportSummaries)
                .Returns(new ReadOnlyObservableCollection<ReportSummary>(new ObservableCollection<ReportSummary>()));

            var actual = new ReportListPageViewModel(navigationService.Object, referReport.Object);

            // Make sure that no exceptions occur.
            actual.OnNavigatedFrom(null);
        }

        [Fact]
        public void OnNavigatedTo()
        {
            var navigationService = new Mock<INavigationService>();
            var referReport = new Mock<IReferReport>();
            referReport
                .Setup(m => m.ReportSummaries)
                .Returns(new ReadOnlyObservableCollection<ReportSummary>(new ObservableCollection<ReportSummary>()));

            var actual = new ReportListPageViewModel(navigationService.Object, referReport.Object);

            actual.OnNavigatedTo(null);

            referReport.Verify(m => m.SearchAsync(), Times.Once);
        }

        [Fact]
        public void OnNavigatingTo()
        {
            var navigationService = new Mock<INavigationService>();
            var referReport = new Mock<IReferReport>();
            referReport
                .Setup(m => m.ReportSummaries)
                .Returns(new ReadOnlyObservableCollection<ReportSummary>(new ObservableCollection<ReportSummary>()));

            var actual = new ReportListPageViewModel(navigationService.Object, referReport.Object);

            actual.OnNavigatingTo(null);
        }
    }
}
