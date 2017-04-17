using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueMonkey.ExpenseServices
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetExpensesAsync();
        Task<IEnumerable<Expense>> GetExpensesFromReportIdAsync(string reportId);
        Task<IEnumerable<Expense>> GetUnregisteredExpensesAsync();
        Task<IEnumerable<ReportSummary>> GetReportSummariesAsync();
        Task<Report> GetReportAsync(string reportId);
        Task<Expense> GetExpenseAsync(string expenseId);
        Task<IEnumerable<ExpenseReceipt>> GetExpenseReceiptsAsync(string expenseId);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task RegisterReportAsync(Report report, IEnumerable<Expense> expenses);
        Task RegisterExpensesAsync(Expense expense, IEnumerable<ExpenseReceipt> expenseReceipts);
    }
}
