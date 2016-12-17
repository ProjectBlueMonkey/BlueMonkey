using BlueMonkey.Business;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMonkey.ExpenseServices.Azure
{
    public class AzureExpenseService : IExpenseService
    {
        private readonly IMobileServiceClient _client;
        private readonly IMobileServiceTable<Expense> _expenseTable;
        private readonly IMobileServiceTable<Report> _reportTable;
        private readonly IMobileServiceTable<ExpenseReceipt> _expenseReceiptTable;

        public AzureExpenseService(IMobileServiceClient client)
        {
            _client = client;
            _expenseTable = _client.GetTable<Expense>();
            _reportTable = _client.GetTable<Report>();
            _expenseReceiptTable = _client.GetTable<ExpenseReceipt>();
        }

        public Task<IEnumerable<Expense>> GetExpensesAsync()
        {
            return _expenseTable.CreateQuery()
                .ToEnumerableAsync();
        }

        public Task<IEnumerable<Expense>> GetExpensesFromReportIdAsync(string reportId)
        {
            return _expenseTable.CreateQuery()
                .Where(x => x.ReportId == reportId)
                .ToEnumerableAsync();
        }

        public Task<Report> GetReportAsync(string reportId)
        {
            return _reportTable.LookupAsync(reportId);
        }

        public Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Report>> GetReportsAsync()
        {
            return _reportTable.CreateQuery()
                .ToEnumerableAsync();
        }

        public Task<IEnumerable<Expense>> GetUnregisteredExpensesAsync()
        {
            return _expenseTable.CreateQuery()
                .Where(x => x.ReportId == null)
                .ToEnumerableAsync();
        }

        public async Task RegisterReportAsync(Report report, IEnumerable<Expense> expenses)
        {
            if (string.IsNullOrEmpty(report.Id))
            {
                await _reportTable.InsertAsync(report);
            }
            else
            {
                await _reportTable.UpdateAsync(report);
                // disconnect current expense
                var currentExpenses = await _expenseTable.CreateQuery()
                    .Where(x => x.ReportId == report.Id)
                    .ToEnumerableAsync();
                foreach (var expense in currentExpenses)
                {
                    expense.ReportId = null;
                }
                await Task.WhenAll(currentExpenses
                    .Select(x => _expenseTable.UpdateAsync(x)));
            }

            foreach (var expense in expenses)
            {
                expense.ReportId = report.Id;
            }

            await Task.WhenAll(expenses
                .Select(x => _expenseTable.UpdateAsync(x)));
        }

        public async Task RegisterExpensesAsync(Expense expense, IEnumerable<ExpenseReceipt> expenseReceipts)
        {
            if (string.IsNullOrEmpty(expense.Id))
            {
                await _expenseTable.InsertAsync(expense);
            }
            else
            {
                await _expenseTable.UpdateAsync(expense);
                // disconnect current expenseReceipt
                var currentExpenseReceipts = await _expenseReceiptTable.CreateQuery()
                    .Where(x => x.ExpenseId == expense.Id)
                    .ToEnumerableAsync();
                foreach (var expenseReceipt in currentExpenseReceipts)
                {
                    expenseReceipt.ExpenseId = null;
                }
                await Task.WhenAll(currentExpenseReceipts.Select(x => _expenseReceiptTable.UpdateAsync(x)));
            }

            foreach (var expenseReceipt in expenseReceipts)
            {
                expenseReceipt.ExpenseId = expense.Id;
            }

            await Task.WhenAll(expenseReceipts.Select(x => _expenseReceiptTable.UpdateAsync(x)));
        }
    }
}
