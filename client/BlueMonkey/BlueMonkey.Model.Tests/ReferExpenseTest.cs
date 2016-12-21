using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.ExpenseServices;
using Moq;
using Xunit;

namespace BlueMonkey.Model.Tests
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
    }
}
