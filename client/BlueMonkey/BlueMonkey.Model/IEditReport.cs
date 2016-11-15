using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.Business;

namespace BlueMonkey.Model
{
    public interface IEditReport : INotifyPropertyChanged
    {
        Report Report { get; }
        /// <summary>
        /// Initialize for new registration.
        /// </summary>
        /// <returns></returns>
        Task InitializeForNewReportAsync();
    }
}
