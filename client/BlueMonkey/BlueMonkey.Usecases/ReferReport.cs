using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BlueMonkey.ExpenseServices;

namespace BlueMonkey.Usecases
{
    public class ReferReport : IReferReport
    {
        private readonly IExpenseService _expenseService;
        private readonly ObservableCollection<ReportSummary> _reports = new ObservableCollection<ReportSummary>();
        public ReadOnlyObservableCollection<ReportSummary> ReportSummaries { get; }
        public ReferReport(IExpenseService expenseService)
        {
            _expenseService = expenseService;
            ReportSummaries = new ReadOnlyObservableCollection<ReportSummary>(_reports);
        }

        public async Task SearchAsync()
        {
            _reports.Clear();
            foreach (var report in await _expenseService.GetReportSummariesAsync())
            {
                _reports.Add(report);
            }
        }
    }
}
