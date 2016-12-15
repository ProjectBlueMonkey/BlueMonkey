﻿using System;
using System.Collections;
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
                // Free expenses.
                _expenses.Add(
                    new Expense()
                    {
                        Id = $"ExpenseId_x_{i}",
                        Name = $"Expense_x_{i}",
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
                            Name = $"Expense_{i}_{j}",
                            Amount = random.Next(1000, 20000),
                            ReportId = report.Id
                        });

                }
            }
        }

        public Task<IEnumerable<Expense>> GetExpensesAsync()
        {
            return Task.FromResult(_expenses.AsEnumerable());
        }

        public Task<IEnumerable<Expense>> GetExpensesFromReportIdAsync(string reportId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Expense>> GetUnregisteredExpensesAsync()
        {
            throw new NotImplementedException();
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
