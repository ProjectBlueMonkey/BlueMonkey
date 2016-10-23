using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueMonkey.MobileApp.DataObjects
{
    public class Report : EntityData
    {
        public string Name { get; set; }
        public DateTimeOffset Date { get; set; }
        public string UserId { get; set; }
    }
}