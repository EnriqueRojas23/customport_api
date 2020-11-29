using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Repository.Contracts.Mantenimiento
{
   
    public class ClienteForRegister
    {
        [Required]
        public string razonsocial {get;set;}
        [Required]
        public string ruc {get;set;}
        public int tipodocumentoid {get;set;}
        
    }
     public class ClienteForUpdate
    {
        [Required]
        public int id {get;set;}
        [Required]
        public string razonsocial {get;set;}
        [Required]
        public string ruc {get;set;}
        public int tipodocumentoid {get;set;}
        
    }
   
}