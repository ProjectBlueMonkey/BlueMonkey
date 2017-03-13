using System;

namespace BlueMonkey.Transaction
{
    public interface ITransactionPolicy
    {
        event EventHandler Completed;

        void Complete();
    }
}