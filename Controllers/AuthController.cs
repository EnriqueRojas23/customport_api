using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Api.Data.Interface;
using Api.Domain.Seguridad;
using API.Lectura.Interface.Seguridad;
using API.Lectura.Results;
using CargaClic.API.Dtos;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;


namespace CargaClic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IRepository<RolUser> _repo_roluser;
        private readonly ISeguridadRepository _repo_seguridad;
        readonly ILogger<AuthController> _log;


        public AuthController(IAuthRepository repo
        , IConfiguration config
        , ISeguridadRepository repo_seguridad
        , IRepository<RolUser> repo_roluser
        ,  ILogger<AuthController> log
        )
        {
            _config = config;
            _repo = repo;
            _repo_roluser = repo_roluser;
            _repo_seguridad = repo_seguridad;
            _log = log;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
           _log.LogInformation("Hello, world!");
            var escliente = false;
            var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

            if (userFromRepo == null)
                return Unauthorized();
            if(userFromRepo.EstadoId == 2)
               return StatusCode(403,"Bloqueado");
            if(userFromRepo.EstadoId == 3)
               return StatusCode(403,"Eliminado");


            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            var rolesFromRepo = await _repo_roluser.GetAll(x=>x.UserId == userFromRepo.Id);
            List<MenuResult> _menu = new List<MenuResult>();
            List<MenuResult> _menuAux = new List<MenuResult>();
            List<MenuResult> final = new List<MenuResult>();

            
            foreach (var rol in rolesFromRepo)
            {
                 _menu.AddRange(await _repo_seguridad.GetMenu(rol.RolId));
            }
             
             foreach (var item in _menu)
             {
                 if(_menuAux.Where(x=>x.Id == item.Id).SingleOrDefault() == null)
                 {
                        _menuAux.Add(item);
                 }
             }
            
            
            foreach (var item in _menuAux.Where(x=>x.srp_seleccion == "1").OrderBy(x=>x.Orden))
            {   
                if (item.Nivel=="1")
                {
                    item.submenu = new List<MenuResult>();
                    item.Class = "sub";
                    item.submenu.AddRange(_menuAux.Where(x=>x.CodigoPadre == item.Codigo && x.Nivel == "2" && x.srp_seleccion=="1" ).OrderBy(x=>x.Orden).ToList());
                    if(final.Where(x=>x.Id == item.Id).SingleOrDefault() == null)
                    final.Add(item);
                }
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8
             .GetBytes(_config.GetSection("AppSettings:Token").Value));

             var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

             var tokenDescriptor = new SecurityTokenDescriptor
             {
                 Subject = new ClaimsIdentity(claims),
                 Expires = DateTime.Now.AddDays(365),
                 SigningCredentials = creds
             };

             var tokenHandler = new JwtSecurityTokenHandler();

             var token = tokenHandler.CreateToken(tokenDescriptor);

             return Ok(new {
                 token = tokenHandler.WriteToken(token),
                 id_usr = userFromRepo.Id,
                 menu = final,
                 rol_id = escliente
             });


        }


    }
}