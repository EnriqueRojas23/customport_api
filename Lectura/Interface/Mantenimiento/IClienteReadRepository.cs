using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.ReadRepository.Contracts.Mantenimiento.Results;

namespace Api.ReadRepository.Interface.Mantenimiento
{
    public interface IClienteReadRepository
    {

         Task<IEnumerable<GetAllClientesResult>> GetAllClientes(String Criterio);
         Task<IEnumerable<GetAllUbigeoResult>> GetAllUbigeo(String Criterio);






    }
}