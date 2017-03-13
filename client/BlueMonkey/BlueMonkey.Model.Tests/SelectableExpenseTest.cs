using System;
using Xunit;

namespace BlueMonkey.Model.Tests
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

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.CategoryId, actual.CategoryId);
            Assert.Equal(expected.Amount, actual.Amount);
            Assert.Equal(expected.Date, actual.Date);
            Assert.Equal(expected.Location, actual.Location);
            Assert.Equal(expected.Note, actual.Note);
            Assert.Equal(expected.ReportId, actual.ReportId);
            Assert.Equal(expected.UserId, actual.UserId);
            Assert.False(actual.IsSelected);
        }
    }
}
