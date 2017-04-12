using Prism.Mvvm;
using System;
using BlueMonkey.Usecases;
using Prism.Navigation;
using Reactive.Bindings;

namespace BlueMonkey.ViewModels
{
    public class ReportListPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IReferReport _referReport;
        public ReadOnlyReactiveCollection<ReportSummary> ReportSummaries { get; }
        /// <summary>
        /// Add New Report Navigation Command.
        /// </summary>
        public ReactiveCommand AddReportCommand { get; }

        public ReactiveCommand<Report> UpdateReportCommand { get; }

        /// <summary>
        /// Initialize Instance
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="referReport"></param>
        public ReportListPageViewModel(INavigationService navigationService, IReferReport referReport)
        {
            _navigationService = navigationService;
            _referReport = referReport;
            ReportSummaries = _referReport.ReportSummaries.ToReadOnlyReactiveCollection();

            AddReportCommand = new ReactiveCommand();
            AddReportCommand.Subscribe(_ => AddReport());

            UpdateReportCommand = new ReactiveCommand<Report>();
            UpdateReportCommand.Subscribe(UpdateReport);
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
            var navigationParameter = new NavigationParameters
            {
                { ReportPageViewModel.ReportIdKey, reportId }
            };
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
