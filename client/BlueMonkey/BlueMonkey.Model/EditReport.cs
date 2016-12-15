using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.Business;
using BlueMonkey.ExpenceServices;
using BlueMonkey.TimeService;
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
        /// IDateTimeService instance.
        /// </summary>
        private readonly IDateTimeService _dateTimeService;

        /// <summary>
        /// Original Report get from service
        /// </summary>
        private Report _originalReport;

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
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
        /// InitializeAsync Instance.
        /// </summary>
        /// <param name="expenseService"></param>
        public EditReport(IExpenseService expenseService, IDateTimeService dateTimeService)
        {
            _expenseService = expenseService;
            _dateTimeService = dateTimeService;
            SelectableExpenses = new ReadOnlyObservableCollection<SelectableExpense>(_selectableExpenses);
        }

        /// <summary>
        /// InitializeAsync for new registration.
        /// </summary>
        /// <returns></returns>
        public async Task InitializeForNewReportAsync()
        {
            Name = null;
            Date = _dateTimeService.Today;

            await InitializeAsync(new Expense[] { });
        }

        /// <summary>
        /// InitializeAsync for update report.
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public async Task InitializeForUpdateReportAsync(string reportId)
        {
            if(reportId == null) throw new ArgumentNullException(nameof(reportId));

            // Since Report is not deleted, null check is not performed.
            _originalReport = await _expenseService.GetReportAsync(reportId);
            Name = _originalReport.Name;
            Date = _originalReport.Date;

            await InitializeAsync(await _expenseService.GetExpensesFromReportIdAsync(reportId));
        }

        /// <summary>
        /// Initialization of common parts.
        /// </summary>
        /// <param name="expensesForReport"></param>
        /// <returns></returns>
        private async Task InitializeAsync(IEnumerable<Expense> expensesForReport)
        {
            var expenses = expensesForReport.Concat(await _expenseService.GetUnregisteredExpensesAsync());

            _selectableExpenses.Clear();
            foreach (var expense in expenses.OrderBy(x => x.Date))
            {
                _selectableExpenses.Add(new SelectableExpense(expense) { IsSelected = expense.ReportId != null });
            }
        }

        /// <summary>
        /// Register or Update Report.
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            await _expenseService.RegisterReport(
                new Report
                {
                    Name = Name,
                    Date = Date
                },
                SelectableExpenses.Where(x => x.IsSelected));
        }
    }
}
