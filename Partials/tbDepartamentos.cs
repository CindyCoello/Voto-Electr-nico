using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VotoElectronico.Models
{
    [MetadataType(typeof(tbDepartamentosPartial))]
    public partial class tbDepartamentos
    {

    }
    public class tbDepartamentosPartial
    {
        [Display(Name = "ID Departamento")]
        public int depto_Id { get; set; }
        [Display(Name = "Nombre Departamento ")]
        [Required(ErrorMessage = "El Departamento {0} es requerido")]
        public string depto_Descripcion { get; set; }


    }
}