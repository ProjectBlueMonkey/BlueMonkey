using BlueMonkey.Business;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.Generic;
using System.Reactive.Disposables;
using BlueMonkey.ExpenseServices;
using BlueMonkey.Model;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace BlueMonkey.ViewModels
{
    /// <summary>
    /// ViewModel for expense list page.
    /// </summary>
    public class ExpenseListPageViewModel : BindableBase, INavigationAware, IDestructible
    {
        /// <summary>
        /// Navigation service.
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// ReferExpense usecase model 
        /// </summary>
        private readonly IReferExpense _referExpense;

        /// <summary>
        /// Resource disposer.
        /// </summary>
        private CompositeDisposable Disposable { get; } = new CompositeDisposable();

        public ReadOnlyReactiveCollection<Expense> Expenses { get; }

        public DelegateCommand<Expense> AddExpenseCommand => new DelegateCommand<Expense>(AddExpense);

        public ExpenseListPageViewModel(INavigationService navigationService, IReferExpense referExpense)
        {
            _navigationService = navigationService;
            _referExpense = referExpense;

            Expenses = _referExpense.Expenses.ToReadOnlyReactiveCollection().AddTo(Disposable);
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
            await _referExpense.SearchAsync();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        public void Destroy()
        {
            Disposable.Dispose();
        }
    }
}
