using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMonkey.ExpenseServices.Local
{
    public class ExpenseService : IExpenseService
    {
        private readonly List<Expense> _expenses = new List<Expense>();
        private readonly List<Report> _reports = new List<Report>();
        private readonly List<Category> _categories = new List<Category>();
        private readonly List<ExpenseReceipt> _expenseReceipts = new List<ExpenseReceipt>();

        public ExpenseService()
        {
            var random = new Random();
            for (int i = 0; i < 10; i++)
            {
                // Free expenses.
                _expenses.Add(
                    new Expense()
                    {
                        Id = $"ExpenseId_x_{i}",
                        Location = $"Expense_x_{i}",
                        Amount = random.Next(1000, 20000)
                    });
            }

            for (int i = 0; i < 10; i++)
            {
                var report = new Report
                {
                    Id = $"ReportId{i}",
                    Name = $"Report{i}",
                    Date = DateTime.Today - TimeSpan.FromDays(20 - i)
                };
                _reports.Add(report);
                for (int j = 0; j < 3; j++)
                {
                    // registerd expenses.
                    _expenses.Add(
                        new Expense()
                        {
                            Id = $"ExpenseId_{i}_{j}",
                            Location = $"Expense_{i}_{j}",
                            Amount = random.Next(1000, 20000),
                            ReportId = report.Id
                        });

                }
                _categories.Add(
                    new Category
                    {
                        Id = $"CategoryId_{i}",
                        Name = $"Category{i}",
                        SortOrder = i
                    });
            }
        }

        public Task<IEnumerable<Expense>> GetExpensesAsync()
        {
            return Task.FromResult(_expenses.AsEnumerable());
        }

        public Task<IEnumerable<Expense>> GetExpensesFromReportIdAsync(string reportId)
        {
            return Task.FromResult(_expenses.Where(expense => expense.ReportId == reportId));
        }

        public Task<IEnumerable<Expense>> GetUnregisteredExpensesAsync()
        {
            return Task.FromResult(_expenses.Where(expense => expense.ReportId == null));
        }

        public Task<IEnumerable<ReportSummary>> GetReportSummariesAsync()
        {
            var reportSummaries =
                _reports
                    .Join(_expenses, report => report.Id, expense => expense.ReportId, (report, expense) => new {report, expense})
                    .GroupBy(g => new
                    {
                        ReportId = g.report.Id,
                    })
                    .Select(x => new ReportSummary
                    {
                        Id = x.First().report.Id,
                        Name = x.First().report.Name,
                        Date = x.First().report.Date,
                        UserId = x.First().report.UserId,
                        TotalAmount = x.Sum(sum => sum.expense.Amount)
                    });
            return Task.FromResult(reportSummaries);
        }

        public Task<Report> GetReportAsync(string reportId)
        {
            return Task.FromResult(_reports.SingleOrDefault(x => x.Id == reportId));
        }

        public Task<Expense> GetExpenseAsync(string expenseId)
        {
            return Task.FromResult(_expenses.SingleOrDefault(x => x.Id == expenseId));
        }

        public Task<IEnumerable<ExpenseReceipt>> GetExpenseReceiptsAsync(string expenseId)
        {
            return Task.FromResult(
                new []
                {
                    new ExpenseReceipt
                    {
                        Id = "ExpenseReceiptId",
                        ExpenseId = expenseId,
                        ReceiptUri = "https://www.bing.com/test.jpg"
                    }
                }.AsEnumerable());
        }

        public Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return Task.FromResult(_categories.AsEnumerable());
        }

        public Task RegisterReportAsync(Report report, IEnumerable<Expense> expenses)
        {
            return Task.Run(() =>
            {
                if (report.Id == null)
                {
                    // Create new report.
                    report.Id = $"ReportId{_reports.Count}";
                    _reports.Add(report);
                }
                else
                {
                    // Update report.
                    var originalReport = _reports.Single(x => x.Id == report.Id);
                    originalReport.Name = report.Name;
                    originalReport.Date = report.Date;
                    foreach (var expense in _expenses.Where(x => x.ReportId == report.Id))
                    {
                        expense.ReportId = null;
                    }
                }
                foreach (var expense in expenses)
                {
                    _expenses.Single(x => x.Id == expense.Id).ReportId = report.Id;
                }
            });
        }

        public Task RegisterExpensesAsync(Expense expense, IEnumerable<ExpenseReceipt> expenseReceipts)
        {
            return Task.Run(() =>
            {
                if (expense.Id == null)
                {
                    expense.Id = $"ExpenseId_x_{_expenses.Count}";
                    _expenses.Add(expense);
                    foreach (var expenseReceipt in expenseReceipts)
                    {
                        expenseReceipt.ExpenseId = expense.Id;
                        _expenseReceipts.Add(expenseReceipt);
                    }
                }
                else
                {
                    var original = _expenses.Single(x => x.Id == expense.Id);
                    original.Amount = expense.Amount;
                    original.CategoryId = expense.CategoryId;
                    original.Date = expense.Date;
                    original.Location = expense.Location;
                    original.Note = expense.Note;

                    var originalExpenseReceipt = _expenseReceipts.SingleOrDefault(x => x.ExpenseId == expense.Id);
                    if (originalExpenseReceipt != null)
                    {
                        _expenseReceipts.Remove(originalExpenseReceipt);
                    }
                    foreach (var expenseReceipt in expenseReceipts)
                    {
                        expenseReceipt.ExpenseId = expense.Id;
                        _expenseReceipts.Add(expenseReceipt);
                    }
                }
            });
        }
    }
}
