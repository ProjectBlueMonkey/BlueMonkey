using BlueMonkey.Business;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueMonkey.ViewModels
{
    public class AddExpensePageViewModel : BindableBase, INavigationAware
    {
        INavigationService _navigationService;

        private string _text;
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        public DelegateCommand SaveCommand => new DelegateCommand(Save);
        public DelegateCommand NavigateCommand => new DelegateCommand(Navigate);

        public AddExpensePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
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
            if (parameters.ContainsKey("expense"))
            {
                var exsp = parameters["expense"] as Expense;
                if (exsp != null)
                {
                    Text = exsp.Name;
                }
                else
                {
                    Text = "You're adding a new one";
                }
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}
