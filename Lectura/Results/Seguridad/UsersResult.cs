using System;

namespace API.Lectura.Results.Seguridad
{
    public class UsersResult
    {
        public int Id	{get;set;}
        public string Username	{get;set;}
        public string Nombres	{get;set;}
        public string Apellidos	{get;set;}
        public string Email{get;set;}
        public string Dni	{get;set;}
        public string Enlinea	{get;set;}
        public DateTime LastActive	{get;set;}
        public string NombreEstado{get;set;}
        public DateTime DateOfBirth {get;set;}

    }
}