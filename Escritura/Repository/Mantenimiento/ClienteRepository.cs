using System;
using System.Threading.Tasks;
using Api.Data;
using Api.Domain.Mantenimiento;
using Api.Repository.Contracts.Mantenimiento;
using Api.Repository.Interface.Mantenimiento;
using Microsoft.Extensions.Configuration;

namespace Api.Repository.Repository.Mantenimiento
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public ClienteRepository(DataContext context,IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public Task<int> AddressRegister(AddressForRegister ownerClientForAttach)
        {
            throw new NotImplementedException();
        }

        public async Task<int> ClientRegister(ClienteForRegister clienteForRegister)
        {
          
            Cliente cliente;
            

          
            using(var transaction = _context.Database.BeginTransaction())
            {
                  try
                  {
                    cliente = new Cliente();
                    cliente.ruc = clienteForRegister.ruc;
                    cliente.razonsocial = clienteForRegister.razonsocial;
                    
                    

                    await  _context.Cliente.AddAsync(cliente);
                    await _context.SaveChangesAsync();

                    transaction.Commit();
                  }
                  catch (System.Exception ex)
                  {
                        transaction.Rollback(); 
                        var sqlException = ex.InnerException as System.Data.SqlClient.SqlException;
                        if (sqlException.Number == 2601 || sqlException.Number == 2627)
                            throw new ArgumentException("El cliente ya existe");
                        else
                            throw new ArgumentException("Error al insertar");
                  }
                  return cliente.id;
                 
            }
        }


        // public async Task<int> AddressRegister(AddressForRegister addressForRegister)
        // {
        //      Direccion direccion ;
   
        //      using(var transaction = _context.Database.BeginTransaction())
        //      {
        //           try
        //           {
        //                 direccion = new Direccion();
        //                 direccion.Activo = true;
        //                 direccion.Clienteid = addressForRegister.ClienteId;
        //                 direccion.codigo = addressForRegister.Codigo;
        //                 direccion.direccion = addressForRegister.Direccion;
        //                 direccion.iddistrito = addressForRegister.DistritoId;
                        
        //                 await  _context.Direccion.AddAsync(direccion);
        //                 await _context.SaveChangesAsync();
        //                 transaction.Commit();
                        
        //                 return direccion.iddireccion;
        //          }
        //           catch (System.Exception)
        //           {
        //                 transaction.Rollback(); 
        //                 throw;
                       
        //           }
                 
        //      }
        // }
    }
}