using BlueMonkey.Business;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueMonkey.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetExpenses();
    }
}
