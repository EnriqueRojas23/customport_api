using System.ComponentModel.DataAnnotations;

namespace Api.Repository.Contracts.Mantenimiento
{
    public class AddressForRegister
    {
        [Required]
        public int ClienteId {get;set;}
        [Required]
        public string Codigo {get;set;}
        [Required]
        public string Direccion {get;set;}
        [Required]
        public int DistritoId {get;set;}
    }
}