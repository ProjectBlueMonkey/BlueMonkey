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
using Xunit;

namespace BlueMonkey.ViewModel.Tests
{
    public class ExpenseSelectionPageViewModelTest
    {
        [Fact]
        public void Constructor()
        {
            var editReport = new Mock<IEditReport>();
            var expense01 = new SelectableExpense(new Expense()) { IsSelected = false };
            var expense02 = new SelectableExpense(new Expense()) { IsSelected = true };
            var expenses = new ObservableCollection<SelectableExpense>(new[] { expense01, expense02 });
            editReport
                .Setup(m => m.SelectableExpenses)
                .Returns(new ReadOnlyObservableCollection<SelectableExpense>(expenses));

            var actual = new ExpenseSelectionPageViewModel(editReport.Object);

            Assert.NotNull(actual.Expenses);
            Assert.Equal(2, actual.Expenses.Count);
            Assert.Equal(expense01, actual.Expenses[0]);
            Assert.Equal(expense02, actual.Expenses[1]);
        }
    }
}
