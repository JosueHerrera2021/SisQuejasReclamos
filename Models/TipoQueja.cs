//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SisQuejasReclamos.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TipoQueja
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoQueja()
        {
            this.Quejas = new HashSet<Queja>();
        }
    
        public int Id_TipoQueja { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public int Id_EstadoG { get; set; }
    
        public virtual EstadoG EstadoG { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Queja> Quejas { get; set; }
    }
}
