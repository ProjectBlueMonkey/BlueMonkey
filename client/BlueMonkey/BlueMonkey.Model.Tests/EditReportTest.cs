using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
