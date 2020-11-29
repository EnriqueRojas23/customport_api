using System.Collections.Generic;
using Api.Common;


namespace Api.Domain.Seguridad
{
    public class Rol: Entity
    {
        public int Id  { get; set; }
        public string Descripcion { get; set; }
        public string Alias { get; set; }
        public bool Activo { get; set; }
        public bool Publico { get; set; }   
        public ICollection<RolPagina> RolPaginas {get;set;}
        public ICollection<RolUser> RolUser {get;set;}
        
    }
}