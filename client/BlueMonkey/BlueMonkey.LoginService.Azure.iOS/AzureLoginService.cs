using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace BlueMonkey.LoginService.Azure.iOS
{
    public class AzureLoginService : ILoginService
    {
        private readonly IMobileServiceClient _client;

        public AzureLoginService(IMobileServiceClient client)
        {
            _client = client;
        }

        public Task<bool> LoginAsync()
        {
            // TODO: Comment out once and implement it later.
            //var user = await _client.LoginAsync(UIApplication.SharedApplication.KeyWindow.RootViewController, 
            //    MobileServiceAuthenticationProvider.MicrosoftAccount);
            //return user != null;
            return Task.FromResult(true);
        }
    }
}
