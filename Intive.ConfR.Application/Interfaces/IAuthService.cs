using System.Threading.Tasks;

namespace Intive.ConfR.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> GetAccessToken();
    }
}