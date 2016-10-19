using BlueMonkey.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMonkey.Business;

namespace BlueMonkey.Services.Local
{
    public class LocalExpenseService : IExpenseService
    {
        public Task<IEnumerable<Expense>> GetExpenses()
        {
            var list = new List<Expense>();
            for (int x = 0; x < 10; x++)
            {
                list.Add(new Expense() { Name = $"Expense{x}" });
            }

            return Task.FromResult(list.AsEnumerable());
        }
    }
}
