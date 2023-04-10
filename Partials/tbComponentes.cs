using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VotoElectronico.Models
{
    [MetadataType(typeof(tbComponentesPartial))]
    public partial class tbComponentes
    {

    }

    public class tbComponentesPartial
    {
        [Display(Name = "ID Componente")]
        public int comp_Id { get; set; }
        [Display(Name = "Nombre Componente")]
        [Required(ErrorMessage= "El componente {0} es requerido")]
        public string comp_Nombre { get; set; }


    }
}