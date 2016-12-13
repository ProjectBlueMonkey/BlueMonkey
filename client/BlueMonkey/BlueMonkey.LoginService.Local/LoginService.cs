using BlueMonkey.Business;
using System;
using System.Threading.Tasks;

namespace BlueMonkey.LoginService.Local
{
    public class LoginService : ILoginService
    {
        public Task<AuthUser> LoginAsync()
        {
            return Task.FromResult(new AuthUser { Identity = Guid.NewGuid().ToString() });
        }
    }
}
