//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VotoElectronico.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbCentrosVotacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbCentrosVotacion()
        {
            this.tbMesasElectorales = new HashSet<tbMesasElectorales>();
            this.tbCentroElectoral = new HashSet<tbCentroElectoral>();
        }
    
        public int cenvot_Id { get; set; }
        public int depto_Id { get; set; }
        public int muni_Id { get; set; }
        public int cenvot_CodigoArea { get; set; }
        public int cenvot_CodigoSectorElectoral { get; set; }
        public string cenvot_Nombre { get; set; }
        public double cenvot_Latitud { get; set; }
        public double cenvot_Longitud { get; set; }
        public int cenvot_TotalMesas { get; set; }
    
        public virtual tbDepartamentos tbDepartamentos { get; set; }
        public virtual tbMunicipios tbMunicipios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbMesasElectorales> tbMesasElectorales { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbCentroElectoral> tbCentroElectoral { get; set; }
    }
}
