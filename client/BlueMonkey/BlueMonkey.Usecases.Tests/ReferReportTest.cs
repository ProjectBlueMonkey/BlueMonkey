using System.Threading.Tasks;
using BlueMonkey.ExpenseServices;
using Moq;
using Xunit;

namespace BlueMonkey.Usecases.Tests
{
    public class ReferReportTest
    {
        [Fact]
        public void Constructor()
        {
            var expenseService = new Mock<IExpenseService>();
            var actual = new ReferReport(expenseService.Object);

            Assert.NotNull(actual.ReportSummaries);
            Assert.Empty(actual.ReportSummaries);
        }

        [Fact]
        public async Task SearchAsync()
        {
            var expenseService = new Mock<IExpenseService>();
            var report = new ReportSummary();
            var reports = new[] { report };
            expenseService
                .Setup(m => m.GetReportSummariesAsync())
                .ReturnsAsync(reports);

            var actual = new ReferReport(expenseService.Object);
            await actual.SearchAsync();

            Assert.NotNull(actual.ReportSummaries);
            Assert.Single(actual.ReportSummaries);
            Assert.Equal(report, actual.ReportSummaries[0]);
        }
    }
}
