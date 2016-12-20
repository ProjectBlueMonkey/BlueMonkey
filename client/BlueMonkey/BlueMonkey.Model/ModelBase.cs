using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace BlueMonkey.Model
{
    public class ModelBase : BindableBase, ITransactionPolicy
    {
        public event EventHandler Completed;

        public void Complete()
        {
            Completed?.Invoke(this, EventArgs.Empty);
        }
    }
}
