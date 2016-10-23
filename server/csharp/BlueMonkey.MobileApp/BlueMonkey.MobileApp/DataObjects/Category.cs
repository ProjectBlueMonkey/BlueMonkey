using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueMonkey.MobileApp.DataObjects
{
    public class Category : EntityData
    {
        public string Name { get; set; }
        public int SortOrder { get; set; }
    }
}