using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.ExpenseServices;
using BlueMonkey.MediaServices;
using BlueMonkey.TimeService;
using Moq;
using Xunit;

namespace BlueMonkey.Model.Tests
{
    public class EditExpenseTest
    {
        [Fact]
        public void NameProperty()
        {
            var expenseService = new Mock<IExpenseService>();
            var fileUploadService = new Mock<IFileUploadService>();
            var dateTimeService = new Mock<IDateTimeService>();
            var mediaService = new Mock<IMediaService>();
            var actual = new EditExpense(expenseService.Object, fileUploadService.Object, dateTimeService.Object, mediaService.Object);

            Assert.Null(actual.Name);
            Assert.PropertyChanged(actual, "Name", () => actual.Name = "update");
            Assert.Equal("update", actual.Name);
        }

        [Fact]
        public void AmountProperty()
        {
            var expenseService = new Mock<IExpenseService>();
            var fileUploadService = new Mock<IFileUploadService>();
            var dateTimeService = new Mock<IDateTimeService>();
            var mediaService = new Mock<IMediaService>();
            var actual = new EditExpense(expenseService.Object, fileUploadService.Object, dateTimeService.Object, mediaService.Object);

            Assert.Equal(0, actual.Amount);
            Assert.PropertyChanged(actual, "Amount", () => actual.Amount = 1);
            Assert.Equal(1, actual.Amount);
        }
    }
}
