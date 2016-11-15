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
        /// Target report.
        /// </summary>
        Report Report { get; }
        /// <summary>
        /// Selectable Expenses.
        /// </summary>
        ReadOnlyObservableCollection<SelectableExpense> SelectableExpenses { get; }
        /// <summary>
        /// Initialize for new registration.
        /// </summary>
        /// <returns></returns>
        Task InitializeForNewReportAsync();
    }
}
