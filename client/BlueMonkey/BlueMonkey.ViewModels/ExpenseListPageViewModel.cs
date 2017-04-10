using System;
using Prism.Mvvm;
using Prism.Navigation;
using System.Reactive.Disposables;
using BlueMonkey.Usecases;
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

        public ReactiveCommand AddExpenseCommand { get; }

        public ReactiveCommand<Expense> UpdateExpenseCommand { get; }

        public ExpenseListPageViewModel(INavigationService navigationService, IReferExpense referExpense)
        {
            _navigationService = navigationService;
            _referExpense = referExpense;

            Expenses = _referExpense.Expenses.ToReadOnlyReactiveCollection().AddTo(Disposable);
            AddExpenseCommand = new ReactiveCommand();
            AddExpenseCommand.Subscribe(_ => AddExpense());

            UpdateExpenseCommand = new ReactiveCommand<Expense>();
            UpdateExpenseCommand.Subscribe(UpdateExpense);
        }

        private void AddExpense()
        {
            _navigationService.NavigateAsync("AddExpensePage");
        }

        private void UpdateExpense(Expense expense)
        {
            var navigationParameters = new NavigationParameters
            {
                { AddExpensePageViewModel.ExpenseIdKey, expense.Id }
            };
            _navigationService.NavigateAsync("AddExpensePage", navigationParameters);
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
