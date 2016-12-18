using BlueMonkey.Business;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using BlueMonkey.Model;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace BlueMonkey.ViewModels
{
    public class AddExpensePageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IEditExpense _editExpense;

        public ReadOnlyReactiveProperty<Expense> Expense { get; }
        public ReadOnlyReactiveProperty<IEnumerable<string>> Categories { get; }

        public DelegateCommand SaveCommand => new DelegateCommand(Save);
        public DelegateCommand NavigateCommand => new DelegateCommand(Navigate);

        public AddExpensePageViewModel(INavigationService navigationService, IEditExpense editExpense)
        {
            _navigationService = navigationService;
            _editExpense = editExpense;

            Expense = _editExpense.ObserveProperty(x => x.Expense).ToReadOnlyReactiveProperty();
            Categories = _editExpense.ObserveProperty(x => x.Categories)
                .Select(x => x.Select(category => category.Name)).ToReadOnlyReactiveProperty();
        }

        private void Navigate()
        {
            _navigationService.NavigateAsync("ReceiptPage");
        }

        private async void Save()
        {
            await _editExpense.SaveAsync();
            await _navigationService.GoBackAsync();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public async void OnNavigatingTo(NavigationParameters parameters)
        {
            await _editExpense.InitializeAsync();
        }
    }
}
