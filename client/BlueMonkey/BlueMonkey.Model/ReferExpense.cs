using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.Business;
using BlueMonkey.ExpenseServices;

namespace BlueMonkey.Model
{
    public class ReferExpense : IReferExpense
    {
        /// <summary>
        /// Expense service.
        /// </summary>
        private readonly IExpenseService _expenseService;

        /// <summary>
        /// Backing field for Expenses.
        /// </summary>
        private readonly ObservableCollection<Expense> _expenses = new ObservableCollection<Expense>();

        public ReadOnlyObservableCollection<Expense> Expenses { get; }

        /// <summary>
        /// Initialize instance.
        /// </summary>
        /// <param name="expenseService"></param>
        public ReferExpense(IExpenseService expenseService)
        {
            _expenseService = expenseService;
            Expenses = new ReadOnlyObservableCollection<Expense>(_expenses);
        }

        public Task SearchAsync()
        {
            throw new NotImplementedException();
        }
    }
}
