//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DSS.Data.Models.Entities.Repositories
{
    using System;
    using System.Collections.Generic;
    
    
    public partial interface IAspNetUserRepository : SkyWeb.DatVM.Data.IBaseRepository<AspNetUser>
    {
    }
    
    public partial class AspNetUserRepository : SkyWeb.DatVM.Data.BaseRepository<AspNetUser>, IAspNetUserRepository
    {
    	public AspNetUserRepository(System.Data.Entity.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
