using System.Threading.Tasks;
using Api.Repository.Contracts.Mantenimiento;

namespace Api.Repository.Interface.Mantenimiento
{
    public interface IClienteRepository
    {
        Task<int> ClientRegister(ClienteForRegister clienteForRegister);
        Task<int> AddressRegister(AddressForRegister ownerClientForAttach);
    } 
}
