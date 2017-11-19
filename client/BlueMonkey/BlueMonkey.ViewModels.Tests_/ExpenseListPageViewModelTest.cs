using System.Collections.ObjectModel;
using System.Reactive.Concurrency;
using System.Threading.Tasks;
using BlueMonkey.Usecases;
using Moq;
using Prism.Navigation;
using Reactive.Bindings;
using Xunit;

namespace BlueMonkey.ViewModels.Tests
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
            Assert.Empty(actual.Expenses);
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
            Assert.Single(actual.Expenses);
            Assert.Equal(expense, actual.Expenses[0]);

            expenses.Clear();
            Assert.Empty(actual.Expenses);

            actual.Destroy();

            expenses.Add(expense);
            Assert.Empty(actual.Expenses);
        }

        [Fact]
        public void AddExpenseCommand()
        {
            var navigationService = new Mock<INavigationService>();
            var referExpense = new Mock<IReferExpense>();
            var expenses = new ObservableCollection<Expense>();
            referExpense.Setup(m => m.Expenses).Returns(new ReadOnlyObservableCollection<Expense>(expenses));
            var actual = new ExpenseListPageViewModel(navigationService.Object, referExpense.Object);

            Assert.NotNull(actual.AddExpenseCommand);
            Assert.True(actual.AddExpenseCommand.CanExecute());

            actual.AddExpenseCommand.Execute(null);

            navigationService.Verify(m => m.NavigateAsync("AddExpensePage", null, null, true), Times.Once);
        }


        [Fact]
        public void UpdateExpenseCommand()
        {
            var navigationService = new Mock<INavigationService>();
            var referExpense = new Mock<IReferExpense>();
            var expenses = new ObservableCollection<Expense>();
            referExpense.Setup(m => m.Expenses).Returns(new ReadOnlyObservableCollection<Expense>(expenses));
            var actual = new ExpenseListPageViewModel(navigationService.Object, referExpense.Object);

            Assert.NotNull(actual.UpdateExpenseCommand);
            Assert.True(actual.UpdateExpenseCommand.CanExecute());

            var isCalled = false;
            navigationService
                .Setup(m => m.NavigateAsync("AddExpensePage", It.IsAny<NavigationParameters>(), null, true))
                .Returns(Task.CompletedTask)
                .Callback<string, NavigationParameters, bool?, bool>(
                    (path, navigationParameters, useModalNavigation, animated) =>
                    {
                        isCalled = true;
                        Assert.NotNull(navigationParameters);
                        Assert.Equal(1, navigationParameters.Count);
                        Assert.True(navigationParameters.ContainsKey(AddExpensePageViewModel.ExpenseIdKey));
                        Assert.Equal("expenseId", navigationParameters[AddExpensePageViewModel.ExpenseIdKey]);
                    });

            actual.UpdateExpenseCommand.Execute(new Expense { Id = "expenseId" });

            Assert.True(isCalled);
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
