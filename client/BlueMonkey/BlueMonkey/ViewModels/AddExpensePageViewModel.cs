using BlueMonkey.Business;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using BlueMonkey.Model;

namespace BlueMonkey.ViewModels
{
    public class AddExpensePageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IEditExpense _editExpense;

        public DelegateCommand SaveCommand => new DelegateCommand(Save);
        public DelegateCommand NavigateCommand => new DelegateCommand(Navigate);

        public AddExpensePageViewModel(INavigationService navigationService, IEditExpense editExpense)
        {
            _navigationService = navigationService;
            _editExpense = editExpense;
        }

        private void Navigate()
        {
            _navigationService.NavigateAsync("ReceiptPage");
        }

        private void Save()
        {

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
