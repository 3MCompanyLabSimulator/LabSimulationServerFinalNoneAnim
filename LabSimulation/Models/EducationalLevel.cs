//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LabSimulation.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EducationalLevel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EducationalLevel()
        {
            this.EducationalYears = new HashSet<EducationalYear>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string ImageLevelPath { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EducationalYear> EducationalYears { get; set; }
    }
}
