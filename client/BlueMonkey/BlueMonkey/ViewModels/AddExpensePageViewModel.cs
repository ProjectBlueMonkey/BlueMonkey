using BlueMonkey.Business;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using BlueMonkey.Model;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace BlueMonkey.ViewModels
{
    /// <summary>
    /// ViewModel for AddExpensePage.
    /// </summary>
    public class AddExpensePageViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        /// INavigationService.
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// EditExpense Use case model.
        /// </summary>
        private readonly IEditExpense _editExpense;

        /// <summary>
        /// Target Expense
        /// </summary>
        public ReadOnlyReactiveProperty<Expense> Expense { get; }

        /// <summary>
        /// Selectable categories.
        /// </summary>
        public ReadOnlyReactiveProperty<IEnumerable<string>> Categories { get; }

        /// <summary>
        /// Selected Category.
        /// </summary>
        public ReactiveProperty<string> SelectedCategory { get; }

        /// <summary>
        /// command to save the expense.
        /// </summary>
        public AsyncReactiveCommand SaveAsyncCommand { get; }

        /// <summary>
        /// Command to navigate receipt page.
        /// </summary>
        public DelegateCommand NavigateReceiptPageCommand => new DelegateCommand(NavigateReceiptPage);

        /// <summary>
        /// Initialize instance.
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="editExpense"></param>
        public AddExpensePageViewModel(INavigationService navigationService, IEditExpense editExpense)
        {
            _navigationService = navigationService;
            _editExpense = editExpense;

            Expense = _editExpense.ObserveProperty(x => x.Expense).ToReadOnlyReactiveProperty();
            // Convert, because picker supports only string.
            Categories = _editExpense.ObserveProperty(x => x.Categories)
                .Where(x => x != null)
                .Select(x => x.Select(category => category.Name)).ToReadOnlyReactiveProperty();
            // Convert, because picker supports only string.
            SelectedCategory = _editExpense.ObserveProperty(x => x.SelectedCategory)
                .Where(x => x != null)
                .Select(x => x.Name).ToReactiveProperty();
            // When you select into the Category name.
            SelectedCategory.Subscribe(x =>
            {
                if (x == null)
                {
                    _editExpense.SelectedCategory = null;
                }
                else
                {
                    _editExpense.SelectedCategory = _editExpense.Categories.Single(category => category.Name == x);
                }
            });

            SaveAsyncCommand = new AsyncReactiveCommand();
            SaveAsyncCommand.Subscribe(SaveAsync);
        }

        /// <summary>
        /// Navigation receipt page.
        /// </summary>
        private void NavigateReceiptPage()
        {
            _navigationService.NavigateAsync("ReceiptPage");
        }

        /// <summary>
        /// Save edit expense.
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        private async Task SaveAsync(object sender)
        {
            await _editExpense.SaveAsync();
            await _navigationService.GoBackAsync();
        }

        /// <summary>
        /// Called when the implementer has been navigated away from.
        /// </summary>
        /// <param name="parameters"></param>
        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="parameters"></param>
        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        /// <summary>
        /// Called before the implementor has been navigated to.
        /// </summary>
        /// <param name="parameters"></param>
        public async void OnNavigatingTo(NavigationParameters parameters)
        {
            await _editExpense.InitializeAsync();
        }
    }
}
