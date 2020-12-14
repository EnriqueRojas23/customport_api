

using System;
using Api.Common;

namespace Api.Domain.Mantenimiento
{
    public class Destinatario : Entity
    {
        public int id {get;set;}
        public string razonsocial {get;set;}
        public int idcliente {get;set;}
    }
}