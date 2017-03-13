using System.Threading.Tasks;
using BlueMonkey.ExpenseServices;
using Moq;
using Xunit;

namespace BlueMonkey.Usecases.Tests
{
    public class ReferExpenseTest
    {
        [Fact]
        public void Constructor()
        {
            var expenseService = new Mock<IExpenseService>();
            var actual = new ReferExpense(expenseService.Object);

            Assert.NotNull(actual.Expenses);
            Assert.Equal(0, actual.Expenses.Count);
        }

        [Fact]
        public async Task SearchAsync()
        {
            var expenseService = new Mock<IExpenseService>();
            var expense = new Expense();
            var expenses = new[] { expense };
            expenseService
                .Setup(m => m.GetExpensesAsync())
                .ReturnsAsync(expenses);

            var actual = new ReferExpense(expenseService.Object);
            await actual.SearchAsync();

            Assert.NotNull(actual.Expenses);
            Assert.Equal(1, actual.Expenses.Count);
            Assert.Equal(expense, actual.Expenses[0]);
        }

    }
}
