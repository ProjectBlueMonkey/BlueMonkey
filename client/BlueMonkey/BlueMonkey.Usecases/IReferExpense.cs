using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BlueMonkey.Usecases
{
    public interface IReferExpense
    {
        ReadOnlyObservableCollection<Expense> Expenses { get; }

        Task SearchAsync();
    }
}
