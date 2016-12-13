using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.Business;
using BlueMonkey.ExpenceServices;
using BlueMonkey.TimeService;
using Moq;
using Xunit;

namespace BlueMonkey.Model.Tests
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
            var expense01 = new Expense();
            var expense02 = new Expense();
            var expenses = new [] { expense01, expense02 };
            expenseService
                .Setup(m => m.GetExpensesAsync())
                .ReturnsAsync(expenses);

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
            Assert.Equal(expense01.Id, actual.SelectableExpenses[0].Id);

            Assert.False(actual.SelectableExpenses[1].IsSelected);
            Assert.Equal(expense02.Id, actual.SelectableExpenses[1].Id);
        }

        [Fact]
        public async Task SaveAsync()
        {
            var expenseService = new Mock<IExpenseService>();
            var expense01 = new Expense();
            var expense02 = new Expense();
            var expenses = new[] { expense01, expense02 };
            expenseService
                .Setup(m => m.GetExpensesAsync())
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
                .Setup(m => m.RegisterReport(It.IsAny<Report>(), It.IsAny<IEnumerable<Expense>>()))
                .Callback<Report, IEnumerable<Expense>>((argReport, argExpenses) =>
                {
                    savedReport = argReport;
                    savedExpenses = argExpenses;
                })
                .Returns(Task.CompletedTask);
            
            await editReport.SaveAsync();

            Assert.NotNull(savedReport);
            Assert.Equal("InputName", editReport.Name);
            Assert.Equal(DateTime.MinValue, editReport.Date);

            Assert.NotNull(savedExpenses);
            Assert.Equal(1, savedExpenses.Count());
            Assert.Equal(expense02.Id, savedExpenses.First().Id);
        }
    }
}
