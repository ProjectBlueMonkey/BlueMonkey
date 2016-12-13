using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.Business;

namespace BlueMonkey.ExpenceServices
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetExpensesAsync();
        Task<IEnumerable<Report>> GetReportsAsync();
        Task<Report> GetReportAsync(string reportId);
        Task RegisterReport(Report report, IEnumerable<Expense> expenses);
    }
}
