using System.Threading.Tasks;
using Api.Domain.Seguridad;

namespace Api.Data.Interface
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Update(User user);
        Task<User> UpdatePassword(User user, string password);

        Task<User> UpdateEstadoId(User user);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}