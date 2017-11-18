// if connect azure then uncomment this line.
#define DEBUG

using BlueMonkey.ExpenseServices;
using BlueMonkey.ExpenseServices.Local;
using BlueMonkey.MediaServices;
using BlueMonkey.TimeService;
using BlueMonkey.Transaction.Unity;
using BlueMonkey.Usecases;
using BlueMonkey.ViewModels;
using BlueMonkey.Views;
using Microsoft.Practices.Unity;
using Microsoft.WindowsAzure.MobileServices;
using Prism.Mvvm;
using Prism.Unity;
using Xamarin.Forms;
#if Azure
using BlueMonkey.ExpenseServices.Azure;
#else
#endif

namespace BlueMonkey
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

#if Azure
            NavigationService.NavigateAsync("LoginPage");
#else
            NavigationService.NavigateAsync("MainPage");
#endif
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            PageNavigationTypeResolver.AssignAssemblies<MainPage, MainPageViewModel>();
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(PageNavigationTypeResolver.ResolveForViewModelType);
        }

        protected override void RegisterTypes()
        {
            Container.RegisterInstance<IMobileServiceClient>(new MobileServiceClient(Secrets.ServerUri));

#if Azure
            Container.RegisterType<IExpenseService, AzureExpenseService>(new ContainerControlledLifetimeManager());
#else
            Container.RegisterType<IExpenseService, ExpenseService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IFileStorageService, FileStorageService>(new ContainerControlledLifetimeManager());
#endif
            Container.RegisterType<IDateTimeService, DateTimeService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMediaService, MediaService>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IEditReport, EditReport>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IReferReport, ReferReport>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IEditExpense, EditExpense>(new TransactionLifetimeManager());
            Container.RegisterType<IReferExpense, ReferExpense>(new TransactionLifetimeManager());

            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<AddExpensePage>();
            Container.RegisterTypeForNavigation<ExpenseListPage>();
            Container.RegisterTypeForNavigation<ChartPage>();
            Container.RegisterTypeForNavigation<ReportPage>();
            Container.RegisterTypeForNavigation<ReceiptPage>();
            Container.RegisterTypeForNavigation<ReportListPage>();
            Container.RegisterTypeForNavigation<ExpenseSelectionPage>();
            Container.RegisterTypeForNavigation<LoginPage>();
        }
    }
}
