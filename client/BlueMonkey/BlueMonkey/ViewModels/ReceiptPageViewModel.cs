using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using BlueMonkey.Model;
using Prism.Navigation;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Xamarin.Forms;

namespace BlueMonkey.ViewModels
{
    public class ReceiptPageViewModel : BindableBase, IDestructible
    {
        private readonly IEditExpense _editExpense;
        private CompositeDisposable Disposable { get; } = new CompositeDisposable();
        public ReadOnlyReactiveProperty<ImageSource> Receipt { get; }
        public AsyncReactiveCommand PickPhotoAsyncCommand { get; }
        public AsyncReactiveCommand TakePhotoAsyncCommand { get; }

        public ReceiptPageViewModel(IEditExpense editExpense)
        {
            _editExpense = editExpense;

            Receipt = _editExpense.ObserveProperty(x => x.Receipt)
                .Where(x => x != null)
                .Select(x => ImageSource.FromStream(x.GetStream))
                .ToReadOnlyReactiveProperty().AddTo(Disposable);

            PickPhotoAsyncCommand = new AsyncReactiveCommand().AddTo(Disposable);
            PickPhotoAsyncCommand.Subscribe(async _ => await _editExpense.PickPhotoAsync());

            TakePhotoAsyncCommand = new AsyncReactiveCommand().AddTo(Disposable);
            TakePhotoAsyncCommand.Subscribe(async _ => await _editExpense.TakePhotoAsync());
        }

        public void Destroy()
        {
            Disposable.Dispose();
        }
    }
}
