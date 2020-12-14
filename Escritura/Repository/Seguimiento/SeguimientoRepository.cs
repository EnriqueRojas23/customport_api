using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Domain.Mantenimiento;
using Api.Lectura.Handlers;
using Api.Repository.Contracts.Mantenimiento;
using API.Domain.Seguimiento;
using API.Escritura.Contracts;
using API.Escritura.Interface.Seguimiento;
using API.Lectura.Results.Seguimiento;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Api.Repository.Repository.Mantenimiento
{
    public class SeguimientoRepository : ISeguimientoRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public SeguimientoRepository(DataContext context,IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public Task<long> CargaAsignar(Carga ordenForRegister)
        {
            throw new NotImplementedException();
        }

        public Task<long> CargaDetele(Carga ordenForRegister)
        {
            throw new NotImplementedException();
        }


        public async  Task<long> CargaRegister(CargaForRegister parameters)
        {
            Carga carga;
            string query = "";

            using(var transaction = _context.Database.BeginTransaction())
            {

                 try
                  {
                    carga = new Carga();
                    carga.carganumero = "";
                    carga.fechahoraregistro =DateTime.Now;
                    carga.idagencia = parameters.idagencia;
                    carga.idusuarioregistro = parameters.idusuarioregistro;
                    carga.idvehiculo = parameters.idvehiculo;
                    carga.peso = parameters.peso;
                    carga.volumen = parameters.volumen;
                    carga.bultos = parameters.bultos;
                    carga.idestado = 11;

                    await  _context.Cargas.AddAsync(carga);
                    await _context.SaveChangesAsync();
                     transaction.Commit();

                     query = string.Format("update seguimiento.ordenservicio set cargaid = {0} , estadoid = {1} , idtiposervicio = {2},  idagencia = {3}, fechaplanificacion = '{4}'  where id in ({5})",
                                   carga.id.ToString(),
                                   "5",
                                   parameters.idtiposervicio.ToString(),

                                   (parameters.idagencia.HasValue ? parameters.idagencia.ToString() : "null"),

                                   DateTime.Now.ToString("M/d/yyyy HH:mm:ss"),
                                   parameters.ids);

                    using (var conn = new ConnectionFactory(_config).GetOpenConnection())
                    {
                        var parametros = new DynamicParameters();
                        var result = await conn.QueryAsync<OrdenesServicioResult>(query
                                                                    ,parametros
                                                                    ,commandType:CommandType.Text);
                        
                    }
                    
            

                   
                  }
                  catch (System.Exception ex)
                  {
                      throw ex;
                  }
                  return 1;
                 
            }


            // using (var conn = new ConnectionFactory(_config).GetOpenConnection())
            // {


            //    var parametros = new DynamicParameters();
                
            
            //         query = string.Format("update seguimiento.ordenservicio set idcarga = {0} , idestado = {1} , idtipooperacion = {2},  idagencia = {3}, fechaplanificacion = '{4}'  where id in ({4})",
            //                        carga.id.ToString(),
            //                        "5",
            //                        parameters.idtiposervicio.ToString(),
            //                        (parameters.idagencia.HasValue ? parameters.idagencia.ToString() : "null"),
            //                        DateTime.Now,
            //                        parameters.ids);
                
             

            //     var result = await 
            //             conn.QueryAsync<OrdenesServicioResult>(query
            //                                     ,parametros
            //                                     ,commandType:CommandType.Text);
            //     return 1;
            // }
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

        public async Task<long> OrdenServicioRegister(OrdenServicioForRegister clienteForRegister)
        {
            OrdenServicio ordenServicio;
            GuiaRemisionRemitente guiaRemisionRemitente;
          
            using(var transaction = _context.Database.BeginTransaction())
            {
                  try
                  {
                    ordenServicio = new OrdenServicio();
                    ordenServicio.activo = clienteForRegister.activo;
                    ordenServicio.direccionentrega = clienteForRegister.direccionentrega;
                    ordenServicio.volumen = clienteForRegister.volumen;
                    ordenServicio.estadoid = 4; // Registrado
                    ordenServicio.fechadespacho = null;
                    ordenServicio.fechaentrega = null;
                    ordenServicio.fecharecojo = clienteForRegister.fecharecojo;
                    ordenServicio.fecharegistro = clienteForRegister.fecharegistro;
                    ordenServicio.horaentrega = null;
                    ordenServicio.idcliente = clienteForRegister.idcliente;
                    ordenServicio.iddestinatario = clienteForRegister.iddestinatario;
                    ordenServicio.iddestino = clienteForRegister.iddestino;
                    ordenServicio.idmanifiesto = null;
                    ordenServicio.idorigen = clienteForRegister.idorigen;
                    ordenServicio.idtiposervicio = clienteForRegister.idtiposervicio;
                    ordenServicio.numeroservicio = "";
                    

                    await  _context.OrdenServicios.AddAsync(ordenServicio);
                    await _context.SaveChangesAsync();


                    
                    
                    string[] prm =  clienteForRegister.grr.Split(',');
                    foreach (var item in prm)
                    {
                        if(item == "") continue;
                        guiaRemisionRemitente = new GuiaRemisionRemitente();
                        guiaRemisionRemitente.numeroguia = item;
                        guiaRemisionRemitente.idordenservicio = ordenServicio.id;
                        _context.guiaRemisionRemitentes.Add(guiaRemisionRemitente);


                    }

                    ordenServicio.numeroservicio =  ordenServicio.id.ToString().PadLeft(10,'0');
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
                  return ordenServicio.id;
                 
            }
        }

        public async Task<long> OrdenServicioUpdate(OrdenServicioForUpdate ordenForUpdate)
        {
            OrdenServicio ordenServicio;
            ordenServicio  = _context.OrdenServicios.Where(x=>x.id == ordenForUpdate.id).SingleOrDefault();

          
            using(var transaction = _context.Database.BeginTransaction())
            {
                  try
                  {
                    
                    ordenServicio.activo = ordenForUpdate.activo;
                    ordenServicio.direccionentrega = ordenForUpdate.direccionentrega;
                    ordenServicio.volumen = ordenForUpdate.volumen;
                    ordenServicio.estadoid = ordenForUpdate.estadoid;
                    

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
                  return ordenServicio.id;
                 
            }
        }
    }
}