using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.Business;

namespace BlueMonkey.Model
{
    public interface IReferExpense
    {
        ReadOnlyObservableCollection<Expense> Expenses { get; }

        Task SearchAsync();
    }
}
