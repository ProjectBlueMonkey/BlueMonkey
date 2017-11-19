using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueMonkey.LoginService.Azure.Droid
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
            //var user = await _client.LoginAsync(Forms.Context, MobileServiceAuthenticationProvider.MicrosoftAccount);
            // https://github.com/jamesmontemagno/MediaPlugin/issues/136
            //await Task.Delay(100); // need... small delay
            //return user != null;
            return Task.FromResult(true);
        }
    }
}
