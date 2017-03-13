using System;
using Microsoft.Practices.Unity;

namespace BlueMonkey.Transaction.Unity
{
    /// <summary>
    /// LifetimeManager delete the managed objects from the container during the Completed event is fired
    /// when a managed class that implements the ITransactionPolicy.
    /// If the managed object implements IDisposable, it also invokes dispose when it is removed from the container.
    /// </summary>
    public class TransactionLifetimeManager : LifetimeManager
    {
        /// <summary>
        /// Managed object
        /// </summary>
        private object _value;
        
        /// <summary>
        /// Get a managed object.
        /// </summary>
        /// <returns></returns>
        public override object GetValue()
        {
            return _value;
        }

        /// <summary>
        /// Set new managed object.
        /// </summary>
        /// <param name="newValue">new managed object.</param>
        public override void SetValue(object newValue)
        {
            _value = newValue;
            var transactionObject = _value as ITransactionPolicy;
            if (transactionObject != null)
            {
                // If the managed object implements ITransactionPolicy, Subscribe the Completed event.
                transactionObject.Completed += TransactionOnCompleted;
            }
        }

        /// <summary>
        /// Handle Completed event and delete object from container.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void TransactionOnCompleted(object sender, EventArgs eventArgs)
        {
            RemoveValue();
        }

        /// <summary>
        /// Delete the managed object from the container.
        /// </summary>
        public override void RemoveValue()
        {
            var transactionObject = _value as ITransactionPolicy;
            if (transactionObject != null) {
                transactionObject.Completed -= TransactionOnCompleted;
            }
            (_value as IDisposable)?.Dispose();
            _value = null;
        }
    }
}
