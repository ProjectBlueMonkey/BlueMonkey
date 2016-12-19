using System;

namespace BlueMonkey.Model
{
    public interface ITransactionPolicy
    {
        event EventHandler Closed;

        void Close();
    }
}