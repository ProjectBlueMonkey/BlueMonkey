using System.Threading.Tasks;
using BlueMonkey.ExpenseServices;
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

        [Fact]
        public async Task SearchAsync()
        {
            var expenseService = new Mock<IExpenseService>();
            var report = new Report();
            var reports = new[] { report };
            expenseService
                .Setup(m => m.GetReportsAsync())
                .ReturnsAsync(reports);

            var actual = new ReferReport(expenseService.Object);
            await actual.SearchAsync();

            Assert.NotNull(actual.Reports);
            Assert.Equal(1, actual.Reports.Count);
            Assert.Equal(report, actual.Reports[0]);
        }
    }
}
