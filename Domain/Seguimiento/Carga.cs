using System;
using Api.Common;

namespace API.Domain.Seguimiento
{
    public class Carga: Entity
    {
        public long id {get;set;}
        public string carganumero {get;set;}
        public decimal volumen {get;set;}
        public decimal peso {get;set;}
        public int bultos {get;set;}
        public int idvehiculo {get;set;}
        public int idusuarioregistro {get;set;}
        public DateTime fechahoraregistro {get;set;}
        public int? idagencia {get;set;}
        public int idestado {get;set;}

    }
}