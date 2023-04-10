using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VotoElectronico.Models
{
    [MetadataType(typeof(tbMunicipiosPartial))]
    public partial class tbMunicipios
    {

    }


    public class tbMunicipiosPartial
    {
        public int muni_Id { get; set; }
        public string muni_Descripcion { get; set; }
        public int depto_Id { get; set; }
        public int muni_Codigo { get; set; }

    }
}