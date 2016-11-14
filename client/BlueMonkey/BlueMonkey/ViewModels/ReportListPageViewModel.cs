using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BlueMonkey.Business;
using BlueMonkey.ExpenceServices;
using Prism.Navigation;

namespace BlueMonkey.ViewModels
{
    public class ReportListPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IExpenseService _expenseService;
        public ObservableCollection<Report> Reports { get; } = new ObservableCollection<Report>();
        /// <summary>
        /// Add New Report Navigation Command.
        /// </summary>
        public DelegateCommand AddReportCommand => new DelegateCommand(AddReport);

        /// <summary>
        /// Initialize Instance
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="expenseService"></param>
        public ReportListPageViewModel(INavigationService navigationService, IExpenseService expenseService)
        {
            _navigationService = navigationService;
            _expenseService = expenseService;
        }

        /// <summary>
        /// Navigation ReportPage.
        /// </summary>
        private void AddReport()
        {
            _navigationService.NavigateAsync("ReportPage");
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            Reports.Clear();
            foreach (var report in await _expenseService.GetReportsAsync())
            {
                Reports.Add(report);
            }
        }
    }
}
