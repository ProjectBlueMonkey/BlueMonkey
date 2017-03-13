using System;
using Microsoft.Practices.Unity;
using Xunit;

namespace BlueMonkey.Transaction.Unity.Tests
{
    public class TransactionLifetimeManagerTest
    {
        [Fact]
        public void LifetimeManagement()
        {
            // Setup container.
            var container = new UnityContainer();
            container.RegisterType<ManagedModel>(new TransactionLifetimeManager());

            // The same instance will be retrieved until complete is called.
            var model1 = container.Resolve<ManagedModel>();
            Assert.NotNull(model1);

            var model2 = container.Resolve<ManagedModel>();
            Assert.Same(model1, model2); // Assert#Same is the same meaning as ReferenceEquals.

            // Transaction complete.
            model1.Complete();

            // The new instance will be retrieved.
            var model3 = container.Resolve<ManagedModel>();
            Assert.NotEqual(model1, model3);

            var model4 = container.Resolve<ManagedModel>();
            Assert.Same(model3, model4);
        }

        /// <summary>
        /// In the case of a class that implements IDisposable, confirm that the Dispose method is called when deleting from the container.
        /// </summary>
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
