//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DSS.Data.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class DeviceScenarioViewModel : SkyWeb.DatVM.Mvc.BaseEntityViewModel<DSS.Data.Models.Entities.DeviceScenario>
    {
    	
    			public virtual int DeviceScenationID { get; set; }
    			public virtual int DeviceID { get; set; }
    			public virtual int ScenarioID { get; set; }
    			public virtual Nullable<int> TimesToPlay { get; set; }
    			public virtual System.DateTime StartTime { get; set; }
    			public virtual Nullable<System.DateTime> EndTime { get; set; }
    	
    	public DeviceScenarioViewModel() : base() { }
    	public DeviceScenarioViewModel(DSS.Data.Models.Entities.DeviceScenario entity) : base(entity) { }
    
    }
}
