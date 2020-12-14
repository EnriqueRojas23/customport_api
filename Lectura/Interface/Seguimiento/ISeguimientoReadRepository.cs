using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Lectura.Results.Seguimiento;

namespace API.Lectura.Interface.Seguimiento
{
    public interface ISeguimientoReadRepository
    {
        Task<IEnumerable<OrdenesServicioResult>> ListarOrdenesServicio();
        Task<IEnumerable<OrdenesServicioResult>> ListarOrdenesServicioProgramacion();
    }
}