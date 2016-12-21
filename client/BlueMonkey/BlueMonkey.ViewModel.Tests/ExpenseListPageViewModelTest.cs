using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.Business;
using BlueMonkey.Model;
using BlueMonkey.ViewModels;
using Moq;
using Prism.Navigation;
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
            Assert.Equal(0, actual.Expenses.Count());
        }
    }
}
