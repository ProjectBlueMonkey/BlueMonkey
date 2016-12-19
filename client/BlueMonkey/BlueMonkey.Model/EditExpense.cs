using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMonkey.Business;
using BlueMonkey.ExpenseServices;
using BlueMonkey.MediaServices;
using BlueMonkey.TimeService;
using Prism.Mvvm;

namespace BlueMonkey.Model
{
    /// <summary>
    /// Use cases to register and update expense.
    /// </summary>
    public class EditExpense : BindableBase, IEditExpense
    {
        /// <summary>
        /// IExpenseService field.
        /// </summary>
        private readonly IExpenseService _expenseService;

        /// <summary>
        /// IFileUploadService field.
        /// </summary>
        private readonly IFileUploadService _fileUploadService;

        /// <summary>
        /// IDateTimeService field.
        /// </summary>
        private readonly IDateTimeService _dateTimeService;

        /// <summary>
        /// IMediaService field.
        /// </summary>
        private readonly IMediaService _mediaService;

        /// <summary>
        /// Backing field of Name property.
        /// </summary>
        private string _name;
        /// <summary>
        /// Name of expense.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        /// <summary>
        /// Backing field of Amount property.
        /// </summary>
        private long _amount;
        /// <summary>
        /// Amount of expense.
        /// </summary>
        public long Amount
        {
            get { return _amount; }
            set { SetProperty(ref _amount, value); }
        }

        /// <summary>
        /// Backing field of Date property.
        /// </summary>
        private DateTime _date;
        /// <summary>
        /// Date of expense.
        /// </summary>
        public DateTime Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }

        /// <summary>
        /// Backing field of Location property.
        /// </summary>
        private string _location;
        /// <summary>
        /// Location of expense.
        /// </summary>
        public string Location
        {
            get { return _location; }
            set { SetProperty(ref _location, value); }
        }

        /// <summary>
        /// Backing field of Note property.
        /// </summary>
        private string _note;
        /// <summary>
        /// Note of expense.
        /// </summary>
        public string Note
        {
            get { return _note; }
            set { SetProperty(ref _note, value); }
        }

        /// <summary>
        /// Backing field of Receipt property.
        /// </summary>
        private IMediaFile _receipt;
        /// <summary>
        /// Receipt of expense.
        /// </summary>
        public IMediaFile Receipt
        {
            get { return _receipt; }
            set { SetProperty(ref _receipt, value); }
        }

        /// <summary>
        /// Backing field of Categories property.
        /// </summary>
        private IEnumerable<Category> _categories;
        /// <summary>
        /// Categories of expense.
        /// </summary>
        public IEnumerable<Category> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }

        /// <summary>
        /// Backing field of SelectedCategory property.
        /// </summary>
        private Category _selectedCategory;
        /// <summary>
        /// SelectedCategory of expense.
        /// </summary>
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set { SetProperty(ref _selectedCategory, value); }
        }

        /// <summary>
        /// Initialize Instance.
        /// </summary>
        /// <param name="expenseService"></param>
        /// <param name="fileUploadService"></param>
        /// <param name="dateTimeService"></param>
        /// <param name="mediaService"></param>
        public EditExpense(
            IExpenseService expenseService,
            IFileUploadService fileUploadService,
            IDateTimeService dateTimeService, 
            IMediaService mediaService)
        {
            _expenseService = expenseService;
            _fileUploadService = fileUploadService;
            _dateTimeService = dateTimeService;
            _mediaService = mediaService;
        }

        /// <summary>
        /// Initialize use case.
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync()
        {
            Name = null;
            Amount = 0;
            Date = _dateTimeService.Today;
            Location = null;
            Note = null;
            Receipt = null;
            Categories = await _expenseService.GetCategoriesAsync();
            SelectedCategory = Categories.FirstOrDefault();
        }

        /// <summary>
        /// Gets if ability to take photos supported on the device
        /// </summary>
        public bool IsTakePhotoSupported => _mediaService.IsCameraAvailable && _mediaService.IsTakePhotoSupported;

        /// <summary>
        /// Gets if the ability to pick photo is supported on the device
        /// </summary>
        public bool IsPickPhotoSupported { get; }

        /// <summary>
        /// Pick phote.
        /// </summary>
        /// <returns></returns>
        public async Task PickPhotoAsync()
        {
            Receipt = await _mediaService.PickPhotoAsync();
        }

        /// <summary>
        /// Take phote.
        /// </summary>
        /// <returns></returns>
        public async Task TakePhotoAsync()
        {
            Receipt = await _mediaService.TakePhotoAsync();
        }

        /// <summary>
        /// Register or update expense.
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            var uri = await _fileUploadService.UploadMediaFileAsync(Receipt);
            var expense = new Expense
            {
                Name = Name,
                Amount = Amount,
                Date = Date,
                Location = Location,
                Note = Note
            };
            await _expenseService.RegisterExpensesAsync(expense, new[] {new ExpenseReceipt {ReceiptUri = uri.ToString()}});
        }
    }
}
