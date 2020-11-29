
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Api.Data;
using Api.Data.Contracts.Results.Seguridad;
using Api.Domain.Seguridad;
using Api.Lectura.Handlers;
using API.Lectura.Interface.Seguridad;
using API.Lectura.Results;
using API.Lectura.Results.Seguridad;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace API.Lectura.Repository
{
    public class SeguridadRepository : ISeguridadRepository
    {    

        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public SeguridadRepository(DataContext context,IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public IDbConnection Connection
        {   
            get
            {
                var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                try
                {
                     connection.Open();
                     return connection;
                }
                catch (System.Exception)
                {
                    connection.Close();
                    connection.Dispose();
                    throw;
                }
            }
        }

        public async Task<IEnumerable<MenuResult>> GetMenu(int rolid)
        {
            using (var conn = new ConnectionFactory(_config).GetOpenConnection())
            {
                 var parametros = new DynamicParameters();
                 parametros.Add("IdRol", dbType: DbType.Int32, direction: ParameterDirection.Input, value: rolid);
                 
                var result = await conn.QueryAsync<MenuResult>("seguridad.pa_listarmenu"
                                                                        ,parametros
                                                                        ,commandType:CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IEnumerable<TreeviewItem>> GetMenuTreeviewItem(int rolid)
        {
            using (var conn = new ConnectionFactory(_config).GetOpenConnection())
            {
                 var parametros = new DynamicParameters();
                 parametros.Add("IdRol", dbType: DbType.Int32, direction: ParameterDirection.Input, value: rolid);
                 // var result = new TreeviewItem();
                  var result=  await conn.QueryAsync<TreeviewItem>("seguridad.pa_listarTreeView"
                                                                        ,parametros
                                                                        ,commandType:CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IEnumerable<UsersResult>> GetUsers(string criterio)
        {
            using (var conn = new ConnectionFactory(_config).GetOpenConnection())
            {
                 var parametros = new DynamicParameters();
                 parametros.Add("criterio", dbType: DbType.String, direction: ParameterDirection.Input, value: criterio);
                 
                var result = await conn.QueryAsync<UsersResult>("seguridad.pa_listarusuarios"
                                                                        ,parametros
                                                                        ,commandType:CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IEnumerable<Rol>> ListarRolesPorUsuario(int userid)
        {
             using (var conn = new ConnectionFactory(_config).GetOpenConnection())
            {
                 var parametros = new DynamicParameters();
                 parametros.Add("UserId", dbType: DbType.Int32, direction: ParameterDirection.Input, value: userid);
                 var result = await conn.QueryAsync<Rol>("seguridad.pa_listarrolesxusuario"
                                                                        ,parametros
                                                                        ,commandType:CommandType.StoredProcedure);
                return result;
            }
        }
    }
}