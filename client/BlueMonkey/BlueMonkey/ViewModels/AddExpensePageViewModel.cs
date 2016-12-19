using BlueMonkey.Business;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BlueMonkey.Model;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Xamarin.Forms;

namespace BlueMonkey.ViewModels
{
    /// <summary>
    /// ViewModel for AddExpensePage.
    /// </summary>
    public class AddExpensePageViewModel : BindableBase, INavigationAware, IDestructible
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
        /// Resource disposer.
        /// </summary>
        private CompositeDisposable Disposable { get; } = new CompositeDisposable();
        /// <summary>
        /// Target Expense
        /// </summary>
        public ReactiveProperty<string> Name { get; }

        public ReactiveProperty<long> Amount { get; }

        public ReactiveProperty<DateTime> Date { get; }

        public ReactiveProperty<string> Location { get; }

        public ReactiveProperty<string> Note { get; }

        /// <summary>
        /// Selectable categories.
        /// </summary>
        public ReadOnlyReactiveProperty<IEnumerable<string>> Categories { get; }

        /// <summary>
        /// Selected Category.
        /// </summary>
        public ReactiveProperty<int> SelectedCategoryIndex { get; }

        /// <summary>
        /// command to save the expense.
        /// </summary>
        public AsyncReactiveCommand SaveAsyncCommand { get; }

        /// <summary>
        /// Command to navigate receipt page.
        /// </summary>
        public ICommand NavigateReceiptPageCommand { get; }

        /// <summary>
        /// Initialize instance.
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="editExpense"></param>
        public AddExpensePageViewModel(INavigationService navigationService, IEditExpense editExpense)
        {
            _navigationService = navigationService;
            _editExpense = editExpense;

            Name = _editExpense.ToReactivePropertyAsSynchronized(x => x.Name).AddTo(Disposable);
            Amount = _editExpense.ToReactivePropertyAsSynchronized(x => x.Amount).AddTo(Disposable);
            Date = _editExpense.ToReactivePropertyAsSynchronized(x => x.Date).AddTo(Disposable);
            Location = _editExpense.ToReactivePropertyAsSynchronized(x => x.Location).AddTo(Disposable);
            Note = _editExpense.ToReactivePropertyAsSynchronized(x => x.Note).AddTo(Disposable);

            // Convert, because picker supports only string.
            Categories = _editExpense.ObserveProperty(x => x.Categories)
                .Where(x => x != null)
                .Select(x => x.Select(category => category.Name)).ToReadOnlyReactiveProperty().AddTo(Disposable);
            // Convert, because picker supports only string.
            SelectedCategoryIndex = _editExpense.ObserveProperty(x => x.SelectedCategory)
                .Select(x => (x == null) ? -1 : _editExpense.Categories.ToList().IndexOf(x))
                .ToReactiveProperty().AddTo(Disposable);
            // When you select into the Category name.
            SelectedCategoryIndex.Subscribe(x =>
            {
                if (0 <= x)
                {
                    _editExpense.SelectedCategory = _editExpense.Categories.ToList()[x];
                }
            });

            SaveAsyncCommand =
                Name.Select(x => !string.IsNullOrWhiteSpace(x))
                .ToAsyncReactiveCommand().AddTo(Disposable);
            SaveAsyncCommand.Subscribe(SaveAsync);

            NavigateReceiptPageCommand = new Command(() => _navigationService.NavigateAsync("ReceiptPage"));
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

        /// <summary>
        /// Free resources.
        /// </summary>
        public void Destroy()
        {
            Disposable.Dispose();
        }
    }
}
