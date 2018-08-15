//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DSS.Data.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Scenario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Scenario()
        {
            this.DeviceScenarios = new HashSet<DeviceScenario>();
            this.ScenarioItems = new HashSet<ScenarioItem>();
            this.Schedules = new HashSet<Schedule>();
        }
    
        public int ScenarioID { get; set; }
        public int LayoutID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int BrandID { get; set; }
        public Nullable<bool> isPublic { get; set; }
        public int AudioArea { get; set; }
        public Nullable<System.DateTime> UpdateDateTime { get; set; }
    
        public virtual Brand Brand { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeviceScenario> DeviceScenarios { get; set; }
        public virtual Layout Layout { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScenarioItem> ScenarioItems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
