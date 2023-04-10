using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VotoElectronico.Models
{
    [MetadataType(typeof(tbClientePartial))]
    public partial class tbCliente
    {

    }

    public class tbClientePartial
    {
        [Display(Name = "ID Cliente")]
        public int cli_Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El Nombre {0} es requerido")]
        public string cli_Nombre { get; set; }

        [Display(Name = "RTN")]
        [Required(ErrorMessage = "El RTN {0} es requerido")]
        public string cli_RTN { get; set; }

        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "El Teléfono {0} es requerido")]
        public string cli_Telefono { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "La Dirección {0} es requerido")]
        public string cli_Dirección { get; set; }

    }
}