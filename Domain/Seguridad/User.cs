using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Api.Common;


namespace Api.Domain.Seguridad
{
    public class User : Entity
    {
        public int Id {get;set;}
        public string Username { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email {get;set;}
        public bool EnLinea { get; set; }
        public bool EsConductor {get;set;}
        public DateTime Created { get; set; }
        public String Dni {get;set;}
        public DateTime? LastActive { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        public int EstadoId { get; set; }
        public string ClientesIds {get;set;}
        public ICollection<RolUser> RolUser {get;set;}
        public bool Activo {get;set;}
        
    }
}