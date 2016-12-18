using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using BlueMonkey.Model;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Xamarin.Forms;

namespace BlueMonkey.ViewModels
{
    public class ReceiptPageViewModel : BindableBase
    {
        private readonly IEditExpense _editExpense;
        public ReadOnlyReactiveProperty<ImageSource> Receipt { get; }
        public AsyncReactiveCommand PickPhotoAsyncCommand { get; }
        public AsyncReactiveCommand TakePhotoAsyncCommand { get; }

        public ReceiptPageViewModel(IEditExpense editExpense)
        {
            _editExpense = editExpense;

            Receipt = _editExpense.ObserveProperty(x => x.Receipt)
                .Where(x => x != null)
                .Select(x => ImageSource.FromStream(x.GetStream))
                .ToReadOnlyReactiveProperty();

            PickPhotoAsyncCommand = new AsyncReactiveCommand();
            PickPhotoAsyncCommand.Subscribe(async _ => await _editExpense.PickPhotoAsync());

            TakePhotoAsyncCommand = new AsyncReactiveCommand();
            TakePhotoAsyncCommand.Subscribe(async _ => await _editExpense.TakePhotoAsync());
        }
    }
}
