using System;
using System.ComponentModel.DataAnnotations;
using Api.Common;

namespace API.Domain.Seguimiento
{
    public class GuiaRemisionRemitente : Entity
    {
        [Key]
        public long id {get;set;}
        public string numeroguia {get;set;}
        public long idordenservicio {get;set;}
        
    }
}