//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DSS.Data.Models.Entities.Services
{
    using System;
    using System.Collections.Generic;
    
    
    public partial interface IVisualTypeService : SkyWeb.DatVM.Data.IBaseService<VisualType>
    {
    }
    
    public partial class VisualTypeService : SkyWeb.DatVM.Data.BaseService<VisualType>, IVisualTypeService
    {
        public VisualTypeService(SkyWeb.DatVM.Data.IUnitOfWork unitOfWork, Repositories.IVisualTypeRepository repository) : base(unitOfWork, repository)
        {
        }
    }
}