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


    public partial interface IBrandService : SkyWeb.DatVM.Data.IBaseService<Brand>
    {
    }

    public partial class BrandService : SkyWeb.DatVM.Data.BaseService<Brand>, IBrandService
    {
        public BrandService(SkyWeb.DatVM.Data.IUnitOfWork unitOfWork, Repositories.IBrandRepository repository) : base(unitOfWork, repository)
        {
        }
    }
}
