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
    public class EditExpense : BindableBase, IEditExpense
    {
        private readonly IExpenseService _expenseService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMediaService _mediaService;

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private long _amount;
        public long Amount
        {
            get { return _amount; }
            set { SetProperty(ref _amount, value); }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }

        private string _location;

        public string Location
        {
            get { return _location; }
            set { SetProperty(ref _location, value); }
        }

        private string _note;

        public string Note
        {
            get { return _note; }
            set { SetProperty(ref _note, value); }
        }

        private string _reportId;

        private IMediaFile _receipt;
        public IMediaFile Receipt
        {
            get { return _receipt; }
            set { SetProperty(ref _receipt, value); }
        }

        private IEnumerable<Category> _categories;

        public IEnumerable<Category> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }

        private Category _selectedCategory;

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

        public async Task InitializeAsync()
        {
            Date = _dateTimeService.Today;
            Categories = await _expenseService.GetCategoriesAsync();
            SelectedCategory = Categories.FirstOrDefault();
        }

        public async Task PickPhotoAsync()
        {
            Receipt = await _mediaService.PickPhotoAsync();
        }

        public async Task TakePhotoAsync()
        {
            Receipt = await _mediaService.TakePhotoAsync();
        }

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
