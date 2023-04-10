using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace VotoElectronico.Models
{
    [MetadataType(typeof(tbMovimientosPartial))]
    public  partial class tbMovimientos
    {
        [NotMapped]
        public HttpPostedFileBase Logofile { get; set; }
    }

    public class tbMovimientosPartial
    {
        [Display(Name = "ID Movimiento")]
        public int mov_Id { get; set; }
        [Display(Name = "Nombre Partido")]
        public int part_Id { get; set; }
        [Display(Name = "Nombre Movimiento")]
        public string mov_Nombre { get; set; }
        [Display(Name = "Logo")]
        public string mov_Logo { get; set; }
        [Display(Name = "Eslogan")]
        public string mov_Eslogan { get; set; }
        
    }
}