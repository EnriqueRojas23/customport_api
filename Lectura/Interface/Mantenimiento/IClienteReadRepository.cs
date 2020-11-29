using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.ReadRepository.Contracts.Mantenimiento.Results;

namespace Api.ReadRepository.Interface.Mantenimiento
{
    public interface IClienteReadRepository
    {

         Task<IEnumerable<GetAllClientesResult>> GetAllClientes(String Criterio);
        //  Task<IEnumerable<GetAllDireccionesResult>> GetAllDirecciones(int ClienteId);

        //  Task<IEnumerable<GetAllDepartamentos>> GetAllDepartamentos();
        //  Task<IEnumerable<GetAllProvincias>> GetAllProvincias(int DepartamentoId);
        //  Task<IEnumerable<GetAllDistritos>> GetAllDistritos(int ProvinciaId);
        //  Task<IEnumerable<Proveedor>> GetAllProvedores();





    }
}