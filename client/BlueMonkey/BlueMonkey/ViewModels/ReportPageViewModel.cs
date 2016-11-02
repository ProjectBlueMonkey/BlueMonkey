using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using BlueMonkey.Model;

namespace BlueMonkey.ViewModels
{
    public class ReportPageViewModel : BindableBase
    {
        /// <summary>
        /// Model to manage the registration and change of the report.
        /// </summary>
        private readonly IEditReport _editReport;
        /// <summary>
        /// It is a screen transition services provided by the Prism.
        /// </summary>
        private readonly INavigationService _navigationService;
        /// <summary>
        /// Save Report Command.
        /// </summary>
        public DelegateCommand SaveReportCommand => null;
        /// <summary>
        /// Initialize Instance.
        /// </summary>
        /// <param name="editReport"></param>
        /// <param name="navigationService"></param>
        public ReportPageViewModel(IEditReport editReport, INavigationService navigationService)
        {
            _editReport = editReport;
            _navigationService = navigationService;
        }
    }
}
