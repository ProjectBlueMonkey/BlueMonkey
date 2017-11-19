using System;
using System.Linq;
using BlueMonkey.MediaServices;
using BlueMonkey.Usecases;
using Moq;
using Prism.Navigation;
using Xunit;

namespace BlueMonkey.ViewModels.Tests
{
    public class AddExpensePageViewModelTest
    {
        [Fact]
        public void HasReceiptProperty()
        {
            var navigationService = new Mock<INavigationService>();
            var editExpense = new Mock<IEditExpense>();

            var actual = new AddExpensePageViewModel(navigationService.Object, editExpense.Object);

            Assert.NotNull(actual.HasReceipt);
            Assert.False(actual.HasReceipt.Value);

            // Model -> ViewModel
            var media = new Mock<IMediaFile>();
            editExpense.NotifyPropertyChanged(m => m.Receipt, media.Object);
            Assert.True(actual.HasReceipt.Value);

            // Destroy
            // ...
        }

        [Fact]
        public void AmountProperty()
        {
            var navigationService = new Mock<INavigationService>();
            var editExpense = new Mock<IEditExpense>();

            var actual = new AddExpensePageViewModel(navigationService.Object, editExpense.Object);

            Assert.NotNull(actual.Amount);
            Assert.Equal(0, actual.Amount.Value);

            // ViewMode -> Model
            actual.Amount.Value = 1;
            editExpense.VerifySet(x => x.Amount = 1, Times.Once);

            // Model -> ViewModel
            editExpense.NotifyPropertyChanged(m => m.Amount, 2);
            Assert.Equal(2, actual.Amount.Value);

            // Destroy
            actual.Destroy();
            editExpense.NotifyPropertyChanged(m => m.Amount, 3);
            Assert.NotEqual(3, actual.Amount.Value);
        }

        [Fact]
        public void DateProperty()
        {
            var navigationService = new Mock<INavigationService>();
            var editExpense = new Mock<IEditExpense>();
            editExpense.Setup(m => m.Date).Returns(DateTime.MinValue);

            var actual = new AddExpensePageViewModel(navigationService.Object, editExpense.Object);

            Assert.NotNull(actual.Date);
            Assert.Equal(DateTime.MinValue, actual.Date.Value);

            // ViewMode -> Model
            actual.Date.Value = DateTime.MaxValue;
            editExpense.VerifySet(x => x.Date = DateTime.MaxValue, Times.Once);

            // Model -> ViewModel
            editExpense.NotifyPropertyChanged(m => m.Date, DateTime.MinValue);
            Assert.Equal(DateTime.MinValue, actual.Date.Value);

            // Destroy
            actual.Destroy();
            editExpense.NotifyPropertyChanged(m => m.Date, DateTime.MaxValue);
            Assert.NotEqual(DateTime.MaxValue, actual.Date.Value);
        }

        [Fact]
        public void LocationProperty()
        {
            var navigationService = new Mock<INavigationService>();
            var editExpense = new Mock<IEditExpense>();

            var actual = new AddExpensePageViewModel(navigationService.Object, editExpense.Object);

            Assert.NotNull(actual.Location);
            Assert.Null(actual.Location.Value);

            // ViewMode -> Model
            actual.Location.Value = "SetValue";
            editExpense.VerifySet(x => x.Location = "SetValue", Times.Once);

            // Model -> ViewModel
            editExpense.NotifyPropertyChanged(m => m.Location, "UpdateValue");
            Assert.Equal("UpdateValue", actual.Location.Value);

            // Destroy
            actual.Destroy();
            editExpense.NotifyPropertyChanged(m => m.Location, "Destroy");
            Assert.NotEqual("Destroy", actual.Location.Value);
        }

        [Fact]
        public void NoteProperty()
        {
            var navigationService = new Mock<INavigationService>();
            var editExpense = new Mock<IEditExpense>();

            var actual = new AddExpensePageViewModel(navigationService.Object, editExpense.Object);

            Assert.NotNull(actual.Note);
            Assert.Null(actual.Note.Value);

            // ViewMode -> Model
            actual.Note.Value = "SetValue";
            editExpense.VerifySet(x => x.Note = "SetValue", Times.Once);

            // Model -> ViewModel
            editExpense.NotifyPropertyChanged(m => m.Note, "UpdateValue");
            Assert.Equal("UpdateValue", actual.Note.Value);

            // Destroy
            actual.Destroy();
            editExpense.NotifyPropertyChanged(m => m.Note, "Destroy");
            Assert.NotEqual("Destroy", actual.Note.Value);
        }

        [Fact]
        public void Categories()
        {
            var navigationService = new Mock<INavigationService>();
            var editExpense = new Mock<IEditExpense>();

            var actual = new AddExpensePageViewModel(navigationService.Object, editExpense.Object);

            Assert.NotNull(actual.Categories);
            Assert.NotNull(actual.Categories.Value);
            Assert.Empty(actual.Categories.Value);

            var categories = 
                new []
                {
                    new Category { Name = "category1", SortOrder = 1},
                    new Category { Name = "category2", SortOrder = 0}
                };
            editExpense.NotifyPropertyChanged(m => m.Categories, categories);

            // Model -> ViewModel
            var actualCategory = actual.Categories.Value?.ToList();
            Assert.NotNull(actualCategory);
            Assert.Equal(2, actualCategory.Count);
            Assert.Equal("category2", actualCategory[0]);
            Assert.Equal("category1", actualCategory[1]);

            // Destroy
            actual.Destroy();
            editExpense.NotifyPropertyChanged(m => m.Categories, new Category[] { });
            Assert.NotEmpty(actual.Categories.Value);
        }

        [Fact]
        public void SelectedCategoryIndexProperty()
        {
            var navigationService = new Mock<INavigationService>();
            var editExpense = new Mock<IEditExpense>();

            var actual = new AddExpensePageViewModel(navigationService.Object, editExpense.Object);

            Assert.NotNull(actual.SelectedCategoryIndex);
            Assert.Equal(-1, actual.SelectedCategoryIndex.Value);

            // Model -> ViewModel
            var category1 = new Category { Id = "category1"};
            var category2 = new Category { Id = "category2" };
            var categories = new[] { category1, category2 };
            editExpense.NotifyPropertyChanged(m => m.Categories, categories);
            editExpense.NotifyPropertyChanged(m => m.SelectedCategory, category2);
            Assert.Equal(1, actual.SelectedCategoryIndex.Value);


            // ViewMode -> Model
            actual.SelectedCategoryIndex.Value = 0;
            editExpense.VerifySet(m => m.SelectedCategory = category1, Times.Once);

            // Destroy
            actual.Destroy();
            editExpense.NotifyPropertyChanged(m => m.SelectedCategory, category1);
            Assert.NotEqual(2, actual.SelectedCategoryIndex.Value);
        }

        [Fact]
        public void CancenCommand()
        {
            var navigationService = new Mock<INavigationService>();
            var editExpense = new Mock<IEditExpense>();

            var actual = new AddExpensePageViewModel(navigationService.Object, editExpense.Object);

            Assert.NotNull(actual.CancelCommand);
            Assert.True(actual.CancelCommand.CanExecute());

            actual.CancelCommand.Execute();
            editExpense.Verify(m => m.SaveAsync(), Times.Never);
            navigationService.Verify(m => m.GoBackAsync(null, true, true), Times.Once);

        }

        [Fact]
        public void SaveCommand()
        {
            var navigationService = new Mock<INavigationService>();
            var editExpense = new Mock<IEditExpense>();

            editExpense.Setup(m => m.Location).Returns("Location");

            var actual = new AddExpensePageViewModel(navigationService.Object, editExpense.Object);

            Assert.NotNull(actual.SaveCommand);
            Assert.True(actual.SaveCommand.CanExecute());

            actual.SaveCommand.Execute();
            editExpense.Verify(m => m.SaveAsync(), Times.Once);
            navigationService.Verify(m => m.GoBackAsync(null, true, true), Times.Once);

            editExpense.NotifyPropertyChanged(m => m.Location, null);
            Assert.False(actual.SaveCommand.CanExecute());

            // Destroy
            actual.Destroy();
            editExpense.NotifyPropertyChanged(m => m.Location, "Location");
            Assert.False(actual.SaveCommand.CanExecute());
        }

        [Fact]
        public void NavigateReceiptPageCommand()
        {
            var navigationService = new Mock<INavigationService>();
            var editExpense = new Mock<IEditExpense>();
            var actual = new AddExpensePageViewModel(navigationService.Object, editExpense.Object);

            Assert.NotNull(actual.NavigateReceiptPageCommand);
            Assert.True(actual.NavigateReceiptPageCommand.CanExecute());

            actual.NavigateReceiptPageCommand.Execute(null);

            navigationService.Verify(m => m.NavigateAsync("ReceiptPage", null, null, true), Times.Once);
        }

        [Fact]
        public void OnNavigatedFrom()
        {
            var navigationService = new Mock<INavigationService>();
            var editExpense = new Mock<IEditExpense>();
            var actual = new AddExpensePageViewModel(navigationService.Object, editExpense.Object);
            actual.OnNavigatedFrom(null);
        }

        [Fact]
        public void OnNavigatedTo()
        {
            var navigationService = new Mock<INavigationService>();
            var editExpense = new Mock<IEditExpense>();
            var actual = new AddExpensePageViewModel(navigationService.Object, editExpense.Object);
            actual.OnNavigatedTo(null);
        }

        [Fact]
        public void OnNavigatingTo()
        {
            var navigationService = new Mock<INavigationService>();
            var editExpense = new Mock<IEditExpense>();
            var actual = new AddExpensePageViewModel(navigationService.Object, editExpense.Object);
            actual.OnNavigatingTo(null);

            editExpense.Verify(m => m.InitializeAsync(), Times.Once);
        }

        [Fact]
        public void OnNavigatingToForUpdate()
        {
            var navigationService = new Mock<INavigationService>();
            var editExpense = new Mock<IEditExpense>();
            var actual = new AddExpensePageViewModel(navigationService.Object, editExpense.Object);

            var navigationParameters = new NavigationParameters
            {
                { AddExpensePageViewModel.ExpenseIdKey, "expenseId" }
            };
            actual.OnNavigatingTo(navigationParameters);

            editExpense.Verify(m => m.InitializeAsync("expenseId"), Times.Once);
        }
    }
}