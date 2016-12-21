using BlueMonkey.Business;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.Generic;
using BlueMonkey.ExpenseServices;
using BlueMonkey.Model;
using Reactive.Bindings;

namespace BlueMonkey.ViewModels
{
    /// <summary>
    /// ViewModel for expense list page.
    /// </summary>
    public class ExpenseListPageViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        /// Navigation service.
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// ReferExpense usecase model 
        /// </summary>
        private readonly IReferExpense _referExpense;

        public ReadOnlyReactiveCollection<Expense> Expenses { get; }

        public DelegateCommand<Expense> AddExpenseCommand => new DelegateCommand<Expense>(AddExpense);

        public ExpenseListPageViewModel(INavigationService navigationService, IReferExpense referExpense)
        {
            _navigationService = navigationService;
            _referExpense = referExpense;

            Expenses = _referExpense.Expenses.ToReadOnlyReactiveCollection();
        }

        private void AddExpense(Expense expense)
        {
            _navigationService.NavigateAsync("AddExpensePage");
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}
