

using System;
using Api.Common;

namespace Api.Domain.Mantenimiento
{
    public class Cliente : Entity
    {
        public int id {get;set;}
        public string razonsocial {get;set;}
        public string ruc {get;set;}
        public int tipodocumentoid {get;set;}
        public bool activo {get;set;}
        public DateTime? fechacreacion {get;set;}
        public DateTime? fechaactualizacion {get;set;}
   
    }
}