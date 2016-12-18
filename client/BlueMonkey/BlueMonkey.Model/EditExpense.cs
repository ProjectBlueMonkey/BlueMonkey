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

        private Expense _expense;
        public Expense Expense
        {
            get { return _expense; }
            set { SetProperty(ref _expense, value); }
        }

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
            Expense = new Expense
            {
                Date = _dateTimeService.Today
            };
            Categories = await _expenseService.GetCategoriesAsync();
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
            await _expenseService.RegisterExpensesAsync(Expense, new[] {new ExpenseReceipt {ReceiptUri = uri.ToString()}});
        }
    }
}
