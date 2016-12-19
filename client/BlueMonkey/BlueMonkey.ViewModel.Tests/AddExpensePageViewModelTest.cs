using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using BlueMonkey.Business;
using BlueMonkey.MediaServices;
using BlueMonkey.Model;
using BlueMonkey.ViewModels;
using Moq;
using Prism.Navigation;
using Xunit;

namespace BlueMonkey.ViewModel.Tests
{
    public class AddExpensePageViewModelTest
    {
        [Fact]
        public void NameProperty()
        {
            var navigationService = new Mock<INavigationService>();
            var editExpense = new Mock<IEditExpense>();

            var actual = new AddExpensePageViewModel(navigationService.Object, editExpense.Object);

            Assert.NotNull(actual.Name);
            Assert.Null(actual.Name.Value);

            actual.Name.Value = "SetValue";
            editExpense.VerifySet(x => x.Name = "SetValue", Times.Once);

            editExpense.Setup(m => m.Name).Returns("UpdateValue");
            editExpense.Raise(m => m.PropertyChanged += null, new PropertyChangedEventArgs("Name"));

            Assert.Equal("UpdateValue", actual.Name.Value);
        }

        [Fact]
        public void AmountProperty()
        {
            var navigationService = new Mock<INavigationService>();
            var editExpense = new Mock<IEditExpense>();

            var actual = new AddExpensePageViewModel(navigationService.Object, editExpense.Object);

            Assert.NotNull(actual.Amount);
            Assert.Equal(0, actual.Amount.Value);

            actual.Amount.Value = 1;
            editExpense.VerifySet(x => x.Amount = 1, Times.Once);

            editExpense.Setup(m => m.Amount).Returns(2);
            editExpense.Raise(m => m.PropertyChanged += null, new PropertyChangedEventArgs("Amount"));

            Assert.Equal(2, actual.Amount.Value);
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

            actual.Date.Value = DateTime.MaxValue;
            editExpense.VerifySet(x => x.Date = DateTime.MaxValue, Times.Once);

            editExpense.Setup(m => m.Date).Returns(DateTime.MinValue);
            editExpense.Raise(m => m.PropertyChanged += null, new PropertyChangedEventArgs("Date"));

            Assert.Equal(DateTime.MinValue, actual.Date.Value);
        }

        [Fact]
        public void LocationProperty()
        {
            var navigationService = new Mock<INavigationService>();
            var editExpense = new Mock<IEditExpense>();

            var actual = new AddExpensePageViewModel(navigationService.Object, editExpense.Object);

            Assert.NotNull(actual.Location);
            Assert.Null(actual.Location.Value);

            actual.Location.Value = "SetValue";
            editExpense.VerifySet(x => x.Location = "SetValue", Times.Once);

            editExpense.Setup(m => m.Location).Returns("UpdateValue");
            editExpense.Raise(m => m.PropertyChanged += null, new PropertyChangedEventArgs("Location"));

            Assert.Equal("UpdateValue", actual.Location.Value);
        }

        [Fact]
        public void NoteProperty()
        {
            var navigationService = new Mock<INavigationService>();
            var editExpense = new Mock<IEditExpense>();

            var actual = new AddExpensePageViewModel(navigationService.Object, editExpense.Object);

            Assert.NotNull(actual.Note);
            Assert.Null(actual.Note.Value);

            actual.Note.Value = "SetValue";
            editExpense.VerifySet(x => x.Note = "SetValue", Times.Once);

            editExpense.Setup(m => m.Note).Returns("UpdateValue");
            editExpense.Raise(m => m.PropertyChanged += null, new PropertyChangedEventArgs("Note"));

            Assert.Equal("UpdateValue", actual.Note.Value);
        }

        [Fact]
        public void Categories()
        {
            var navigationService = new Mock<INavigationService>();
            var editExpense = new Mock<IEditExpense>();

            var actual = new AddExpensePageViewModel(navigationService.Object, editExpense.Object);

            Assert.NotNull(actual.Categories);
            Assert.NotNull(actual.Categories.Value);
            Assert.Equal(0, actual.Categories.Value.Count());

            var categories = new [] { new Category {Name = "category1"}, new Category { Name = "category2" } };
            editExpense.Setup(x => x.Categories).Returns(categories);
            editExpense.Raise(m => m.PropertyChanged += null, new PropertyChangedEventArgs("Categories"));

            var actualCategory = actual.Categories.Value?.ToList();
            Assert.NotNull(actualCategory);
            Assert.Equal(2, actualCategory.Count);
            Assert.Equal("category1", actualCategory[0]);
            Assert.Equal("category2", actualCategory[1]);
        }

        [Fact]
        public void SelectedCategoryIndexProperty()
        {
            var navigationService = new Mock<INavigationService>();
            var editExpense = new Mock<IEditExpense>();

            var actual = new AddExpensePageViewModel(navigationService.Object, editExpense.Object);

            Assert.NotNull(actual.SelectedCategoryIndex);
            Assert.Equal(-1, actual.SelectedCategoryIndex.Value);

            // notify model to vm.
            var category1 = new Category();
            var category2 = new Category();
            var categories = new[] { category1, category2 };
            editExpense.Setup(x => x.Categories).Returns(categories);
            editExpense.Setup(x => x.SelectedCategory).Returns(category2);
            editExpense.Raise(m => m.PropertyChanged += null, new PropertyChangedEventArgs("SelectedCategory"));
            Assert.Equal(1, actual.SelectedCategoryIndex.Value);

            actual.SelectedCategoryIndex.Value = 0;
            editExpense.VerifySet(m => m.SelectedCategory = category1, Times.Once);
        }

        [Fact]
        public void SaveAsyncCommand()
        {
            var navigationService = new Mock<INavigationService>();
            var editExpense = new Mock<IEditExpense>();

            editExpense.Setup(m => m.Name).Returns("Name");

            var actual = new AddExpensePageViewModel(navigationService.Object, editExpense.Object);

            Assert.NotNull(actual.SaveAsyncCommand);
            Assert.True(actual.SaveAsyncCommand.CanExecute());

            actual.SaveAsyncCommand.Execute();
            editExpense.Verify(m => m.SaveAsync(), Times.Once);
            navigationService.Verify(m => m.GoBackAsync(null, null, true), Times.Once);

            editExpense.Setup(m => m.Name).Returns<string>(null);
            editExpense.Raise(m => m.PropertyChanged += null, new PropertyChangedEventArgs("Name"));
            Assert.False(actual.SaveAsyncCommand.CanExecute());
        }

        [Fact]
        public void NavigateReceiptPageCommand()
        {
            var navigationService = new Mock<INavigationService>();
            var editExpense = new Mock<IEditExpense>();
            var actual = new AddExpensePageViewModel(navigationService.Object, editExpense.Object);

            Assert.NotNull(actual.NavigateReceiptPageCommand);
            Assert.True(actual.NavigateReceiptPageCommand.CanExecute(null));

            actual.NavigateReceiptPageCommand.Execute(null);

            navigationService.Verify(m => m.NavigateAsync("ReceiptPage", null, null, true), Times.Once);
        }
    }
}