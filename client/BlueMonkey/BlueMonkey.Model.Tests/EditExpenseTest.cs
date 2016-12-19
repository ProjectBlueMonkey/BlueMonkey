using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.Business;
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

        [Fact]
        public void DateProperty()
        {
            var expenseService = new Mock<IExpenseService>();
            var fileUploadService = new Mock<IFileUploadService>();
            var dateTimeService = new Mock<IDateTimeService>();
            var mediaService = new Mock<IMediaService>();
            var actual = new EditExpense(expenseService.Object, fileUploadService.Object, dateTimeService.Object, mediaService.Object);

            Assert.Equal(default(DateTime), actual.Date);
            Assert.PropertyChanged(actual, "Date", () => actual.Date = DateTime.MaxValue);
            Assert.Equal(DateTime.MaxValue, actual.Date);
        }

        [Fact]
        public void LocationProperty()
        {
            var expenseService = new Mock<IExpenseService>();
            var fileUploadService = new Mock<IFileUploadService>();
            var dateTimeService = new Mock<IDateTimeService>();
            var mediaService = new Mock<IMediaService>();
            var actual = new EditExpense(expenseService.Object, fileUploadService.Object, dateTimeService.Object, mediaService.Object);

            Assert.Null(actual.Location);
            Assert.PropertyChanged(actual, "Location", () => actual.Location = "update");
            Assert.Equal("update", actual.Location);
        }

        [Fact]
        public void NoteProperty()
        {
            var expenseService = new Mock<IExpenseService>();
            var fileUploadService = new Mock<IFileUploadService>();
            var dateTimeService = new Mock<IDateTimeService>();
            var mediaService = new Mock<IMediaService>();
            var actual = new EditExpense(expenseService.Object, fileUploadService.Object, dateTimeService.Object, mediaService.Object);

            Assert.Null(actual.Note);
            Assert.PropertyChanged(actual, "Note", () => actual.Note = "update");
            Assert.Equal("update", actual.Note);
        }

        [Fact]
        public void ReceiptProperty()
        {
            var expenseService = new Mock<IExpenseService>();
            var fileUploadService = new Mock<IFileUploadService>();
            var dateTimeService = new Mock<IDateTimeService>();
            var mediaService = new Mock<IMediaService>();
            var actual = new EditExpense(expenseService.Object, fileUploadService.Object, dateTimeService.Object, mediaService.Object);

            Assert.Null(actual.Receipt);
            var mediaFile = new Mock<IMediaFile>();
            Assert.PropertyChanged(actual, "Receipt", () => actual.Receipt = mediaFile.Object);
            Assert.Equal(mediaFile.Object, actual.Receipt);
        }

        [Fact]
        public void CategoriesProperty()
        {
            var expenseService = new Mock<IExpenseService>();
            var fileUploadService = new Mock<IFileUploadService>();
            var dateTimeService = new Mock<IDateTimeService>();
            var mediaService = new Mock<IMediaService>();
            var actual = new EditExpense(expenseService.Object, fileUploadService.Object, dateTimeService.Object, mediaService.Object);

            Assert.Null(actual.Categories);
            var categories = new Category[] { };
            Assert.PropertyChanged(actual, "Categories", () => actual.Categories = categories);
            Assert.Equal(categories, actual.Categories);
        }

        [Fact]
        public void SelectedCategoryProperty()
        {
            var expenseService = new Mock<IExpenseService>();
            var fileUploadService = new Mock<IFileUploadService>();
            var dateTimeService = new Mock<IDateTimeService>();
            var mediaService = new Mock<IMediaService>();
            var actual = new EditExpense(expenseService.Object, fileUploadService.Object, dateTimeService.Object, mediaService.Object);

            Assert.Null(actual.SelectedCategory);
            var category = new Category();
            Assert.PropertyChanged(actual, "SelectedCategory", () => actual.SelectedCategory = category);
            Assert.Equal(category, actual.SelectedCategory);
        }

        [Fact]
        public async Task InitializeAsync()
        {
            var expenseService = new Mock<IExpenseService>();
            var category1 = new Category();
            var category2 = new Category();
            var categories = new[] { category1, category2 };
            expenseService.Setup(m => m.GetCategoriesAsync()).ReturnsAsync(categories);

            var fileUploadService = new Mock<IFileUploadService>();
            var dateTimeService = new Mock<IDateTimeService>();
            dateTimeService.Setup(m => m.Today).Returns(DateTime.MaxValue);

            var mediaService = new Mock<IMediaService>();

            var actual = new EditExpense(expenseService.Object, fileUploadService.Object, dateTimeService.Object, mediaService.Object);
            actual.Name = "name";
            actual.Amount = 1;
            actual.Date = DateTime.MaxValue;
            actual.Location = "location";
            actual.Note = "note";
            actual.Receipt = new Mock<IMediaFile>().Object;
            actual.Categories = new Category[] { };
            actual.SelectedCategory = new Category();


            await actual.InitializeAsync();
            Assert.Null(actual.Name);
            Assert.Equal(0, actual.Amount);
            Assert.Equal(DateTime.MaxValue, actual.Date);
            Assert.Null(actual.Location);
            Assert.Null(actual.Note);
            Assert.Null(actual.Receipt);
            Assert.Equal(categories, actual.Categories);
            Assert.Equal(category1, actual.SelectedCategory);
        }

        [Fact]
        public async Task InitializeAsyncWhenNotExistCategories()
        {
            var expenseService = new Mock<IExpenseService>();
            var categories = new Category[] { };
            expenseService.Setup(m => m.GetCategoriesAsync()).ReturnsAsync(categories);

            var fileUploadService = new Mock<IFileUploadService>();
            var dateTimeService = new Mock<IDateTimeService>();
            dateTimeService.Setup(m => m.Today).Returns(DateTime.MaxValue);

            var mediaService = new Mock<IMediaService>();

            var actual = new EditExpense(expenseService.Object, fileUploadService.Object, dateTimeService.Object, mediaService.Object);

            await actual.InitializeAsync();
            Assert.Equal(DateTime.MaxValue, actual.Date);
            Assert.Equal(categories, actual.Categories);
            Assert.Null(actual.SelectedCategory);
        }

        [Fact]
        public async Task PickPhotoAsync()
        {
            var expenseService = new Mock<IExpenseService>();
            var fileUploadService = new Mock<IFileUploadService>();
            var dateTimeService = new Mock<IDateTimeService>();
            var mediaService = new Mock<IMediaService>();
            var actual = new EditExpense(expenseService.Object, fileUploadService.Object, dateTimeService.Object, mediaService.Object);

            await actual.PickPhotoAsync();
            mediaService.Verify(m => m.PickPhotoAsync(), Times.Once);
        }

        [Fact]
        public async Task TakePhotoAsync()
        {
            var expenseService = new Mock<IExpenseService>();
            var fileUploadService = new Mock<IFileUploadService>();
            var dateTimeService = new Mock<IDateTimeService>();
            var mediaService = new Mock<IMediaService>();
            var actual = new EditExpense(expenseService.Object, fileUploadService.Object, dateTimeService.Object, mediaService.Object);

            await actual.TakePhotoAsync();
            mediaService.Verify(m => m.TakePhotoAsync(), Times.Once);
        }

        [Fact]
        public async Task SaveAsyncWhenRegister()
        {
            var expenseService = new Mock<IExpenseService>();
            var fileUploadService = new Mock<IFileUploadService>();

            var dateTimeService = new Mock<IDateTimeService>();
            var mediaService = new Mock<IMediaService>();
            var actual = new EditExpense(expenseService.Object, fileUploadService.Object, dateTimeService.Object, mediaService.Object);

            actual.Name = "name";
            actual.Amount = 1;
            actual.Date = DateTime.MaxValue;
            actual.Location = "location";
            actual.Note = "note";
            var receipt = new Mock<IMediaFile>().Object;
            actual.Receipt = receipt;

            var uri = new Uri("https://googole.co.jp");
            fileUploadService.Setup(m => m.UploadMediaFileAsync(receipt)).ReturnsAsync(uri);

            expenseService
                .Setup(m => m.RegisterExpensesAsync(It.IsAny<Expense>(), It.IsAny<IEnumerable<ExpenseReceipt>>()))
                .Returns(Task.CompletedTask)
                .Callback<Expense, IEnumerable<ExpenseReceipt>>((expense, expenseReceipts) =>
                {
                    Assert.NotNull(expense);
                    Assert.Equal("name", actual.Name);
                    Assert.Equal(1, actual.Amount);
                    Assert.Equal(DateTime.MaxValue, actual.Date);
                    Assert.Equal("location", actual.Location);
                    Assert.Equal("note", actual.Note);
                    Assert.Equal("name", actual.Name);

                    Assert.NotNull(expenseReceipts);
                    Assert.Equal(1, expenseReceipts.Count());
                    var expenseReceipt = expenseReceipts.First();
                    Assert.Null(expenseReceipt.Id);
                    Assert.Null(expenseReceipt.ExpenseId);
                    Assert.Equal(uri.ToString(), expenseReceipt.ReceiptUri);
                    Assert.Null(expenseReceipt.UserId);
                });

            await actual.SaveAsync();

        }
    }
}
