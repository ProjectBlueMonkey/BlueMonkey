using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.Business;

namespace BlueMonkey.ExpenseServices
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetExpensesAsync();
        Task<IEnumerable<Expense>> GetExpensesFromReportIdAsync(string reportId);
        Task<IEnumerable<Expense>> GetUnregisteredExpensesAsync();
        Task<IEnumerable<Report>> GetReportsAsync();
        Task<Report> GetReportAsync(string reportId);
        Task RegisterReportAsync(Report report, IEnumerable<Expense> expenses);
        Task RegisterExpensesAsync(Expense expense, IEnumerable<ExpenseReceipt> expenseReceipts);
    }
}
