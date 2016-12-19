// if connect azure then uncomment this line.
#define AZURE

using BlueMonkey.ExpenseServices;
using BlueMonkey.MediaServices;
#if AZURE
using BlueMonkey.ExpenseServices.Azure;
#else
using BlueMonkey.ExpenseServices.Local;
#endif
using BlueMonkey.Model;
using BlueMonkey.TimeService;
using Prism.Unity;
using BlueMonkey.Views;
using Xamarin.Forms;
using Microsoft.Practices.Unity;
using Microsoft.WindowsAzure.MobileServices;

namespace BlueMonkey
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

#if AZURE
            NavigationService.NavigateAsync("LoginPage");
#else
            NavigationService.NavigateAsync("NavigationPage/MainPage");
#endif
        }

        protected override void RegisterTypes()
        {
            Container.RegisterInstance<IMobileServiceClient>(new MobileServiceClient(Secrets.ServerUri));

#if AZURE
            Container.RegisterType<IExpenseService, AzureExpenseService>(new ContainerControlledLifetimeManager());
#else
            Container.RegisterType<IExpenseService, ExpenseService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IFileUploadService, FileUploadService>(new ContainerControlledLifetimeManager());
#endif
            Container.RegisterType<IDateTimeService, DateTimeService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMediaService, MediaService>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IEditReport, EditReport>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IReferReport, ReferReport>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IEditExpense, EditExpense>(new ContainerControlledLifetimeManager());

            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<AddExpensePage>();
            Container.RegisterTypeForNavigation<ExpenseListPage>();
            Container.RegisterTypeForNavigation<ChartPage>();
            Container.RegisterTypeForNavigation<ReportPage>();
            Container.RegisterTypeForNavigation<ReceiptPage>();
            Container.RegisterTypeForNavigation<AddReportPage>();
            Container.RegisterTypeForNavigation<ReportListPage>();
            Container.RegisterTypeForNavigation<ExpenseSelectionPage>();
            Container.RegisterTypeForNavigation<LoginPage>();
        }
    }
}
