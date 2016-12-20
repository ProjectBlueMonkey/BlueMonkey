using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.Model;
using Microsoft.Practices.Unity;

namespace BlueMonkey
{
    public class TransactionLifetimeManager : LifetimeManager
    {
        private object _value;
        public override object GetValue()
        {
            return _value;
        }

        public override void SetValue(object newValue)
        {
            _value = newValue;
            var transactionObject = _value as ITransactionPolicy;
            if (transactionObject != null)
            {
                transactionObject.Completed += TransactionOnCompleted;
            }
        }

        private void TransactionOnCompleted(object sender, EventArgs eventArgs)
        {
            var transactionObject = (ITransactionPolicy) sender;
            transactionObject.Completed -= TransactionOnCompleted;
            RemoveValue();
        }

        public override void RemoveValue()
        {
            (_value as IDisposable)?.Dispose();
            _value = null;
        }
    }
}
