using System.Collections.Generic;

namespace API.Lectura.Results
{
    public class MenuResult
    {
        public int Id {get;set;}
        public string Codigo	{ get;set; }
        public string CodigoPadre	{ get;set; }
        public string Title	{ get;set; }
        public string Path	{ get;set; }
        public bool isExternalLink {get;set;}
        public string Nivel	{ get;set; }
        public string Orden	{ get;set; }
        public string Class {get;set;}
        public string Icon	{ get;set; }        
        public string srp_seleccion { get;set; }
        public bool visible {get;set;}
        public List<MenuResult> submenu {get;set;}
    }
}