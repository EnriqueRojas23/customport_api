using System;
using System.ComponentModel.DataAnnotations;
using Api.Common;

namespace API.Domain.Seguimiento
{
    public class OrdenServicio : Entity
    {
        [Key]
        public long id {get;set;}
        public string numeroservicio {get;set;}
        public int idcliente {get;set;}
        public int iddestinatario {get;set;}
        public int idorigen {get;set;}
        public int iddestino {get;set;}
        public decimal volumen {get;set;}
        public decimal peso {get;set;}
        public int idtiposervicio {get;set;}
        public DateTime fecharegistro {get;set;}
        public DateTime fecharecojo {get;set;}
        public DateTime? fechadespacho {get;set;}
        public string direccionentrega {get;set;}
        public DateTime? fechaentrega {get;set;}
        public string horaentrega {get;set;}
        public int? idmanifiesto {get;set;}
        public int estadoid {get;set;}
        public bool activo {get;set;}
    }
}