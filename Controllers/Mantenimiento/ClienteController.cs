using System;
using System.Threading.Tasks;
using Api.Data.Interface;
using Api.Domain.Mantenimiento;
using Api.ReadRepository.Interface.Mantenimiento;
using Api.Repository.Contracts.Mantenimiento;
using Api.Repository.Interface.Mantenimiento;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CargaClic.API.Controllers.Mantenimiento
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IRepository<Cliente> _repoCliente;
        private readonly IRepository<Destinatario> _repoDestinatario;
        private readonly IClienteRepository _repository;
        private readonly IClienteReadRepository _read_repository;
        private readonly IMapper _mapper;

        public ClienteController(IRepository<Cliente> repoCliente,
         IClienteRepository repository_cliente, IClienteReadRepository read_repository
         , IMapper mapper, IRepository<Destinatario> repoDestinatario)
        {
            _repoCliente = repoCliente;
            _repository = repository_cliente;
            _read_repository = read_repository;
            _mapper = mapper;
            _repoDestinatario = repoDestinatario;
        }
        [HttpGet("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var usuario = await  _repoCliente.Get(x=> x.id == id);
            return Ok(usuario);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           var usuarios = await  _repoCliente.GetAll();
            return Ok(usuarios);
        }
      
        [HttpGet("GetAllClientes")]
        public async Task<IActionResult> GetAllClientes(string criterio)
        {
            var result = await _read_repository.GetAllClientes(criterio);
            return Ok(result);
        }
       
        [HttpPost("ClientRegister")]
        public async Task<IActionResult> ClientRegister(ClienteForRegister model)
        {
            var ocliente = new Cliente { 
                razonsocial = model.razonsocial,
                ruc = model.ruc,
                tipodocumentoid = model.tipodocumentoid,
                fechaactualizacion = null,
                fechacreacion = DateTime.Now,
                activo = true
            };

            var result = await _repoCliente.AddAsync(ocliente);
            await _repoCliente.SaveAll();
            return Ok(result);
        }
        [HttpDelete("ClientDelete")]
        public async Task<IActionResult> ClientDelete(int id)
        {
            var cliente = await _repoCliente.Get(x=>x.id == id);
            cliente.activo = false;
            await _repoCliente.SaveAll();
            return Ok();
        }
        [HttpPost("ClientUpdate")]
        public async Task<IActionResult> ClientUpdate(ClienteForUpdate model)
        {
             var client = await _repoCliente.Get(x=>x.id == model.id);
             client.razonsocial = model.razonsocial;
             client.ruc = model.ruc;
             client.tipodocumentoid = model.tipodocumentoid;
             client.fechacreacion = DateTime.Now;
             await _repoCliente.SaveAll();
             return Ok();
        }
        [HttpGet("GetAllUbigeo")]
        public async Task<IActionResult> GetAllUbigeo(string criterio)
        {
           var result = await  _read_repository.GetAllUbigeo(criterio);
           return Ok(result);
        }
        [HttpGet("GetAllDestinatario")]
        public async Task<IActionResult> GetAllDestinatario(int idcliente)
        {
           var result = await  _repoDestinatario.Get(x => x.idcliente == idcliente);
           return Ok(result);
        }
        
        
        // [HttpGet("GetAllDirecciones")]
        // public async Task<IActionResult> GetAllDirecciones(int Id)
        // {
        //     var resp = await  _repository.GetAllDirecciones(Id);
        //     return Ok(resp);
        // }
        // [HttpPost("AddressRegister")]
        // public async Task<IActionResult> AddressRegister(AddressForRegister model)
        // {
        //     var result = await _repository_Cliente.AddressRegister(model);
        //     return Ok(result);
        // }
        // [HttpGet("GetAllDepartamentos")]
        // public async Task<IActionResult> GetAllDepartamentos()
        // {
        //     var resp = await  _repository.GetAllDepartamentos();
        //     return Ok(resp);
        // }
        // [HttpGet("GetAllProvincias")]
        // public async Task<IActionResult> GetAllProvincias(int DepartamentoId)
        // {
        //     var resp = await  _repository.GetAllProvincias(DepartamentoId);
        //     return Ok(resp);
        // }
        // [HttpGet("GetAllDistritos")]
        // public async Task<IActionResult> GetAllDistritos(int ProvinciaId)
        // {
        //     var resp = await  _repository.GetAllDistritos(ProvinciaId);
        //     return Ok(resp);
        // }
    }
}