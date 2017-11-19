using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMonkey.ExpenseServices;
using BlueMonkey.MediaServices;
using BlueMonkey.TimeService;
using Moq;
using Xunit;

namespace BlueMonkey.Usecases.Tests
{
    public class EditExpenseTest
    {
        [Fact]
        public void AmountProperty()
        {
            var expenseService = new Mock<IExpenseService>();
            var fileUploadService = new Mock<IFileStorageService>();
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
            var fileUploadService = new Mock<IFileStorageService>();
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
            var fileUploadService = new Mock<IFileStorageService>();
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
            var fileUploadService = new Mock<IFileStorageService>();
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
            var fileUploadService = new Mock<IFileStorageService>();
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
            var fileUploadService = new Mock<IFileStorageService>();
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
            var fileUploadService = new Mock<IFileStorageService>();
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

            var fileUploadService = new Mock<IFileStorageService>();
            var dateTimeService = new Mock<IDateTimeService>();
            dateTimeService.Setup(m => m.Today).Returns(DateTime.MaxValue);

            var mediaService = new Mock<IMediaService>();

            var actual = new EditExpense(expenseService.Object, fileUploadService.Object, dateTimeService.Object, mediaService.Object);
            actual.Date = DateTime.MaxValue;
            actual.Categories = new Category[] { };
            actual.SelectedCategory = new Category();


            await actual.InitializeAsync();
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

            var fileUploadService = new Mock<IFileStorageService>();
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
        public async Task InitializeAsyncForUpdate()
        {
            var expenseService = new Mock<IExpenseService>();

            expenseService
                .Setup(m => m.GetExpenseAsync("expenseId"))
                .ReturnsAsync(new Expense
                {
                    Amount = 1,
                    Date = DateTime.MaxValue,
                    CategoryId = "category2",
                    Id = "expenseId",
                    Location = "location",
                    Note = "note"
                });

            var category1 = new Category { Id = "category1" };
            var category2 = new Category { Id = "category2" };
            var categories = new[] { category1, category2 };
            expenseService.Setup(m => m.GetCategoriesAsync()).ReturnsAsync(categories);

            var expenseReceipts =
                new[]
                {
                    new ExpenseReceipt
                    {
                        ExpenseId = "expenseId",
                        ReceiptUri = "https://www.bing.com/"
                    }
                };
            expenseService
                .Setup(m => m.GetExpenseReceiptsAsync("expenseId"))
                .ReturnsAsync(expenseReceipts.AsEnumerable());

            var fileStorageService = new Mock<IFileStorageService>();
            var mediaFile = new MediaFile(".jpg", new byte[] {});
            fileStorageService
                .Setup(m => m.DownloadMediaFileAsync(new Uri("https://www.bing.com/")))
                .ReturnsAsync(mediaFile);


            var dateTimeService = new Mock<IDateTimeService>();

            var mediaService = new Mock<IMediaService>();

            var actual = new EditExpense(expenseService.Object, fileStorageService.Object, dateTimeService.Object, mediaService.Object);
            actual.Date = DateTime.MaxValue;
            actual.Categories = new Category[] { };
            actual.SelectedCategory = new Category();


            await actual.InitializeAsync("expenseId");
            Assert.Equal(1, actual.Amount);
            Assert.Equal(DateTime.MaxValue, actual.Date);
            Assert.Equal("location", actual.Location);
            Assert.Equal("note", actual.Note);
            Assert.Equal(mediaFile, actual.Receipt);
            Assert.Equal(categories, actual.Categories);
            Assert.Equal(category2, actual.SelectedCategory);
        }


        [Fact]
        public async Task PickPhotoAsync()
        {
            var expenseService = new Mock<IExpenseService>();
            var fileUploadService = new Mock<IFileStorageService>();
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
            var fileUploadService = new Mock<IFileStorageService>();
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
            var fileUploadService = new Mock<IFileStorageService>();

            var dateTimeService = new Mock<IDateTimeService>();
            var mediaService = new Mock<IMediaService>();
            var actual = new EditExpense(expenseService.Object, fileUploadService.Object, dateTimeService.Object, mediaService.Object);

            actual.Amount = 1;
            actual.Date = DateTime.MaxValue;
            actual.Location = "location";
            actual.Note = "note";
            var receipt = new Mock<IMediaFile>().Object;
            actual.Receipt = receipt;
            actual.SelectedCategory = new Category { Id = "categoryId" };

            var uri = new Uri("https://www.bing.com/");
            fileUploadService.Setup(m => m.UploadMediaFileAsync(receipt)).ReturnsAsync(uri);

            expenseService
                .Setup(m => m.RegisterExpensesAsync(It.IsAny<Expense>(), It.IsAny<IEnumerable<ExpenseReceipt>>()))
                .Returns(Task.CompletedTask)
                .Callback<Expense, IEnumerable<ExpenseReceipt>>((expense, expenseReceipts) =>
                {
                    Assert.NotNull(expense);
                    Assert.Equal(1, expense.Amount);
                    Assert.Equal(DateTime.MaxValue, expense.Date);
                    Assert.Equal("location", expense.Location);
                    Assert.Equal("note", expense.Note);
                    Assert.Equal("categoryId", expense.CategoryId);

                    Assert.NotNull(expenseReceipts);
                    Assert.Single(expenseReceipts);
                    var expenseReceipt = expenseReceipts.First();
                    Assert.Null(expenseReceipt.Id);
                    Assert.Null(expenseReceipt.ExpenseId);
                    Assert.Equal(uri.ToString(), expenseReceipt.ReceiptUri);
                    Assert.Null(expenseReceipt.UserId);
                });

            await actual.SaveAsync();

        }

        [Fact]
        public async Task SaveAsyncWhenRegisterWhenNotSelectedCategory()
        {
            var expenseService = new Mock<IExpenseService>();
            var fileUploadService = new Mock<IFileStorageService>();

            var dateTimeService = new Mock<IDateTimeService>();
            var mediaService = new Mock<IMediaService>();
            var actual = new EditExpense(expenseService.Object, fileUploadService.Object, dateTimeService.Object, mediaService.Object);

            var receipt = new Mock<IMediaFile>().Object;
            actual.Receipt = receipt;
            actual.SelectedCategory = null;

            var uri = new Uri("https://googole.co.jp");
            fileUploadService.Setup(m => m.UploadMediaFileAsync(receipt)).ReturnsAsync(uri);

            expenseService
                .Setup(m => m.RegisterExpensesAsync(It.IsAny<Expense>(), It.IsAny<IEnumerable<ExpenseReceipt>>()))
                .Returns(Task.CompletedTask)
                .Callback<Expense, IEnumerable<ExpenseReceipt>>((expense, expenseReceipts) =>
                {
                    Assert.Null(expense.CategoryId);
                });

            await actual.SaveAsync();
        }

        [Fact]
        public async Task SaveAsyncWhenUpdate()
        {
            var expenseService = new Mock<IExpenseService>();
            expenseService
                .Setup(m => m.GetExpenseAsync("expenseId"))
                .ReturnsAsync(new Expense
                {
                    Amount = 2,
                    Date = DateTime.MinValue,
                    CategoryId = "category2",
                    Id = "expenseId",
                    Location = "Location",
                    Note = "Note"
                });

            var fileUploadService = new Mock<IFileStorageService>();

            var dateTimeService = new Mock<IDateTimeService>();
            var mediaService = new Mock<IMediaService>();
            var actual = new EditExpense(expenseService.Object, fileUploadService.Object, dateTimeService.Object, mediaService.Object);
            await actual.InitializeAsync("expenseId");

            actual.Amount = 1;
            actual.Date = DateTime.MaxValue;
            actual.Location = "location";
            actual.Note = "note";
            var receipt = new Mock<IMediaFile>().Object;
            actual.Receipt = receipt;

            var uri = new Uri("https://www.bing.com/");
            fileUploadService.Setup(m => m.UploadMediaFileAsync(receipt)).ReturnsAsync(uri);

            expenseService
                .Setup(m => m.RegisterExpensesAsync(It.IsAny<Expense>(), It.IsAny<IEnumerable<ExpenseReceipt>>()))
                .Returns(Task.CompletedTask)
                .Callback<Expense, IEnumerable<ExpenseReceipt>>((expense, expenseReceipts) =>
                {
                    Assert.NotNull(expense);
                    Assert.Equal("expenseId", expense.Id);
                    Assert.Equal(1, expense.Amount);
                    Assert.Equal(DateTime.MaxValue, expense.Date);
                    Assert.Equal("location", expense.Location);
                    Assert.Equal("note", expense.Note);

                    Assert.NotNull(expenseReceipts);
                    Assert.Single(expenseReceipts);
                    var expenseReceipt = expenseReceipts.First();
                    Assert.Null(expenseReceipt.Id);
                    Assert.Null(expenseReceipt.ExpenseId);
                    Assert.Equal(uri.ToString(), expenseReceipt.ReceiptUri);
                    Assert.Null(expenseReceipt.UserId);
                });

            await actual.SaveAsync();
        }

        [Fact]
        public void IsTakePhotoSupported()
        {
            var expenseService = new Mock<IExpenseService>();
            var fileUploadService = new Mock<IFileStorageService>();
            var dateTimeService = new Mock<IDateTimeService>();
            var mediaService = new Mock<IMediaService>();
            var actual = new EditExpense(expenseService.Object, fileUploadService.Object, dateTimeService.Object, mediaService.Object);

            mediaService.Setup(m => m.IsTakePhotoSupported).Returns(true);
            mediaService.Setup(m => m.IsCameraAvailable).Returns(true);
            Assert.True(actual.IsTakePhotoSupported);

            mediaService.Setup(m => m.IsTakePhotoSupported).Returns(false);
            mediaService.Setup(m => m.IsCameraAvailable).Returns(true);
            Assert.False(actual.IsTakePhotoSupported);

            mediaService.Setup(m => m.IsTakePhotoSupported).Returns(true);
            mediaService.Setup(m => m.IsCameraAvailable).Returns(false);
            Assert.False(actual.IsTakePhotoSupported);

            mediaService.Setup(m => m.IsTakePhotoSupported).Returns(false);
            mediaService.Setup(m => m.IsCameraAvailable).Returns(false);
            Assert.False(actual.IsTakePhotoSupported);
        }

        [Fact]
        public void IsPickPhotoSupported()
        {
            var expenseService = new Mock<IExpenseService>();
            var fileUploadService = new Mock<IFileStorageService>();
            var dateTimeService = new Mock<IDateTimeService>();
            var mediaService = new Mock<IMediaService>();
            var actual = new EditExpense(expenseService.Object, fileUploadService.Object, dateTimeService.Object, mediaService.Object);

            mediaService.Setup(m => m.IsPickPhotoSupported).Returns(true);
            Assert.True(actual.IsPickPhotoSupported);

            mediaService.Setup(m => m.IsPickPhotoSupported).Returns(false);
            Assert.False(actual.IsPickPhotoSupported);
        }
    }
}
