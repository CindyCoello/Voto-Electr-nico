using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VotoElectronico.Models
{
    [MetadataType(typeof(tbEstadosCivilPartial))]
    public partial class tbEstadosCivil
    {

    }

    public class tbEstadosCivilPartial
    {
        [Display(Name = "ID Estado Civil")]
        public int estCiv_Id { get; set; }
        [Display(Name = "Estado Civil")]
        [Required(ErrorMessage = "El Estado Civil {0} es requerido")]
        public string estCiv_Descripcion { get; set; }


    }
}