using System.ComponentModel.DataAnnotations;

namespace CargaClic.API.Dtos.Matenimiento
{
    public class VehiculoForRegisterDto
    {
        public int? Id {get;set;}
        [Required]
        [MaxLength(6)]
        [MinLength(6)]
        public string Placa { get; set; }
        public int? TipoId { get; set; }
        public int ProveedorId {get;set;}
        public int? MarcaId { get; set; }
        //public int? ModeloId { get; set; }
        //public int? AnioId { get; set; }
        //public int? ColorId { get; set; }
        //public int? CombustibleId { get; set; }
        //public string Regmtc { get; set; }
        public string Confveh { get; set; }
        //[Required]
        //public decimal? CargaUtil { get; set; }
        //[Required]
        public decimal? PesoBruto { get; set; }
        public decimal? Volumen { get; set; }
        //public string SerieMotor { get; set; }
        //public string Kilometraje { get; set; }
    }
}