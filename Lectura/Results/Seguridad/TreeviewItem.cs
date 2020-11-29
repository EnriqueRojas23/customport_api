using System.Collections.Generic;


namespace Api.Data.Contracts.Results.Seguridad
{

    public class TreeviewItem
    {
        public int Value {get;set;}
        public string Codigo	{ get;set; }
        public string CodigoPadre	{ get;set; }
        public string Text	{ get;set; }
        public string Link	{ get;set; }
        public string Nivel	{ get;set; }
        public string Orden	{ get;set; }
        public string Icono	{ get;set; }        
        public bool check { get;set; }
        public List<TreeviewItem> children {get;set;}

    } 
}