using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BlueMonkey.MobileApp.Startup))]

namespace BlueMonkey.MobileApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}