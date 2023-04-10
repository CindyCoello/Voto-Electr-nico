using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VotoElectronico.Models
{
    [MetadataType(typeof(tbCentroElectoralPartial))]
    public partial  class tbCentroElectoral
    {
        [NotMapped]

        public string accion { get; set; }
    }


    public class tbCentroElectoralPartial
    {

        public int censo_Id { get; set; }
        public string censo_Identidad { get; set; }
        public string censo_PrimerNombre { get; set; }
        public string censo_SegundoNombre { get; set; }
        public string censo_PrimerApellido { get; set; }
        public string censo_SegundoApellido { get; set; }
        public string censo_CodigoSexo { get; set; }
        public System.DateTime censo_FechaNacimiento { get; set; }
        public int estCiv_Id { get; set; }
        public int depto_Id { get; set; }
        public int muni_Id { get; set; }
        public int aldea_Id { get; set; }
        public int pobl_Id { get; set; }
        public int censo_CodigoArea { get; set; }
        public int censo_CodigoSectorElectoral { get; set; }
        public int cenvot_Id { get; set; }
        public int censo_NumeroLinea { get; set; }
        public bool censo_EsHabilitado { get; set; }
        public Nullable<bool> censo_Voto { get; set; }
    }
}