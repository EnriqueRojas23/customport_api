using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Api.Data;
using Api.Lectura.Handlers;
using API.Lectura.Interface.Seguimiento;
using API.Lectura.Results.Seguimiento;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace API.Lectura.Repository.Seguimiento
{
    public class SeguimientoReadRepository : ISeguimientoReadRepository
    {
         private readonly DataContext _context;
        private readonly IConfiguration _config;

        public SeguimientoReadRepository(DataContext context,IConfiguration config)
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

        public async Task<IEnumerable<OrdenesServicioResult>> ListarOrdenesServicio()
        {
            using (var conn = new ConnectionFactory(_config).GetOpenConnection())
            {
                 var parametros = new DynamicParameters();
                 //parametros.Add("criterio", dbType: DbType.String, direction: ParameterDirection.Input, value: criterio);
                 
                var result = await conn.QueryAsync<OrdenesServicioResult>("seguimiento.pa_listarordenservicio"
                                                                        ,parametros
                                                                        ,commandType:CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IEnumerable<OrdenesServicioResult>> ListarOrdenesServicioProgramacion()
        {
             using (var conn = new ConnectionFactory(_config).GetOpenConnection())
            {
                 var parametros = new DynamicParameters();
                 //parametros.Add("criterio", dbType: DbType.String, direction: ParameterDirection.Input, value: criterio);
                 
                var result = await conn.QueryAsync<OrdenesServicioResult>("seguimiento.pa_listarordenservicio_programacion"
                                                                        ,parametros
                                                                        ,commandType:CommandType.StoredProcedure);
                return result;
            }
        }
    }
}