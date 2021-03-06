//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SaleApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class Material
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Material()
        {
            this.Productions = new HashSet<Production>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public Nullable<int> material_type { get; set; }
        public Nullable<int> count_in_package { get; set; }
        public Nullable<int> unit { get; set; }
        public Nullable<int> amount_storage { get; set; }
        public Nullable<int> min_possible_amount { get; set; }
        public Nullable<decimal> price { get; set; }
    
        public virtual TypeMaterial TypeMaterial { get; set; }
        public virtual TypeUnit TypeUnit { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Production> Productions { get; set; }
    }
}
