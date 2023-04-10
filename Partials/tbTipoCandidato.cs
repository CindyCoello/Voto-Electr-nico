using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VotoElectronico.Models
{
    [MetadataType(typeof(tbTipoCandidatoPartial))]
    public partial class tbTipoCandidato
    {

    }

    public class tbTipoCandidatoPartial
    {
        [Display(Name = "ID Tipo Candidato")]
        public int tipcan_Id { get; set; }

        [Display(Name = "Nombre Tipo Candidato ")]
        [Required(ErrorMessage = "El Tipo Candidato {0} es requerido")]
        public string tipcan_Descripcion { get; set; }


    }
}