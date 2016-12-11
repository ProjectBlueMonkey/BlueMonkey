using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.Business;

namespace BlueMonkey.Model
{
    public class ReferReport : IReferReport
    {
        private readonly ObservableCollection<Report> _reports = new ObservableCollection<Report>();
        public ReadOnlyObservableCollection<Report> Reports { get; }
        public Task Search()
        {
            throw new NotImplementedException();
        }
    }
}
