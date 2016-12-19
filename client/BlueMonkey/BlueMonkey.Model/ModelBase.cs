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
        public event EventHandler Closed;

        public void Close()
        {
            Closed?.Invoke(this, EventArgs.Empty);
        }
    }
}
