using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueMonkey.MobileApp.DataObjects
{
    public class Expense : EntityData
    {
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Location { get; set; }
        public string Note { get; set; }
        public string ReportId { get; set; }
        public Report Report { get; set; }
        public string UserId { get; set; }
    }
}