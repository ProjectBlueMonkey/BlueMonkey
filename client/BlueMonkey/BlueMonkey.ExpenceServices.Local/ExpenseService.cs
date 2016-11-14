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
        public Task<IEnumerable<Expense>> GetExpensesAsync()
        {
            var list = new List<Expense>();
            for (int x = 0; x < 10; x++)
            {
                list.Add(new Expense() { Name = $"Expense{x}" });
            }

            return Task.FromResult(list.AsEnumerable());
        }

        public Task<IEnumerable<Report>> GetReportsAsync()
        {
            var list = new List<Report>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(
                    new Report
                    {
                        Id = $"ReportId{i}",
                        Name = $"Report{i}",
                        Date = DateTime.Today - TimeSpan.FromDays(20 - i)
                    });
            }

            return Task.FromResult(list.AsEnumerable());
        }
    }
}
