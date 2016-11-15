using BlueMonkey.Business;

namespace BlueMonkey.Model
{
    public class SelectableExpense : Expense
    {
        public SelectableExpense(Expense expense)
        {
            Id = expense.Id;
            CategoryId = expense.CategoryId;
            Name = expense.Name;
            Amount = expense.Amount;
            Date = expense.Date;
            Location = expense.Location;
            Note = expense.Note;
            ReportId = expense.ReportId;
            UserId = expense.UserId;
        }
        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }
    }
}