using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using BlueMonkey.Model;
using Reactive.Bindings;

namespace BlueMonkey.ViewModels
{
    public class ExpenseSelectionPageViewModel : BindableBase
    {
        public ReadOnlyReactiveCollection<SelectableExpense> Expenses { get; }
        public ExpenseSelectionPageViewModel(IEditReport editReport)
        {
            Expenses = editReport.SelectableExpenses.ToReadOnlyReactiveCollection();
        }
    }
}
