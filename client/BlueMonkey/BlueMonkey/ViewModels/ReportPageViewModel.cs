using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using BlueMonkey.Business;
using BlueMonkey.Model;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace BlueMonkey.ViewModels
{
    public class ReportPageViewModel : BindableBase, INavigationAware
    {
        public static readonly string ReportIdKey = "reportId";

        /// <summary>
        /// Model to manage the registration and change of the report.
        /// </summary>
        private readonly IEditReport _editReport;

        /// <summary>
        /// It is a screen transition services provided by the Prism.
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// Name of Report.
        /// </summary>
        public ReactiveProperty<string> Name { get; }
        /// <summary>
        /// Date of Report.
        /// </summary>
        public ReactiveProperty<DateTime> Date { get; }
        /// <summary>
        /// Backing store of Expenses.
        /// </summary>
        private IEnumerable<SelectableExpense> _expenses;
        /// <summary>
        /// Selected Expenses.
        /// </summary>
        public IEnumerable<SelectableExpense> Expenses
        {
            get { return _expenses; }
            set { SetProperty(ref _expenses, value); }
        }

        public DelegateCommand InitializeCommand { get; }

        /// <summary>
        /// Navigate to ExpenseSelectionPage Command.
        /// </summary>
        public DelegateCommand NavigateExpenseSelectionCommand { get; }

        /// <summary>
        /// Save Report Command.
        /// </summary>
        public DelegateCommand SaveReportCommand { get; }

        /// <summary>
        /// Initialize Instance.
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="editReport"></param>
        public ReportPageViewModel(INavigationService navigationService, IEditReport editReport)
        {
            _navigationService = navigationService;
            _editReport = editReport;
            Name = editReport.ToReactivePropertyAsSynchronized(x => x.Name);
            Date = editReport.ToReactivePropertyAsSynchronized(x => x.Date);

            InitializeCommand = new DelegateCommand(() =>
            {
                Expenses = editReport.SelectableExpenses.Where(x => x.IsSelected);
            });

            NavigateExpenseSelectionCommand = new DelegateCommand(() =>
            {
                _navigationService.NavigateAsync("ExpenseSelectionPage");
            });

            SaveReportCommand = new DelegateCommand(() =>
            {
                _editReport.Save();
                _navigationService.GoBackAsync();
            });
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey(ReportIdKey))
            {
                var reportId = parameters[ReportIdKey] as string;
                if (reportId == null)
                {
                    // Case : New Report.
                    await _editReport.InitializeForNewReportAsync();
                }
                else
                {
                    // Case : Update Report.

                }
            }
        }
    }
}
