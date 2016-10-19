using Prism.Unity;
using BlueMonkey.Views;
using Xamarin.Forms;
using Microsoft.Practices.Unity;
using BlueMonkey.Services.Interfaces;
using BlueMonkey.Services.Local;

namespace BlueMonkey
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterType<IExpenseService, LocalExpenseService>(new ContainerControlledLifetimeManager());

            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<AddExpensePage>();
            Container.RegisterTypeForNavigation<ExpenseListPage>();
            Container.RegisterTypeForNavigation<ChartPage>();
            Container.RegisterTypeForNavigation<ReportPage>();
            Container.RegisterTypeForNavigation<ReceiptPage>();
            Container.RegisterTypeForNavigation<AddReportPage>();
        }
    }
}
