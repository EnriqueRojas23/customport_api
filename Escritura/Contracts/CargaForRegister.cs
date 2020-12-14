using System;

namespace API.Escritura.Contracts
{
    public class CargaForRegister
    {
        
        public string carganumero {get;set;}
        public decimal volumen {get;set;}
        public decimal peso {get;set;}
        public int bultos {get;set;}
        public int idvehiculo {get;set;}
        public int idusuarioregistro {get;set;}
        public DateTime fechahoraregistro {get;set;}
        public int? idagencia {get;set;}
        public string ids {get;set;}
        public int idtiposervicio {get;set;}
        public int idestado {get;set;}
    }
}
