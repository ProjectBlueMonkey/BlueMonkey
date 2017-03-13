using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BlueMonkey.Model
{
    /// <summary>
    /// Provide use cases that reference reports.
    /// </summary>
    public interface IReferReport
    {
        ReadOnlyObservableCollection<Report> Reports { get; }

        Task SearchAsync();
    }
}
