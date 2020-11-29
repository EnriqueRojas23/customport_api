using System;
using System.Threading.Tasks;
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
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> _repo;
        private readonly IAuthRepository _auth;
        private readonly IMapper _mapper;
        private readonly ISeguridadRepository _repo_seguridad;

        public UsersController(IRepository<User> repo
        ,ISeguridadRepository repo_seguridad
        ,IAuthRepository auth
        , IMapper mapper)
        {
        
            _mapper = mapper;
            _repo = repo;
            _repo_seguridad = repo_seguridad;
            _auth = auth;
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int userid)
        {
            var userDb = await _repo.Get(x=>x.Id == userid);
            userDb.Activo = false;
            await _repo.SaveAll();

            return Ok();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
            if (await _auth.UserExists(userForRegisterDto.Username))
                return BadRequest("Username ya existe");

            var userToCreate = new User
            {
                Username = userForRegisterDto.Username,
                Nombres = userForRegisterDto.FirstName,
                Apellidos = userForRegisterDto.LastName,
                Email = userForRegisterDto.Email,
                Created = DateTime.Now,
                LastActive = DateTime.Now,
                EstadoId = 1,
                Dni = userForRegisterDto.Dni    ,
                DateOfBirth = userForRegisterDto.dob

                
            };

            var createdUser = await _auth.Register(userToCreate, userForRegisterDto.Password);
            return StatusCode(201);
        }
        [HttpPost("updateestado")]
        public async Task<IActionResult> UpdateEstado(UserForUpdateDto userForUpdateDto )
        {
          
            var userToUpdate = new User
            {
                Id = userForUpdateDto.Id,
                EstadoId = userForUpdateDto.EstadoId,
            };

            var createdUser = await _auth.UpdateEstadoId(userToUpdate);
            return StatusCode(200);
        }
        [HttpPost("update")]
        public async Task<IActionResult> Update(UserForUpdateDto userForRegisterDto)
        {
          
            var userToCreate = new User
            {
                Id = userForRegisterDto.Id,
                Nombres = userForRegisterDto.Nombres,
                Apellidos = userForRegisterDto.Apellidos,
                Email = userForRegisterDto.Email,
                Dni = userForRegisterDto.Dni,
                EstadoId = userForRegisterDto.EstadoId,
                DateOfBirth = userForRegisterDto.DateOfBirth
                
            };

            var createdUser = await _auth.Update(userToCreate);
            return StatusCode(201);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUsers(string criterio)
        {
            var resp = await _repo_seguridad.GetUsers(criterio);
            return Ok(resp);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.Get(x => x.Id == id);
            var userToResult = _mapper.Map<UserForDetailedDto>(user);
            return Ok(userToResult);
        }


    }
}