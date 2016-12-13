using System.Threading.Tasks;

namespace BlueMonkey.LoginService
{
    public interface ILoginService
    {
        Task<bool> LoginAsync();
    }
}
