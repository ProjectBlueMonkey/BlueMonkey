using System.Threading.Tasks;

namespace BlueMonkey.LoginService.Local
{
    public class LoginService : ILoginService
    {
        public Task<bool> LoginAsync()
        {
            return Task.FromResult(true);
        }
    }
}
