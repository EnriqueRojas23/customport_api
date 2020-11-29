using System;

namespace CargaClic.API.Dtos
{
    public class UserForDetailedDto
    {
        public int Id {get;set;}
        public string Username { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email {get;set;}
        public bool EnLinea { get; set; }
        public string Dni { get; set; }
        public int EstadoId {get;set;}
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public bool? EsConductor {get;set;}
        public string clientesids {get;set;}
        public DateTime DateOfBirth { get; set; }
   
    } 
}
