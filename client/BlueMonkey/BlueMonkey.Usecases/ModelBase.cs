using System;
using BlueMonkey.Transaction;
using Prism.Mvvm;

namespace BlueMonkey.Usecases
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
