namespace BlueMonkey.Usecases
{
    public class SelectableExpense : Expense
    {
        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }

        public SelectableExpense(Expense expense)
        {
            Id = expense.Id;
            CategoryId = expense.CategoryId;
            Amount = expense.Amount;
            Date = expense.Date;
            Location = expense.Location;
            Note = expense.Note;
            ReportId = expense.ReportId;
            UserId = expense.UserId;
        }
    }
}