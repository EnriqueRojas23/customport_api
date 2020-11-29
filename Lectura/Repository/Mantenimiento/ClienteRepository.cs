using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Api.Data;
using Api.ReadRepository.Contracts.Mantenimiento.Results;
using Api.ReadRepository.Interface.Mantenimiento;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Api.Repository.Repository.Mantenimiento
{
    public class ClienteReadRepository : IClienteReadRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public ClienteReadRepository(DataContext context,IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public IDbConnection Connection
        {   
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public async Task<IEnumerable<GetAllClientesResult>> GetAllClientes(string Criterio)
        {
           var parametros = new DynamicParameters();
            parametros.Add("criterio", dbType: DbType.String, direction: ParameterDirection.Input, value: Criterio);
            using (IDbConnection conn = Connection)
            {
                string sQuery = "[Mantenimiento].[pa_listarclientes]";
                conn.Open();
                var result = await conn.QueryAsync<GetAllClientesResult>(sQuery, parametros ,commandType:CommandType.StoredProcedure);
                return result;
            }
        }
    }
}