using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.Business;

namespace BlueMonkey.Model
{
    public interface IEditReport : INotifyPropertyChanged
    {
        /// <summary>
        /// Name of Report.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Date of Report.
        /// </summary>
        DateTime Date { get; set; }
        /// <summary>
        /// Selectable Expenses.
        /// </summary>
        ReadOnlyObservableCollection<SelectableExpense> SelectableExpenses { get; }
        /// <summary>
        /// Initialize for new registration.
        /// </summary>
        /// <returns></returns>
        Task InitializeForNewReportAsync();
        /// <summary>
        /// Initialize for update report.
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        Task InitializeForUpdateReportAsync(string reportId);
        /// <summary>
        /// Register or Update Report.
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}
