//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TUSSAN_TUNQUI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TipoMercaderia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoMercaderia()
        {
            this.Mercaderia = new HashSet<Mercaderia>();
        }
    
        public int idTipoMerca { get; set; }
        public string descripcionTipoMerca { get; set; }
        public Nullable<decimal> precioMerca { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mercaderia> Mercaderia { get; set; }
    }
}