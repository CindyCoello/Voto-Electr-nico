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
    
    public partial class tbModuloPantallas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbModuloPantallas()
        {
            this.tbRolModuloPantallas = new HashSet<tbRolModuloPantallas>();
        }
    
        public int modpan_Id { get; set; }
        public Nullable<int> mod_Id { get; set; }
        public string modpan_Nombre { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbRolModuloPantallas> tbRolModuloPantallas { get; set; }
        public virtual tbModulos tbModulos { get; set; }
    }
}
