using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.Business;
using BlueMonkey.ExpenceServices;
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
            var actual = new EditReport(expenseService.Object);

            Assert.Null(actual.Name);

            Assert.Equal(default(DateTime), actual.Date);

            Assert.NotNull(actual.SelectableExpenses);
            Assert.False(actual.SelectableExpenses.Any());
        }

        [Fact]
        public void NameProperty()
        {
            var expenseService = new Mock<IExpenseService>();
            var actual = new EditReport(expenseService.Object);

            Assert.PropertyChanged(actual, "Name", () => { actual.Name = "NewName"; });

            Assert.Equal("NewName", actual.Name);
        }

        [Fact]
        public void DateProperty()
        {
            var expenseService = new Mock<IExpenseService>();
            var actual = new EditReport(expenseService.Object);

            DateTime newDateTime = DateTime.Today + TimeSpan.FromDays(1);
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

            var actual = new EditReport(expenseService.Object);

            await actual.InitializeForNewReportAsync();

            Assert.Null(actual.Name);

            Assert.Equal(DateTime.Today, actual.Date);

            Assert.NotNull(actual.SelectableExpenses);
            Assert.Equal(2, actual.SelectableExpenses.Count);

            Assert.False(actual.SelectableExpenses[0].IsSelected);
            Assert.Equal(expense01.Id, actual.SelectableExpenses[0].Id);

            Assert.False(actual.SelectableExpenses[1].IsSelected);
            Assert.Equal(expense02.Id, actual.SelectableExpenses[1].Id);
        }

        [Fact]
        public async Task Save()
        {
            var expenseService = new Mock<IExpenseService>();
            var expense01 = new Expense();
            var expense02 = new Expense();
            var expenses = new[] { expense01, expense02 };
            expenseService
                .Setup(m => m.GetExpensesAsync())
                .ReturnsAsync(expenses);

            var editReport = new EditReport(expenseService.Object);

            await editReport.InitializeForNewReportAsync();

            editReport.Name = "InputName";
            var imputDate = DateTime.Today - TimeSpan.FromDays(2);
            editReport.Date = imputDate;
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
            
            await editReport.Save();

            Assert.NotNull(savedReport);
            Assert.Equal("InputName", editReport.Name);
            Assert.Equal(imputDate, editReport.Date);

            Assert.NotNull(savedExpenses);
            Assert.Equal(1, savedExpenses.Count());
            Assert.Equal(expense02.Id, savedExpenses.First().Id);
        }
    }
}
