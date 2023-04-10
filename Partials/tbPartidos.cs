
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace VotoElectronico.Models
{
    [MetadataType(typeof(tbPartidosPartial))]

    public partial class tbPartidos
    {
        [NotMapped]
        public HttpPostedFileBase Logofile { get; set; }
    }


    public class tbPartidosPartial
    {
        [Display(Name = "ID Partido")]
        public int part_Id { get; set; }

        [Display(Name = "Nombre Partido")]
        public string part_Nombre { get; set; }

        [Display(Name = "Siglas")]
        public string part_Siglas { get; set; }

        [Display(Name = "Sede")]
        public string part_Sede { get; set; }

        [Display(Name = "Color Representativo")]
        public string part_ColorEmblema { get; set; }

        [Display(Name = "Logo")]
        public string part_Logo { get; set; }
    }
}