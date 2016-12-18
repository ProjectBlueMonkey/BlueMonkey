using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using BlueMonkey.Business;
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
        public void Expense()
        {
            var navigationService = new Mock<INavigationService>();
            var editExpense = new Mock<IEditExpense>();

            var actual = new AddExpensePageViewModel(navigationService.Object, editExpense.Object);

            Assert.NotNull(actual.Expense);
            Assert.Null(actual.Expense.Value);

            var expense = new Expense();
            editExpense.Setup(x => x.Expense).Returns(expense);
            editExpense.Raise(m => m.PropertyChanged += null, new PropertyChangedEventArgs("Expense"));

            Assert.Equal(expense, actual.Expense.Value);
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
    }
}