using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BlueMonkey.Business;
using BlueMonkey.ExpenceServices;
using BlueMonkey.Model;
using Prism.Navigation;
using Reactive.Bindings;

namespace BlueMonkey.ViewModels
{
    public class ReportListPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IReferReport _referReport;
        public ReadOnlyReactiveCollection<Report> Reports { get; }
        /// <summary>
        /// Add New Report Navigation Command.
        /// </summary>
        public DelegateCommand AddReportCommand => new DelegateCommand(AddReport);

        /// <summary>
        /// Initialize Instance
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="referReport"></param>
        public ReportListPageViewModel(INavigationService navigationService, IReferReport referReport)
        {
            _navigationService = navigationService;
            _referReport = referReport;
            Reports = _referReport.Reports.ToReadOnlyReactiveCollection(ApplicationEnvironments.DefaultScheduler);
        }

        /// <summary>
        /// Navigation ReportPage.
        /// </summary>
        private void AddReport()
        {
            var navigationParameter = new NavigationParameters();
            navigationParameter[ReportPageViewModel.ReportIdKey] = null;
            _navigationService.NavigateAsync("ReportPage", navigationParameter);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            _referReport.Search();
        }
    }
}
