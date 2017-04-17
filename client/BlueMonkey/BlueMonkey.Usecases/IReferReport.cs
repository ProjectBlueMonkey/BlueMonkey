using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BlueMonkey.Usecases
{
    /// <summary>
    /// Provide use cases that reference reports.
    /// </summary>
    public interface IReferReport
    {
        ReadOnlyObservableCollection<ReportSummary> ReportSummaries { get; }

        Task SearchAsync();
    }
}
