using Prism.Mvvm;

namespace BlueMonkey.Business
{
    public class AuthUser : BindableBase
    {
        private string _identity;

        public string Identity
        {
            get { return _identity; }
            set { SetProperty(ref _identity, value); }
        }

    }
}
