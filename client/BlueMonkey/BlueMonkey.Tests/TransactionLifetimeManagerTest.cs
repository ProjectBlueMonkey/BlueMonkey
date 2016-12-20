using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.Model;
using Microsoft.Practices.Unity;
using Xunit;

namespace BlueMonkey.Tests
{
    public class TransactionLifetimeManagerTest
    {
        [Fact]
        public void LifetimeManagement()
        {
            var container = new UnityContainer();
            container.RegisterType<ManagedModel>(new TransactionLifetimeManager());

            var model1 = container.Resolve<ManagedModel>();
            Assert.NotNull(model1);

            var model2 = container.Resolve<ManagedModel>();
            Assert.Equal(model1, model2);

            model1.Complete();

            var model3 = container.Resolve<ManagedModel>();
            Assert.NotEqual(model1, model3);

            var model4 = container.Resolve<ManagedModel>();
            Assert.Equal(model3, model4);
        }

        [Fact]
        public void Disposable()
        {
            var container = new UnityContainer();
            container.RegisterType<DisposableModel>(new TransactionLifetimeManager());

            var model = container.Resolve<DisposableModel>();

            Assert.NotNull(model);
            model.Complete();

            Assert.True(model.IsDisposed);
        }
    }

    public class ManagedModel : ITransactionPolicy
    {
        public event EventHandler Completed;
        public void Complete()
        {
            Completed?.Invoke(this, EventArgs.Empty);
        }
    }

    public class DisposableModel : ITransactionPolicy, IDisposable
    {
        public event EventHandler Completed;
        public bool IsDisposed { get; private set; }
        public void Complete()
        {
            Completed?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            IsDisposed = true;
        }
    }
}
