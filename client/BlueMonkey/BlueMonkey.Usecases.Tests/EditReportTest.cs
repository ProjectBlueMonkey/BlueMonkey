using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMonkey.ExpenseServices;
using BlueMonkey.TimeService;
using Moq;
using Xunit;

namespace BlueMonkey.Usecases.Tests
{
    public class EditReportTest
    {
        [Fact]
        public void Constructor()
        {
            var expenseService = new Mock<IExpenseService>();
            var dateTimeService = new Mock<IDateTimeService>();
            var actual = new EditReport(expenseService.Object, dateTimeService.Object);

            Assert.Null(actual.Name);

            Assert.Equal(default(DateTime), actual.Date);

            Assert.NotNull(actual.SelectableExpenses);
            Assert.False(actual.SelectableExpenses.Any());
        }

        [Fact]
        public void NameProperty()
        {
            var expenseService = new Mock<IExpenseService>();
            var dateTimeService = new Mock<IDateTimeService>();
            var actual = new EditReport(expenseService.Object, dateTimeService.Object);

            Assert.PropertyChanged(actual, "Name", () => { actual.Name = "NewName"; });

            Assert.Equal("NewName", actual.Name);
        }

        [Fact]
        public void DateProperty()
        {
            var expenseService = new Mock<IExpenseService>();
            var dateTimeService = new Mock<IDateTimeService>();
            var actual = new EditReport(expenseService.Object, dateTimeService.Object);

            DateTime newDateTime = DateTime.MaxValue;
            Assert.PropertyChanged(actual, "Date", () => { actual.Date = newDateTime; });

            Assert.Equal(newDateTime, actual.Date);
        }

        [Fact]
        public async Task InitializeForNewReportAsync()
        {
            var expenseService = new Mock<IExpenseService>();
            var expense01 = new Expense { Id = "Expense01", Date = DateTime.MinValue + TimeSpan.FromDays(1) };
            var expense02 = new Expense { Id = "Expense02", Date = DateTime.MinValue + TimeSpan.FromDays(2) };
            var unregisteredExpenses = new [] { expense01, expense02 };
            expenseService
                .Setup(m => m.GetUnregisteredExpensesAsync())
                .ReturnsAsync(unregisteredExpenses);

            var dateTimeService = new Mock<IDateTimeService>();
            dateTimeService
                .Setup(m => m.Today)
                .Returns(DateTime.MaxValue);

            var actual = new EditReport(expenseService.Object, dateTimeService.Object);

            await actual.InitializeForNewReportAsync();

            Assert.Null(actual.Name);

            // The initial value is today.
            // Consider the case when it is the next day at the time of execution.
            Assert.Equal(DateTime.MaxValue, actual.Date);

            Assert.NotNull(actual.SelectableExpenses);
            Assert.Equal(2, actual.SelectableExpenses.Count);

            Assert.False(actual.SelectableExpenses[0].IsSelected);
            Assert.Equal((string) expense01.Id, actual.SelectableExpenses[0].Id);

            Assert.False(actual.SelectableExpenses[1].IsSelected);
            Assert.Equal((string) expense02.Id, actual.SelectableExpenses[1].Id);
        }

        [Fact]
        public async Task InitializeForNewReportAsyncWhenReportIdIsNull()
        {
            var expenseService = new Mock<IExpenseService>();
            var dateTimeService = new Mock<IDateTimeService>();
            var editReport = new EditReport(expenseService.Object, dateTimeService.Object);
            var actuial = await Assert.ThrowsAsync<ArgumentNullException>(() => editReport.InitializeForUpdateReportAsync(null));

            Assert.Equal("reportId", actuial.ParamName);
        }

        [Fact]
        public async Task InitializeForUpdateAsync()
        {
            var expenseService = new Mock<IExpenseService>();
            var expense01 = new Expense { Id = "Expense01", Date = DateTime.MinValue + TimeSpan.FromDays(4) };
            var expense02 = new Expense { Id = "Expense02", Date = DateTime.MinValue + TimeSpan.FromDays(3) };
            var expense03 = new Expense { Id = "Expense03", Date = DateTime.MinValue + TimeSpan.FromDays(2), ReportId = "report01" };
            var expenses = new[] { expense01, expense02 };
            expenseService
                .Setup(m => m.GetUnregisteredExpensesAsync())
                .ReturnsAsync(expenses);
            expenseService
                .Setup(m => m.GetExpensesFromReportIdAsync("report01"))
                .ReturnsAsync(new[] { expense03 });

            var reportDate = DateTime.Parse("2100/01/01");
            var report = new Report { Id = "report01", Name = "reportName", Date = reportDate };
            expenseService
                .Setup(m => m.GetReportAsync("report01"))
                .ReturnsAsync(report);

            var dateTimeService = new Mock<IDateTimeService>();
            dateTimeService
                .Setup(m => m.Today)
                .Returns(DateTime.MaxValue);

            var actual = new EditReport(expenseService.Object, dateTimeService.Object);

            await actual.InitializeForUpdateReportAsync("report01");

            Assert.Equal("reportName", actual.Name);

            // The initial value is today.
            // Consider the case when it is the next day at the time of execution.
            Assert.Equal(reportDate, actual.Date);

            Assert.NotNull(actual.SelectableExpenses);
            Assert.Equal(3, actual.SelectableExpenses.Count);

            Assert.True(actual.SelectableExpenses[0].IsSelected);
            Assert.Equal((string) expense03.Id, actual.SelectableExpenses[0].Id);

            Assert.False(actual.SelectableExpenses[1].IsSelected);
            Assert.Equal((string) expense02.Id, actual.SelectableExpenses[1].Id);

            Assert.False(actual.SelectableExpenses[2].IsSelected);
            Assert.Equal((string) expense01.Id, actual.SelectableExpenses[2].Id);
        }


        [Fact]
        public async Task SaveAsync()
        {
            var expenseService = new Mock<IExpenseService>();
            var expense01 = new Expense();
            var expense02 = new Expense();
            var expenses = new[] { expense01, expense02 };
            expenseService
                .Setup(m => m.GetUnregisteredExpensesAsync())
                .ReturnsAsync(expenses);

            var dateTimeService = new Mock<IDateTimeService>();
            var editReport = new EditReport(expenseService.Object, dateTimeService.Object);

            await editReport.InitializeForNewReportAsync();

            editReport.Name = "InputName";
            editReport.Date = DateTime.MinValue;
            editReport.SelectableExpenses[1].IsSelected = true;

            Report savedReport = null;
            IEnumerable<Expense> savedExpenses = null;
            expenseService
                .Setup(m => m.RegisterReportAsync(It.IsAny<Report>(), It.IsAny<IEnumerable<Expense>>()))
                .Callback<Report, IEnumerable<Expense>>((argReport, argExpenses) =>
                {
                    savedReport = argReport;
                    savedExpenses = argExpenses;
                })
                .Returns(Task.CompletedTask);
            
            await editReport.SaveAsync();

            Assert.NotNull(savedReport);
            Assert.Null(savedReport.Id);
            Assert.Equal("InputName", savedReport.Name);
            Assert.Equal(DateTime.MinValue, savedReport.Date);

            Assert.NotNull(savedExpenses);
            Assert.Single(savedExpenses);
            Assert.Equal(expense02.Id, savedExpenses.First().Id);
        }

        [Fact]
        public async Task SaceForUpdateAsync()
        {
            var expenseService = new Mock<IExpenseService>();
            var expense01 = new Expense { Id = "Expense01", Date = DateTime.MinValue + TimeSpan.FromDays(4) };
            var expense02 = new Expense { Id = "Expense02", Date = DateTime.MinValue + TimeSpan.FromDays(3) };
            var expenses = new[] { expense01, expense02 };
            expenseService
                .Setup(m => m.GetUnregisteredExpensesAsync())
                .ReturnsAsync(expenses);

            var expense03 = new Expense { Id = "Expense03", Date = DateTime.MinValue + TimeSpan.FromDays(2), ReportId = "report01" };
            expenseService
                .Setup(m => m.GetExpensesFromReportIdAsync("report01"))
                .ReturnsAsync(new[] { expense03 });

            var report = new Report { Id = "report01", Name = "reportName", Date = DateTime.MinValue, UserId = "user01" };
            expenseService
                .Setup(m => m.GetReportAsync("report01"))
                .ReturnsAsync(report);

            var dateTimeService = new Mock<IDateTimeService>();

            var editReport = new EditReport(expenseService.Object, dateTimeService.Object);

            await editReport.InitializeForUpdateReportAsync("report01");

            editReport.Name = "UpdatedName";
            editReport.Date = DateTime.MaxValue;
            editReport.SelectableExpenses[0].IsSelected = !editReport.SelectableExpenses[0].IsSelected;
            editReport.SelectableExpenses[1].IsSelected = !editReport.SelectableExpenses[1].IsSelected;
            editReport.SelectableExpenses[2].IsSelected = !editReport.SelectableExpenses[2].IsSelected;

            Report savedReport = null;
            List<Expense> savedExpenses = null;
            expenseService
                .Setup(m => m.RegisterReportAsync(It.IsAny<Report>(), It.IsAny<IEnumerable<Expense>>()))
                .Callback<Report, IEnumerable<Expense>>((argReport, argExpenses) =>
                {
                    savedReport = argReport;
                    savedExpenses = argExpenses.ToList();
                })
                .Returns(Task.CompletedTask);

            await editReport.SaveAsync();

            Assert.NotNull(savedReport);
            Assert.Equal("report01", savedReport.Id);
            Assert.Equal("UpdatedName", savedReport.Name);
            Assert.Equal(DateTime.MaxValue, savedReport.Date);
            Assert.Equal("user01", savedReport.UserId);

            Assert.NotNull(savedExpenses);
            Assert.Equal(2, savedExpenses.Count);
            Assert.Equal(expense02.Id, savedExpenses[0].Id);
            Assert.Equal(expense01.Id, savedExpenses[1].Id);
        }

    }
}
