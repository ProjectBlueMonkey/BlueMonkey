using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace BlueMonkey.ViewModels
{
    public class ReportListPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        /// <summary>
        /// Add New Report Navigation Command.
        /// </summary>
        public DelegateCommand AddReportCommand => new DelegateCommand(AddReport);

        /// <summary>
        /// Initialize Instance
        /// </summary>
        /// <param name="navigationService"></param>
        public ReportListPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        /// <summary>
        /// Navigation ReportPage.
        /// </summary>
        private void AddReport()
        {
            _navigationService.NavigateAsync("ReportPage");
        }
    }
}
