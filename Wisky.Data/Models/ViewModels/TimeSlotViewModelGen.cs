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
    
    public partial class TimeSlotViewModel : SkyWeb.DatVM.Mvc.BaseEntityViewModel<DSS.Data.Models.Entities.TimeSlot>
    {
    	
    			public virtual int SlotID { get; set; }
    			public virtual string SlotDetail { get; set; }
    	
    	public TimeSlotViewModel() : base() { }
    	public TimeSlotViewModel(DSS.Data.Models.Entities.TimeSlot entity) : base(entity) { }
    
    }
}
