using System.ComponentModel.DataAnnotations;
using Api.Common;

namespace Api.Domain.Mantenimiento
{

    public class ValorTabla : Entity
    {
        
        [Key]
        public int Id { get; set; }  

        public string ValorPrincipal { get; set; }
        public string ValorPrimero { get; set; }
        public string ValorSegundo { get; set; }

        public int TablaId {get;set;}
        public bool Visible {get;set;}
        public int Orden {get;set;}

        
    }
}