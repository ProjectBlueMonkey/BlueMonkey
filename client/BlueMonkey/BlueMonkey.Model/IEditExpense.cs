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
    /// <summary>
    /// Use cases to register and update expense.
    /// </summary>
    public interface IEditExpense : INotifyPropertyChanged
    {
        /// <summary>
        /// Name of expense.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Amount of expense.
        /// </summary>
        long Amount { get; set; }

        /// <summary>
        /// Date of expense.
        /// </summary>
        DateTime Date { get; set; }

        /// <summary>
        /// Location of expense.
        /// </summary>
        string Location { get; set; }

        /// <summary>
        /// Note of expense.
        /// </summary>
        string Note { get; set; }

        /// <summary>
        /// Receipt of expense.
        /// </summary>
        IMediaFile Receipt { get; }

        /// <summary>
        /// Selectable categories.
        /// </summary>
        IEnumerable<Category> Categories { get; }

        /// <summary>
        /// Category of expense.
        /// </summary>
        Category SelectedCategory { get; set; }

        /// <summary>
        /// Initialize use case.
        /// </summary>
        /// <returns></returns>
        Task InitializeAsync();

        /// <summary>
        /// Gets if ability to take photos supported on the device
        /// </summary>
        bool IsTakePhotoSupported { get; }

        /// <summary>
        /// Gets if the ability to pick photo is supported on the device
        /// </summary>
        bool IsPickPhotoSupported { get; }

        /// <summary>
        /// Pick photo.
        /// </summary>
        /// <returns></returns>
        Task PickPhotoAsync();

        /// <summary>
        /// Take photo.
        /// </summary>
        /// <returns></returns>
        Task TakePhotoAsync();

        /// <summary>
        /// Register or update expense.
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}
