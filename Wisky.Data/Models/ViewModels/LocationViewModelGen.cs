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
    
    public partial class LocationViewModel : SkyWeb.DatVM.Mvc.BaseEntityViewModel<DSS.Data.Models.Entities.Location>
    {
    	
    			public virtual int LocationID { get; set; }
    			public virtual int BrandID { get; set; }
    			public virtual string Province { get; set; }
    			public virtual string District { get; set; }
    			public virtual string Address { get; set; }
    			public virtual Nullable<System.DateTime> CreateDatetime { get; set; }
    			public virtual string Description { get; set; }
    	
    	public LocationViewModel() : base() { }
    	public LocationViewModel(DSS.Data.Models.Entities.Location entity) : base(entity) { }
    
    }
}
