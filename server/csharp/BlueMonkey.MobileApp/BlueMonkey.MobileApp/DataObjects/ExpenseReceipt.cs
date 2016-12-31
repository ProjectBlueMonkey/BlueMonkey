using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueMonkey.MobileApp.DataObjects
{
    public class ExpenseReceipt : EntityData
    {
        public string ExpenseId { get; set; }
        public string ReceiptUri { get; set; }
        public string UserId { get; set; }
    }
}