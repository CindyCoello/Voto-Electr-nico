using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VotoElectronico.Models
{
    [MetadataType(typeof(tbUsuariosPartial))]
    public partial class tbUsuarios
    {
        [NotMapped]

        public string accion { get; set; }
        public string passaword { get; set; }
    }


    public class tbUsuariosPartial
    {

        public int usu_Id { get; set; }
        public string usu_Identidad { get; set; }
        public string usu_PrimerNombre { get; set; }
        public string usu_PrimerApellido { get; set; }
        public string usu_SegundoNombre { get; set; }
        public string usu_SegundoApellido { get; set; }
        public string usu_Telefono { get; set; }
        public string usu_Contraseña { get; set; }
        public int rol_Id { get; set; }
        public bool usu_EsActivo { get; set; }
    }
}