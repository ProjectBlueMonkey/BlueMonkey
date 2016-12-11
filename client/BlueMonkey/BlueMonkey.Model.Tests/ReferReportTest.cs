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
    public class ReferReportTest
    {
        [Fact]
        public void Constructor()
        {
            var expenseService = new Mock<IExpenseService>();
            var actual = new ReferReport(expenseService.Object);

            Assert.NotNull(actual.Reports);
            Assert.Equal(0, actual.Reports.Count);
        }
    }
}
