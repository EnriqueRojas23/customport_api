using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Lectura.Interface.Seguimiento;
using System.Threading.Tasks;
using API.Escritura.Interface.Seguimiento;
using API.Escritura.Contracts;
using Api.Data.Interface;
using Api.Domain.Mantenimiento;
using System;

namespace CargaClic.API.Controllers.Despacho
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenController : ControllerBase
    {

        
        private readonly IConfiguration _config;
        private readonly ISeguimientoReadRepository _repoReadSeguimiento;
        private readonly ISeguimientoRepository _repoSeguimiento;
        private readonly IRepository<Destinatario> _repodestinatario;

        public OrdenController(IConfiguration config,
         ISeguimientoReadRepository repoReadSeguimiento,
         ISeguimientoRepository repoSeguimiento, 
         IRepository<Destinatario> repodestinatario)

        {
            _config = config;
            _repoReadSeguimiento = repoReadSeguimiento;
            _repodestinatario = repodestinatario;
            _repoSeguimiento = repoSeguimiento;
        }

        [HttpGet("GetAllOrdenSeguimiento")]
        public async Task<IActionResult> GetAllOrdenSeguimiento()
        {
            var resp = await _repoReadSeguimiento.ListarOrdenesServicio();
            return Ok(resp);
        }
        [HttpGet("GetAllOrdenProgramacion")]
        public async Task<IActionResult> GetAllOrdenProgramacion()
        {
            var resp = await _repoReadSeguimiento.ListarOrdenesServicioProgramacion();
            return Ok(resp);
        }

        [HttpPost("registarCarga")]
        public async Task<IActionResult> registarCarga(CargaForRegister cargaForRegister )
        {

            var resp = await _repoSeguimiento.CargaRegister(cargaForRegister);
            return Ok(resp);
        }

        [HttpPost("registrarOrden")]
        public async Task<IActionResult> registrarOrden(OrdenServicioForRegister ordenServicioForRegister)
        {
            var destinatario =
                     await _repodestinatario.Get( x=> x.razonsocial == ordenServicioForRegister.destinatario );
            if(destinatario == null)
            {
                Destinatario destinatarioNew = new Destinatario 
                        { 
                              razonsocial = ordenServicioForRegister.destinatario
                            , idcliente = ordenServicioForRegister.idcliente 
                        };
                await _repodestinatario.AddAsync(destinatarioNew);
                await _repodestinatario.SaveAll();

                ordenServicioForRegister.iddestinatario = destinatarioNew.id;
                
                  
            } else {
                ordenServicioForRegister.iddestinatario = destinatario.id;
            }

            
            ordenServicioForRegister.fecharegistro = DateTime.Now;
            
            ordenServicioForRegister.activo = true;

            var resp = await _repoSeguimiento.OrdenServicioRegister(ordenServicioForRegister);
            return Ok(resp);
        }

        [HttpPost("OrdenSeguimientoUpdate")]
        public async Task<IActionResult> OrdenSeguimientoUpdate(OrdenServicioForUpdate ordenServicioForUpdate)
        {
            var resp = await _repoSeguimiento.OrdenServicioUpdate(ordenServicioForUpdate);
            return Ok(resp);
        }




    

    }
}