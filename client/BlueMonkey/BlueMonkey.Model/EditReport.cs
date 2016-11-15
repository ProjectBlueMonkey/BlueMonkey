using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.Business;
using BlueMonkey.ExpenceServices;
using Prism.Mvvm;

namespace BlueMonkey.Model
{
    public class EditReport : BindableBase, IEditReport
    {
        /// <summary>
        /// IExpenseService instance.
        /// </summary>
        private readonly IExpenseService _expenseService;

        /// <summary>
        /// Backing store of report.
        /// </summary>
        private Report _report;

        public Report Report
        {
            get { return _report; }
            set { SetProperty(ref _report, value); }
        }

        /// <summary>
        /// Backing store of SelectedExpenses.
        /// </summary>
        private readonly ObservableCollection<SelectableExpense> _selectableExpenses = new ObservableCollection<SelectableExpense>();
        /// <summary>
        /// Expenses to include in the report.
        /// </summary>
        public ReadOnlyObservableCollection<SelectableExpense> SelectableExpenses { get; }
        /// <summary>
        /// Initialize Instance.
        /// </summary>
        /// <param name="expenseService"></param>
        public EditReport(IExpenseService expenseService)
        {
            _expenseService = expenseService;
            SelectableExpenses = new ReadOnlyObservableCollection<SelectableExpense>(_selectableExpenses);
        }

        public async Task InitializeForNewReportAsync()
        {
            Report =
                new Report
                {
                    Date = DateTime.Today
                };
            _selectableExpenses.Clear();
            var expenses = await _expenseService.GetExpensesAsync();
            foreach (var expense in expenses.Where(x => x.ReportId == null))
            {
                _selectableExpenses.Add(new SelectableExpense(expense));
            }
        }
    }
}
