using System;
using Xunit;

namespace BlueMonkey.Usecases.Tests
{
    public class SelectableExpenseTest
    {
        [Fact]
        public void ConstructorFromExpense()
        {
            var expected =
                new Expense
                {
                    Id = "Id",
                    CategoryId = "CategoryId",
                    Amount = 123456789012345,
                    Date = DateTime.MinValue,
                    Location = "Location",
                    Note = "Note",
                    ReportId = "ReportId",
                    UserId = "UserId"
                };

            var actual = new SelectableExpense(expected);

            Assert.Equal((string) expected.Id, actual.Id);
            Assert.Equal((string) expected.CategoryId, actual.CategoryId);
            Assert.Equal(expected.Amount, actual.Amount);
            Assert.Equal(expected.Date, actual.Date);
            Assert.Equal((string) expected.Location, actual.Location);
            Assert.Equal((string) expected.Note, actual.Note);
            Assert.Equal((string) expected.ReportId, actual.ReportId);
            Assert.Equal((string) expected.UserId, actual.UserId);
            Assert.False(actual.IsSelected);
        }
    }
}
