
using Android.App;
using Android.Content.PM;
using Android.OS;
using BlueMonkey.ExpenseServices;
using BlueMonkey.ExpenseServices.Azure;
using BlueMonkey.LoginService;
using BlueMonkey.LoginService.Azure.Droid;
using Microsoft.Practices.Unity;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Prism.Unity;

namespace BlueMonkey.Droid
{
    [Activity(Label = "BlueMonkey", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);
            CurrentPlatform.Init();
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ILoginService, AzureLoginService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IFileStorageService, AzureFileStorageService>(new ContainerControlledLifetimeManager());
        }
    }
}

