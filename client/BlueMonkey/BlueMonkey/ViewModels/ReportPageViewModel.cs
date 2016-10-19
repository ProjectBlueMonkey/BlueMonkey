using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueMonkey.ViewModels
{
    public class ReportPageViewModel : BindableBase
    {
        INavigationService _navigationService;

        public DelegateCommand AddReportCommand => new DelegateCommand(AddReport);

        public ReportPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private void AddReport()
        {
            _navigationService.NavigateAsync("AddReportPage");
        }
    }
}
