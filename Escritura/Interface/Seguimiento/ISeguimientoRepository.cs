using System.Threading.Tasks;
using API.Domain.Seguimiento;
using API.Escritura.Contracts;

namespace API.Escritura.Interface.Seguimiento
{
    public interface ISeguimientoRepository
    {
        Task<long> OrdenServicioRegister(OrdenServicioForRegister ordenForRegister);
        Task<long> OrdenServicioUpdate(OrdenServicioForUpdate ordenForRegister);


        Task<long> CargaRegister(CargaForRegister ordenForRegister);
        Task<long> CargaDetele(Carga ordenForRegister);
        Task<long> CargaAsignar(Carga ordenForRegister);






    }
}