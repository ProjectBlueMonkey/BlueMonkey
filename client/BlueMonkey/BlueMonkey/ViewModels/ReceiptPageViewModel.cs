using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueMonkey.ViewModels
{
    public class ReceiptPageViewModel : BindableBase
    {
        public DelegateCommand SaveCommand => new DelegateCommand(Save);

        public ReceiptPageViewModel()
        {

        }

        private void Save()
        {

        }
    }
}
