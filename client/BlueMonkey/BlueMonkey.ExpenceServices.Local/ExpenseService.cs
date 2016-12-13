using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.Business;

namespace BlueMonkey.ExpenceServices.Local
{
    public class ExpenseService : IExpenseService
    {
        private readonly List<Expense> _expenses = new List<Expense>();
        private readonly List<Report> _reports = new List<Report>();

        public ExpenseService()
        {
            var random = new Random();
            for (int i = 0; i < 10; i++)
            {
                _expenses.Add(
                    new Expense()
                    {
                        Id = $"ExpenseId{i}",
                        Name = $"Expense{i}",
                        Amount = random.Next(1000, 20000)
                    });
            }

            for (int i = 0; i < 10; i++)
            {
                _reports.Add(
                    new Report
                    {
                        Id = $"ReportId{i}",
                        Name = $"Report{i}",
                        Date = DateTime.Today - TimeSpan.FromDays(20 - i)
                    });
            }
        }

        public Task<IEnumerable<Expense>> GetExpensesAsync()
        {
            return Task.FromResult(_expenses.AsEnumerable());
        }

        public Task<IEnumerable<Report>> GetReportsAsync()
        {
            return Task.FromResult(_reports.AsEnumerable());
        }

        public Task<Report> GetReportAsync(string reportId)
        {
            return Task.FromResult(_reports.SingleOrDefault(x => x.Id == reportId));
        }

        public Task RegisterReport(Report report, IEnumerable<Expense> expenses)
        {
            return Task.Run(() =>
            {
                report.Id = $"ReportId{_reports.Count}";
                _reports.Add(report);
                foreach (var expense in expenses)
                {
                    _expenses.Single(x => x.Id == expense.Id).ReportId = report.Id;
                }
            });
        }
    }
}
