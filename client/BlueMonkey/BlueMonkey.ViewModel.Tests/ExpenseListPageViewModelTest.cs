using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.Business;
using BlueMonkey.Model;
using BlueMonkey.ViewModels;
using Moq;
using Prism.Navigation;
using Reactive.Bindings;
using Xunit;

namespace BlueMonkey.ViewModel.Tests
{
    public class ExpenseListPageViewModelTest
    {
        [Fact]
        public void Constructor()
        {
            var navigationService = new Mock<INavigationService>();
            var referExpense = new Mock<IReferExpense>();
            referExpense.Setup(m => m.Expenses)
                .Returns(new ReadOnlyObservableCollection<Expense>(new ObservableCollection<Expense>()));
            var actual = new ExpenseListPageViewModel(navigationService.Object, referExpense.Object);

            Assert.NotNull(actual.Expenses);
            Assert.Equal(0, actual.Expenses.Count);
        }


        [Fact]
        public void ExpensesProperty()
        {
            ReactivePropertyScheduler.SetDefault(CurrentThreadScheduler.Instance);

            var navigationService = new Mock<INavigationService>();
            var referExpense = new Mock<IReferExpense>();
            var expenses = new ObservableCollection<Expense>();
            referExpense.Setup(m => m.Expenses).Returns(new ReadOnlyObservableCollection<Expense>(expenses));
            var actual = new ExpenseListPageViewModel(navigationService.Object, referExpense.Object);

            var expense = new Expense();
            expenses.Add(expense);
            Assert.Equal(1, actual.Expenses.Count);
            Assert.Equal(expense, actual.Expenses[0]);

            expenses.Clear();
            Assert.Equal(0, actual.Expenses.Count);

            actual.Destroy();

            expenses.Add(expense);
            Assert.Equal(0, actual.Expenses.Count);
        }

        [Fact]
        public void OnNavigatedFrom()
        {
            var navigationService = new Mock<INavigationService>();
            var referExpense = new Mock<IReferExpense>();
            var expenses = new ObservableCollection<Expense>();
            referExpense.Setup(m => m.Expenses).Returns(new ReadOnlyObservableCollection<Expense>(expenses));
            var actual = new ExpenseListPageViewModel(navigationService.Object, referExpense.Object);

            actual.OnNavigatedFrom(null);
        }

        [Fact]
        public void OnNavigatedTo()
        {
            var navigationService = new Mock<INavigationService>();
            var referExpense = new Mock<IReferExpense>();
            var expenses = new ObservableCollection<Expense>();
            referExpense.Setup(m => m.Expenses).Returns(new ReadOnlyObservableCollection<Expense>(expenses));
            var actual = new ExpenseListPageViewModel(navigationService.Object, referExpense.Object);

            actual.OnNavigatedTo(null);

            referExpense.Verify(m => m.SearchAsync(), Times.Once);
        }

        [Fact]
        public void OnNavigatingTo()
        {
            var navigationService = new Mock<INavigationService>();
            var referExpense = new Mock<IReferExpense>();
            var expenses = new ObservableCollection<Expense>();
            referExpense.Setup(m => m.Expenses).Returns(new ReadOnlyObservableCollection<Expense>(expenses));
            var actual = new ExpenseListPageViewModel(navigationService.Object, referExpense.Object);

            actual.OnNavigatingTo(null);
        }

    }
}
