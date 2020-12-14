using System;

namespace API.Lectura.Results.Seguimiento
{
    public class OrdenesServicioResult
    {
        public int id	{get;set;}
        public string numeroservicio	{get;set;}
        public int idcliente	{get;set;}
        public int iddestinatario	{get;set;}
        public int idorigen	{get;set;}
        public int iddestino{get;set;}

        public string cliente	{get;set;}
        public string destinatario	{get;set;}
        public string origen	{get;set;}
        public string destino{get;set;}

        public DateTime fecharegistro {get;set;}
        public DateTime fecharecojo {get;set;}
        public DateTime fechaentrega {get;set;}

        public int bultos {get;set;}
        public decimal peso {get;set;}
        public decimal volumen {get;set;}
        
        public string estado {get;set;}
    }
}