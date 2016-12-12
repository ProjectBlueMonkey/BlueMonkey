using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.Business;
using BlueMonkey.ExpenceServices;

namespace BlueMonkey.Model
{
    public class ReferReport : IReferReport
    {
        private readonly IExpenseService _expenseService;
        private readonly ObservableCollection<Report> _reports = new ObservableCollection<Report>();
        public ReadOnlyObservableCollection<Report> Reports { get; }
        public ReferReport(IExpenseService expenseService)
        {
            _expenseService = expenseService;
            Reports = new ReadOnlyObservableCollection<Report>(_reports);
        }

        public async Task SearchAsync()
        {
            _reports.Clear();
            foreach (var report in await _expenseService.GetReportsAsync())
            {
                _reports.Add(report);
            }
        }
    }
}
