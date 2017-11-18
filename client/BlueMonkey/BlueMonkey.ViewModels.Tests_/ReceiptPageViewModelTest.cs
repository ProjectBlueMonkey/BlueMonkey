using System.IO;
using BlueMonkey.MediaServices;
using BlueMonkey.Usecases;
using Moq;
using Xunit;

namespace BlueMonkey.ViewModels.Tests
{
    public class ReceiptPageViewModelTest
    {
        [Fact]
        public void ReceiptProperty()
        {
            var editExpense = new Mock<IEditExpense>();
            var actual = new ReceiptPageViewModel(editExpense.Object);

            Assert.NotNull(actual.Receipt);
            Assert.Null(actual.Receipt.Value);

            // First time.
            var image = File.ReadAllBytes("lena.jpg");
            editExpense.NotifyPropertyChanged(m => m.Receipt, new MediaFile(".jpg", image));
            Assert.NotNull(actual.Receipt.Value);

            var first = actual.Receipt.Value;
            // Second time.
            editExpense.NotifyPropertyChanged(m => m.Receipt, new MediaFile(".jpg", image));
            var second = actual.Receipt.Value;
            Assert.NotEqual(first, second);

            // Destory
            actual.Destroy();
            editExpense.NotifyPropertyChanged(m => m.Receipt, new MediaFile(".jpg", image));
            Assert.Equal(second, actual.Receipt.Value);
        }

        [Fact]
        public void PickPhotoAsyncCommand()
        {
            var editExpense = new Mock<IEditExpense>();
            editExpense.Setup(m => m.IsPickPhotoSupported).Returns(true);

            var actual = new ReceiptPageViewModel(editExpense.Object);

            Assert.NotNull(actual.PickPhotoAsyncCommand);
            Assert.True(actual.PickPhotoAsyncCommand.CanExecute());

            actual.PickPhotoAsyncCommand.Execute();
            editExpense.Verify(m => m.PickPhotoAsync(), Times.Once);
        }

        [Fact]
        public void PickPhotoAsyncCommandWhenIsPickPhotoSupportedFalse()
        {
            var editExpense = new Mock<IEditExpense>();
            editExpense.Setup(m => m.IsPickPhotoSupported).Returns(false);

            var actual = new ReceiptPageViewModel(editExpense.Object);

            Assert.NotNull(actual.PickPhotoAsyncCommand);
            Assert.False(actual.PickPhotoAsyncCommand.CanExecute());
        }

        [Fact]
        public void TakePhotoAsyncCommand()
        {
            var editExpense = new Mock<IEditExpense>();
            editExpense.Setup(m => m.IsTakePhotoSupported).Returns(true);

            var actual = new ReceiptPageViewModel(editExpense.Object);

            Assert.NotNull(actual.TakePhotoAsyncCommand);
            Assert.True(actual.TakePhotoAsyncCommand.CanExecute());

            actual.TakePhotoAsyncCommand.Execute();

            editExpense.Verify(m => m.TakePhotoAsync(), Times.Once);
        }

        [Fact]
        public void TakePhotoAsyncCommandWhenIsTakePhotoSupportedFalse()
        {
            var editExpense = new Mock<IEditExpense>();
            editExpense.Setup(m => m.IsTakePhotoSupported).Returns(false);

            var actual = new ReceiptPageViewModel(editExpense.Object);

            Assert.NotNull(actual.TakePhotoAsyncCommand);
            Assert.False(actual.TakePhotoAsyncCommand.CanExecute());
        }
    }
}
