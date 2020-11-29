using System.Collections.Generic;

namespace API.Lectura.Results
{
    public class MenuResult
    {
        public int Id {get;set;}
        public string Codigo	{ get;set; }
        public string CodigoPadre	{ get;set; }
        public string Descripcion	{ get;set; }
        public string Link	{ get;set; }
        public string Nivel	{ get;set; }
        public string Orden	{ get;set; }
        public string Icono	{ get;set; }        
        public string srp_seleccion { get;set; }
        public bool visible {get;set;}
        public List<MenuResult> submenu {get;set;}
    }
}