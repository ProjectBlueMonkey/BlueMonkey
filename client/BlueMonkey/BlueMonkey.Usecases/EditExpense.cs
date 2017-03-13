using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMonkey.ExpenseServices;
using BlueMonkey.MediaServices;
using BlueMonkey.TimeService;

namespace BlueMonkey.Usecases
{
    /// <summary>
    /// Use cases to register and update expense.
    /// </summary>
    public class EditExpense : ModelBase, IEditExpense
    {
        /// <summary>
        /// IExpenseService field.
        /// </summary>
        private readonly IExpenseService _expenseService;

        /// <summary>
        /// IFileStorageService field.
        /// </summary>
        private readonly IFileStorageService _fileStorageService;

        /// <summary>
        /// IDateTimeService field.
        /// </summary>
        private readonly IDateTimeService _dateTimeService;

        /// <summary>
        /// IMediaService field.
        /// </summary>
        private readonly IMediaService _mediaService;

        /// <summary>
        /// Id of expense.
        /// </summary>
        private string _expenseId;
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
        /// <param name="fileStorageService"></param>
        /// <param name="dateTimeService"></param>
        /// <param name="mediaService"></param>
        public EditExpense(
            IExpenseService expenseService,
            IFileStorageService fileStorageService,
            IDateTimeService dateTimeService, 
            IMediaService mediaService)
        {
            _expenseService = expenseService;
            _fileStorageService = fileStorageService;
            _dateTimeService = dateTimeService;
            _mediaService = mediaService;
        }

        /// <summary>
        /// Initialize use case.
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync()
        {
            Date = _dateTimeService.Today;
            Categories = await _expenseService.GetCategoriesAsync();
            SelectedCategory = Categories.FirstOrDefault();
        }

        /// <summary>
        /// Initialize use case for update expense.
        /// </summary>
        /// <param name="expenseId"></param>
        /// <returns></returns>
        public async Task InitializeAsync(string expenseId)
        {
            _expenseId = expenseId;
            var expense = await _expenseService.GetExpenseAsync(expenseId);
            Amount = expense.Amount;
            Date = expense.Date;
            Location = expense.Location;
            Note = expense.Note;
            Categories = await _expenseService.GetCategoriesAsync();
            SelectedCategory = Categories.SingleOrDefault(x => x.Id == expense.CategoryId);

            var expenseReceipts = await _expenseService.GetExpenseReceiptsAsync(expenseId);
            var expenseReceipt = expenseReceipts.SingleOrDefault();
            if (expenseReceipt != null)
            {
                Receipt = await _fileStorageService.DownloadMediaFileAsync(new Uri(expenseReceipt.ReceiptUri));
            }
        }

        /// <summary>
        /// Gets if ability to take photos supported on the device
        /// </summary>
        public bool IsTakePhotoSupported => _mediaService.IsCameraAvailable && _mediaService.IsTakePhotoSupported;

        /// <summary>
        /// Gets if the ability to pick photo is supported on the device
        /// </summary>
        public bool IsPickPhotoSupported => _mediaService.IsPickPhotoSupported;

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
            var uri = await _fileStorageService.UploadMediaFileAsync(Receipt);
            var expense = new Expense
            {
                Id = _expenseId,
                Amount = Amount,
                Date = Date,
                Location = Location,
                Note = Note,
                CategoryId = SelectedCategory?.Id
            };
            await _expenseService.RegisterExpensesAsync(expense, new[] {new ExpenseReceipt {ReceiptUri = uri.ToString()}});
        }
    }
}
