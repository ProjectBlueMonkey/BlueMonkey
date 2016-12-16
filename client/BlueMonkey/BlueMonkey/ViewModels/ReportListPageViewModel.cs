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

        public DelegateCommand<Report> UpdateReportCommand => new DelegateCommand<Report>(UpdateReport);

        /// <summary>
        /// Initialize Instance
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="referReport"></param>
        public ReportListPageViewModel(INavigationService navigationService, IReferReport referReport)
        {
            _navigationService = navigationService;
            _referReport = referReport;
            Reports = _referReport.Reports.ToReadOnlyReactiveCollection();
        }

        /// <summary>
        /// Navigation ReportPage when create Report.
        /// </summary>
        private void AddReport()
        {
            NavigateReportPage(null);
        }
        /// <summary>
        /// Navigation ReportPage when update Report.
        /// </summary>
        /// <param name="selectedReport"></param>
        private void UpdateReport(Report selectedReport)
        {
            NavigateReportPage(selectedReport.Id);
        }

        /// <summary>
        /// Navigation ReportPage.
        /// </summary>
        private void NavigateReportPage(string reportId)
        {
            var navigationParameter = new NavigationParameters();
            navigationParameter[ReportPageViewModel.ReportIdKey] = reportId;
            _navigationService.NavigateAsync("ReportPage", navigationParameter);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            await _referReport.SearchAsync();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}
