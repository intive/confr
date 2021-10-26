using System.Threading.Tasks;
using Intive.ConfR.Domain.Entities;

namespace Intive.ConfR.Application.Interfaces
{
    public interface IUserService
    {
        Task<GraphUser> GetPersonalData();
    }
}
