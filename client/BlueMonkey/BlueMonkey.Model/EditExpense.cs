using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.Business;
using BlueMonkey.ExpenseServices;
using Prism.Mvvm;

namespace BlueMonkey.Model
{
    public class EditExpense : BindableBase, IEditExpense
    {
        private readonly IExpenseService _expenseService;

        private Expense _expense;
        public Expense Expense
        {
            get { return _expense; }
            set { SetProperty(ref _expense, value); }
        }

        private IEnumerable<Category> _categories;

        public IEnumerable<Category> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }

        /// <summary>
        /// Initialize Instance.
        /// </summary>
        /// <param name="expenseService"></param>
        public EditExpense(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        public async Task InitializeAsync()
        {
            Expense = new Expense();
            Categories = await _expenseService.GetCategoriesAsync();
        }
    }
}
