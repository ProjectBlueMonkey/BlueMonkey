using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.Model;
using BlueMonkey.ViewModels;
using Moq;
using Prism.Navigation;
using Xunit;

namespace BlueMonkey.ViewModel.Tests
{
    public class ReportPageViewModelTest
    {
        [Fact]
        public void Constructor()
        {
            var navigationService = new Mock<INavigationService>();
            var editReport = new Mock<IEditReport>();
            editReport.Setup(m => m.Name).Returns("Name");
            editReport.Setup(m => m.Date).Returns(DateTime.Today);

            var actual = new ReportPageViewModel(navigationService.Object, editReport.Object);

            Assert.NotNull(actual.Name);
            Assert.Equal("Name", actual.Name.Value);

            Assert.NotNull(actual.Date);
            Assert.Equal(DateTime.Today, actual.Date.Value);

            Assert.Null(actual.Expenses);

            Assert.NotNull(actual.InitializeCommand);
            Assert.True(actual.InitializeCommand.CanExecute());

            Assert.NotNull(actual.NavigateExpenseSelectionCommand);
            Assert.True(actual.NavigateExpenseSelectionCommand.CanExecute());

            Assert.NotNull(actual.SaveReportCommand);
            Assert.True(actual.SaveReportCommand.CanExecute());
        }

        [Fact]
        public void NameProperty()
        {
            var navigationService = new Mock<INavigationService>();
            var editReport = new Mock<IEditReport>();

            var actual = new ReportPageViewModel(navigationService.Object, editReport.Object);

            Assert.Null(actual.Name.Value);

            // Update model.
            editReport.Setup(m => m.Name).Returns("Name");
            editReport
                .Raise(m => m.PropertyChanged += null, new PropertyChangedEventArgs("Name"));

            Assert.Equal("Name", actual.Name.Value);

            // Update ViewModel.
            actual.Name.Value = "Update name";
            editReport
                .VerifySet(m => m.Name = "Update name", Times.Once);
        }

        [Fact]
        public void DateProperty()
        {
            var navigationService = new Mock<INavigationService>();
            var editReport = new Mock<IEditReport>();

            var actual = new ReportPageViewModel(navigationService.Object, editReport.Object);

            Assert.Equal(default(DateTime), actual.Date.Value);

            // Update model.
            editReport.Setup(m => m.Date).Returns(DateTime.Today);
            editReport
                .Raise(m => m.PropertyChanged += null, new PropertyChangedEventArgs("Date"));

            Assert.Equal(DateTime.Today, actual.Date.Value);

            // Update ViewModel.
            actual.Date.Value = default(DateTime);
            editReport
                .VerifySet(m => m.Date = default(DateTime), Times.Exactly(2));
        }
    }
}
