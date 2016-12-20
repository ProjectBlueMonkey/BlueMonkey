using System;

namespace BlueMonkey.Model
{
    public interface ITransactionPolicy
    {
        event EventHandler Completed;

        void Complete();
    }
}