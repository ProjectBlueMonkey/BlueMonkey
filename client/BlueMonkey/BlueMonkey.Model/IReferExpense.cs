using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BlueMonkey.Model
{
    public interface IReferExpense
    {
        ReadOnlyObservableCollection<Expense> Expenses { get; }

        Task SearchAsync();
    }
}
