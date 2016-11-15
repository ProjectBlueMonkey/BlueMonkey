using System;
using System.Collections.Generic;
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
        private Report _report;

        public Report Report
        {
            get { return _report; }
            set { SetProperty(ref _report, value); }
        }
        private readonly IExpenseService _expenseService;
        private IEnumerable<Expense> _expenses;
        /// <summary>
        /// Initialize Instance.
        /// </summary>
        /// <param name="expenseService"></param>
        public EditReport(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        public async Task InitializeForNewReportAsync()
        {
            Report =
                new Report
                {
                    Date = DateTime.Today
                };
            _expenses = await _expenseService.GetExpensesAsync();
        }
    }
}
