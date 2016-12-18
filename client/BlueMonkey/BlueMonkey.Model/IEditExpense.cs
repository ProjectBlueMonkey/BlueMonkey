using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.Business;
using BlueMonkey.MediaServices;

namespace BlueMonkey.Model
{
    public interface IEditExpense : INotifyPropertyChanged
    {
        Expense Expense { get; }
        IMediaFile Receipt { get; }
        IEnumerable<Category> Categories { get; }
        Category SelectedCategory { get; set; }
        Task InitializeAsync();

        Task PickPhotoAsync();

        Task TakePhotoAsync();

        Task SaveAsync();
    }
}
