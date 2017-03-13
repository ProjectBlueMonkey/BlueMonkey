using System;
using Prism.Mvvm;

namespace BlueMonkey
{
    public class Expense : BindableBase
    {
        private string _id;

        public string Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _categoryId;

        public string CategoryId
        {
            get { return _categoryId; }
            set { SetProperty(ref _categoryId, value); }
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

        public string ReportId
        {
            get { return _reportId; }
            set { SetProperty(ref _reportId, value); }
        }

        private string _userId;
        public string UserId
        {
            get { return _userId; }
            set { SetProperty(ref _userId, value); }
        }
    }
}
