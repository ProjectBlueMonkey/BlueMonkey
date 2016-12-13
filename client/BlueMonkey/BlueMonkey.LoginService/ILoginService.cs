using BlueMonkey.Business;
using System.Threading.Tasks;

namespace BlueMonkey.LoginService
{
    public interface ILoginService
    {
        Task<AuthUser> LoginAsync();
    }
}
