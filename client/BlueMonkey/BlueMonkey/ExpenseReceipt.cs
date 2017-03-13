using Prism.Mvvm;

namespace BlueMonkey
{
    public class ExpenseReceipt : BindableBase
    {
        private string _id;

        public string Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _expenseId;
        public string ExpenseId
        {
            get { return _expenseId; }
            set { SetProperty(ref _expenseId, value); }
        }

        private string _receiptUri;
        public string ReceiptUri
        {
            get { return _receiptUri; }
            set { SetProperty(ref _receiptUri, value); }
        }

        private string _userId;
        public string UserId
        {
            get { return _userId; }
            set { SetProperty(ref _userId, value); }
        }

    }
}