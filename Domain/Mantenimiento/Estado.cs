using System.Collections.Generic;
using Api.Common;
using Api.Domain.Seguridad;

namespace Api.Domain.Mantenimiento
{
    public class Estado : Entity
    {
        public int Id { get; set; }
        public string NombreEstado { get; set; }
        public string Descripcion { get; set; }
        public int TablaId {get;set;}
        public ICollection<User> Users {get;set;}
    }
}