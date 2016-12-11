using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.Business;

namespace BlueMonkey.Model
{
    /// <summary>
    /// Provide use cases that reference reports.
    /// </summary>
    public interface IReferReport
    {
        ReadOnlyObservableCollection<Report> Reports { get; }

        Task Search();
    }
}
