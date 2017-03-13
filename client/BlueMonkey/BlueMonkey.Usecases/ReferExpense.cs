using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BlueMonkey.ExpenseServices;

namespace BlueMonkey.Usecases
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

        /// <summary>
        /// Search expenses.
        /// </summary>
        /// <returns></returns>
        public async Task SearchAsync()
        {
            _expenses.Clear();
            foreach (var expense in await _expenseService.GetExpensesAsync())
            {
                _expenses.Add(expense);
            }
        }
    }
}
