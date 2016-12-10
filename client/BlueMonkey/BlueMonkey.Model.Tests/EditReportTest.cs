using System;
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
    }
}
