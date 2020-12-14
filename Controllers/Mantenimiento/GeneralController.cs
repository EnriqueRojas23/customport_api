using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Data.Interface;
using Api.Domain.Mantenimiento;
using Api.ReadRepository.Interface.Mantenimiento;

namespace API.Controllers.Mantenimiento
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {

        private readonly IRepository<ValorTabla> _repoValorTabla;
        private readonly IRepository<Estado> _repoestado;
        private readonly IClienteReadRepository _repoReadCliente;

        private readonly IMapper _mapper;

        public GeneralController(
         IRepository<ValorTabla> repoValorTabla
        , IRepository<Estado> repoestado
        , IMapper mapper
        , IClienteReadRepository repoReadCliente)
        {
            _repoValorTabla = repoValorTabla;
            _repoestado = repoestado;
            _mapper = mapper;
            _repoReadCliente = repoReadCliente;
        }
        [HttpGet("GetAllEstados")]
        public async Task<IActionResult> GetAllEstados(int TablaId)
        {
           var result = await _repoestado.GetAll(x=>x.TablaId == TablaId);
           return Ok(result);
        }
       
        [HttpGet("GetAllValorTabla")]
        public async Task<IActionResult> GetAllValorTabla(int TablaId)
        {
           var result = await _repoValorTabla.GetAll(x=>x.TablaId == TablaId);
           return Ok(result);
        }
       

    }
}