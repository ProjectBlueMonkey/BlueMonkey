using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueMonkey.ViewModels
{
    public class AddReportPageViewModel : BindableBase
    {
        public DelegateCommand SubmitReportCommand => new DelegateCommand(SubmitReport);

        public AddReportPageViewModel()
        {

        }

        private void SubmitReport()
        {
            
        }
    }
}
