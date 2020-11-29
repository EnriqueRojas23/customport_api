using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data.Contracts.Results.Seguridad;
using Api.Domain.Seguridad;
using API.Lectura.Results;
using API.Lectura.Results.Seguridad;

namespace API.Lectura.Interface.Seguridad
{
    public interface ISeguridadRepository
    {
          Task<IEnumerable<MenuResult>> GetMenu(int rolid);
          Task<IEnumerable<TreeviewItem>> GetMenuTreeviewItem(int rolid);
          Task<IEnumerable<UsersResult>> GetUsers(string criterio);
          Task<IEnumerable<Rol>> ListarRolesPorUsuario(int userid);
    }
}