using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Logging;
using System;

namespace BlueMonkey.ExpenseServices.Azure
{
    public class AzureExpenseService : IExpenseService
    {
        private readonly IMobileServiceClient _client;
        private readonly IMobileServiceTable<Expense> _expenseTable;
        private readonly IMobileServiceTable<Report> _reportTable;
        private readonly IMobileServiceTable<ExpenseReceipt> _expenseReceiptTable;
        private readonly IMobileServiceTable<Category> _categoryTable;

        public AzureExpenseService(IMobileServiceClient client)
        {
            _client = client;
            _expenseTable = _client.GetTable<Expense>();
            _reportTable = _client.GetTable<Report>();
            _expenseReceiptTable = _client.GetTable<ExpenseReceipt>();
            _categoryTable = _client.GetTable<Category>();
        }

        public async Task<IEnumerable<Expense>> GetExpensesAsync()
        {
            return (await _expenseTable.CreateQuery()
                .ToEnumerableAsync())
                .ToArray();
        }

        public async Task<IEnumerable<Expense>> GetExpensesFromReportIdAsync(string reportId)
        {
            return (await _expenseTable.CreateQuery()
                .Where(x => x.ReportId == reportId)
                .ToEnumerableAsync())
                .ToArray();
        }

        public Task<Report> GetReportAsync(string reportId)
        {
            return _reportTable.LookupAsync(reportId);
        }

        public Task<Expense> GetExpenseAsync(string expenseId)
        {
            return _expenseTable.LookupAsync(expenseId);
        }

        public async Task<IEnumerable<ExpenseReceipt>> GetExpenseReceiptsAsync(string expenseId)
        {
            return (await _expenseReceiptTable.CreateQuery().Where(x => x.ExpenseId == expenseId).ToEnumerableAsync())
                .ToArray();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return (await _categoryTable.CreateQuery().ToEnumerableAsync()).ToArray();
        }

        public async Task<IEnumerable<ReportSummary>> GetReportSummariesAsync()
        {
            throw new NotImplementedException();
            //return (await _reportTable.CreateQuery()
            //    .ToEnumerableAsync())
            //    .ToArray();
        }

        public async Task<IEnumerable<Expense>> GetUnregisteredExpensesAsync()
        {
            return (await _expenseTable.CreateQuery()
                .Where(x => x.ReportId == null)
                .ToEnumerableAsync())
                .ToArray();
        }

        public async Task RegisterReportAsync(Report report, IEnumerable<Expense> expenses)
        {
            if (string.IsNullOrEmpty(report.Id))
            {
                await _reportTable.InsertAsync(report);
            }
            else
            {
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

                await _reportTable.UpdateAsync(report);
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

            await Task.WhenAll(expenseReceipts.Select(x =>
            {
                if (string.IsNullOrEmpty(x.Id))
                {
                    return _expenseReceiptTable.InsertAsync(x);
                }
                else
                {
                    return _expenseReceiptTable.UpdateAsync(x);
                }
            }));
        }
    }
}
