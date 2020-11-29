using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Contracts.Results.Seguridad;
using Api.Data.Interface;
using Api.Domain.Seguridad;
using API.Lectura.Interface.Seguridad;
using AutoMapper;
using CargaClic.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CargaClic.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRepository<Rol> _repo;
        private readonly IMapper _mapper;
        private readonly IRepository<RolPagina> _repo_option;
        private readonly ISeguridadRepository _repoSeguridad;
        private readonly IRepository<Pagina> _repo_Pagina;
        private readonly IRepository<RolUser> _repo_RolUser;

        public RolesController(IRepository<Rol> repo,
        IRepository<RolPagina> repoOption,
        IRepository<Pagina> repoPagina,
        IRepository<RolUser> repoRolUser,
        ISeguridadRepository repoSeguridad,
        IMapper mapper)
        {
            _repo_option = repoOption;
            _repo_Pagina = repoPagina;
            _repo_RolUser = repoRolUser;
            _repo = repo;
            _mapper = mapper;
            _repoSeguridad = repoSeguridad;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles(string criterio)
        {
            IEnumerable<Rol> roles ;
            if (criterio == null)
            {
               roles =     await _repo.GetAll(x=>x.Activo);
            }
            else
            {
                roles = await _repo.GetAll(x=>x.Activo && x.Descripcion.Contains(criterio.Trim()));
            }
            return Ok(roles);
        }
        [HttpDelete("deleteRol")]
        public async Task<IActionResult> deleteRol(int rolId)
        {
             var options = await _repo_option.GetAll(x=>x.IdRol  == rolId);
             if(options.Count() > 0)
             {
                 throw new System.ArgumentException("Parameter cannot be null", "original");
             }

             var  roluser  = await _repo_RolUser.GetAll(x=>x.RolId == rolId);
             if(roluser.Count() > 0)
             {
                 throw new System.ArgumentException("Parameter cannot be null", "original");
             }
        
            var rol = await _repo.Get(x=>x.Id == rolId);
            _repo.Delete(rol) ;
            
            return Ok();
        }


        [HttpGet("getallroles")]
        public async Task<IActionResult> getAllRolesForUser(int UserId)
        {
            var resp = await _repoSeguridad.ListarRolesPorUsuario(UserId);
            return Ok(resp);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RolForRegisterDto rolForRegisterDto)
        {
            var rolToCreate = new Rol
            {
                Descripcion = rolForRegisterDto.Descripcion,
                Alias = rolForRegisterDto.Alias,
                Publico = rolForRegisterDto.Publico,
                Activo = true

            };
            
            await _repo.AddAsync(rolToCreate);
            await _repo.SaveAll();
            return StatusCode(202);
        }




        [HttpPost("addroluser")]
        public async Task<IActionResult> AddRolUser(IEnumerable<RolUserForRegisterDto> rolUserForRegisterDto,int UserId)
        {

            #region Eliminar todo

                    if(rolUserForRegisterDto.ToList().Count == 0)
                    {
                        var todo = await _repo_RolUser.GetAll(x=>x.UserId == UserId);
                        _repo_RolUser.DeleteAll(todo);
                    }
                    else
                    {
                        var todo = await _repo_RolUser.GetAll(x=>x.UserId == UserId);
                        _repo_RolUser.DeleteAll(todo);
                    }

            #endregion


            foreach (var item in rolUserForRegisterDto)
            {

               var rol = await _repo.Get(x=>x.Alias == item.Alias);
               var RolUserToCreate = new RolUser
                {
                       UserId = UserId,
                       RolId  = rol.Id

                };
                // var exist = await _repo_RolUser.Get(x=>x.RolId == RolUserToCreate.RolId && x.UserId == RolUserToCreate.UserId);
                // if(exist == null)
                await _repo_RolUser.AddAsync(RolUserToCreate);
                await _repo_RolUser.SaveAll();

            }
            return Ok(rolUserForRegisterDto);
        }


     
      
      
        [HttpGet("obtenermenu")]
        public async Task<IActionResult> ObtenerMenu(int idRol)
        {
            var pantallas = await _repoSeguridad.GetMenuTreeviewItem(idRol);

            List<TreeviewItem> final = new List<TreeviewItem>();
            foreach (var item in pantallas.ToList())
            {

                if (item.Nivel == "1")
                {

                    item.children = new List<TreeviewItem>();
                    item.children.AddRange(pantallas.Where(x => x.CodigoPadre == item.Codigo && x.Nivel == "2").ToList());
                    final.Add(item);
                }
            }
            return Ok(final);
        }


        [HttpPost("addoption")]
        public async Task<IActionResult> AddOptions(IEnumerable<RolForAddOptionDto> rolForAddOptionDto, int idrol)
        {
            if(rolForAddOptionDto.Count() == 0) {
                  var roles =   await _repo_option.GetAll(x=> x.IdRol == idrol);
                   _repo_option.DeleteAll(roles);
                    return Ok();
            }
              var rolespagina = await _repo_option.GetAll(x=>x.IdRol== rolForAddOptionDto.ToList()[0].IdRol) ;

              _repo_option.DeleteAll(rolespagina);

              var total  = await _repo_Pagina.GetAll();

              
              foreach (var item in rolForAddOptionDto)
              {
                  var rolPaginaCreate = new RolPagina
                  {
                    IdRol = item.IdRol,
                    IdPagina = item.IdPagina,
                    permisos = item.permisos
                
                  };
                   var aux  =  total.Where(x=>x.Id == rolPaginaCreate.IdPagina).SingleOrDefault();
                   var padre = total.Where(x=>x.Codigo == aux.CodigoPadre).SingleOrDefault();

                  var exist =   await _repo_option.Get(x=>x.IdPagina == padre.Id && x.IdRol == item.IdRol );
                  if( exist == null)
                  {
                    var rolPaginaCreatePadre = new RolPagina
                    {
        
                        IdRol = item.IdRol,
                        IdPagina = padre.Id,
                        permisos = "AME"
                    
                    };
                    await _repo_option.AddAsync(rolPaginaCreatePadre);
                  }

                   await _repo_option.AddAsync(rolPaginaCreate);
              }

             
          
            return Ok();
        }



    }
}