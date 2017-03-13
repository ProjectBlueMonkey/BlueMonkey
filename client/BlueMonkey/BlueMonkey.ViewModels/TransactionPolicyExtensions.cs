using System;
using System.Reactive.Disposables;
using BlueMonkey.Transaction;

namespace BlueMonkey.ViewModels
{
    public static class TransactionPolicyExtensions
    {
        public static void AddTo(this ITransactionPolicy transactionPolicy, CompositeDisposable disposable)
        {
            disposable.Add(new TransactionPolicyDisposer(transactionPolicy));
        }

        private class TransactionPolicyDisposer : IDisposable
        {
            private ITransactionPolicy _transactionPolicy;
            public TransactionPolicyDisposer(ITransactionPolicy transactionPolicy)
            {
                _transactionPolicy = transactionPolicy;
            }
            public void Dispose()
            {
                _transactionPolicy?.Complete();
                _transactionPolicy = null;
            }
        }
    }
}
